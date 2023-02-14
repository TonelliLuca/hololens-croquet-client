using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text.RegularExpressions;
using System.Text;

public abstract class ProxyClient
{
    private TcpClient socketConnection;
    private Thread connectionThread;
    private Thread writeThread;
    private NetworkStream stream;



    public ProxyClient()
    {
        try
        {
            this.socketConnection = new TcpClient("192.168.40.100", 10000);
            this.stream = socketConnection.GetStream();
            Debug.Log("Connection established");

            this.connectionThread = new Thread(new ThreadStart(ListenData));
            this.connectionThread.IsBackground = true;
            this.connectionThread.Start();

           
            
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void ListenData()
    {
        try
        {

            Debug.Log("Listen data started");
            Byte[] bytes = new Byte[4096];
            
            while (true)
            {
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    
                    int length;
                    stream.Flush();
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {

                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);

                        string msgString = Encoding.ASCII.GetString(incommingData);

                        this.AnalyzeString(msgString);

                    }



                }
                
            }




        }
        catch (SocketException e)
        {
            Debug.Log(e);
        }
    }
    private void AnalyzeString(string val)
    {
        
        Debug.Log("val: " + val);
        string[] str = val.Split('\n');
        foreach (string splitted in str)
        {
            if (splitted.Length > 0)
            {
                Message message = Message.CreateFromJSON(splitted);
                switch (message.action)
                {
                    case "test":
                        Debug.Log("test");
                        break;
                    case "connection-ready":
                        this.Connect();
                        break;
                    case "ready":
                        this.OnReady();
                        break;
                    case "data":
                        Debug.Log("data");
                        this.OnData(message.data);
                        break;
                    case "event":
                        this.OnEvent(message.scope, message.eventName, message.data);
                        break;

                }

            }
        
        
        }
        
            


        
    }

    /*
    private void SendData() 
    {
        
            if (stream.CanWrite)
            {
                string clientMessage = "{\"event\":\"sub\"}";
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                stream.WriteAsync(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
            }
            
        
    }
    */
    public void Subscribe(string scope, string eventName)
    {
        if (stream.CanWrite)
        {
            string clientMessage = "{\"action\":\"subscribe\", \"event\":\"" + eventName + " \", \"scope\":\"" + scope + " \"}" + '\n';
            byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
            stream.WriteAsync(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
        }
    }

    public void Publish(string scope, string eventName, System.Object data)
    {
        if (stream.CanWrite)
        {
            string clientMessage = "{\"action\":\"publish\", \"event\":\"" + eventName + " \", \"scope\":\"" + scope + " \", \"data\":" + JsonUtility.ToJson(data) + " }"+'\n';
            byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
            stream.WriteAsync(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
            
        }

    }

    public void Connect()
    {
        if (stream.CanWrite)
        {
            string clientMessage = "{\"action\":\"join\"}"+'\n';
            byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
            stream.WriteAsync(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
        }
    }

    public abstract void OnReady();
    public abstract void OnData(CounterModel jsonData);
    public abstract void OnDataUpdate(string jsonPatch);
    public abstract void OnEvent(string scope, string eventName, System.Object data);




    }

[System.Serializable]
public class Message
{
    public CounterModel data;
    public string eventName = "";
    public string scope = "";
    public string action = "";

    public static Message CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Message>(jsonString);
    }

   
}


