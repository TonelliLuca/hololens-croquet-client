using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class View 
{
    readonly ViewClient client;

    public View(CounterModel data)
    {
        this.client = new ViewClient(data);
    }

    public void Subscribe(string scope, string eventName, Action<System.Object> onEvent)
    {
        this.client.Subscribe(scope, eventName, onEvent);
    }

    public CounterModel getData()
    {
        return this.client.data;
    }



    public void Publish(string scope, string eventName, System.Object data)
    {
        this.client.Publish(scope, eventName, data);
    }

    public void Start()
    {
        this.client.Start();
    }
}
