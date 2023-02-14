using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewClient : ProxyClient
{
    public CounterModel data;
    
    private Dictionary<Subscription, Action<System.Object>> subHandler = new Dictionary<Subscription, Action<System.Object>>();

    public ViewClient(CounterModel data) : base()
    {
        this.data = data;
        
      
    }

    public void Subscribe(string scope, string eventName, Action<System.Object> onEvent)
    {
        this.subHandler.Add(new Subscription(scope.Trim(), eventName.Trim()), onEvent) ;
       
        
    }

    public void Publish(string scope, string eventName, System.Object data)
    {
        base.Publish(scope, eventName, data);
    }

    public void Start()
    {
        base.Connect();
    }

    public override void OnReady() 
    { 
        foreach(KeyValuePair<Subscription, Action<System.Object>> entry in this.subHandler)
        {
            base.Subscribe(entry.Key.Scope(), entry.Key.EventName());

        }
    }

    public override void OnData(CounterModel jsonData)
    {
        this.data = jsonData;
        
    }

    public override void OnDataUpdate(string jsonData)
    {
        JsonUtility.FromJsonOverwrite(jsonData, this.data);
    }

    public override void OnEvent(string scope, string eventName, System.Object data) 
    {
        
        Action<System.Object> tmp;
        if (this.subHandler.TryGetValue(new Subscription(scope.Trim(), eventName.Trim()), out tmp))
        {
            tmp(data);
        }
        else
        {
            Debug.Log("Method not found");
            
        }
        
    }






}


