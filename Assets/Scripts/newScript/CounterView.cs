using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CounterView : View
{
    private CounterModel data;
    public CounterView(CounterModel data) : base(data)
    {
        //base.Subscribe("cube", "updated", (data) => OnCubeUpdated(data));
        this.data = data;
    }

    

    private void OnCubeUpdated(System.Object tmp)
    {
        CounterModel tmpC = (CounterModel)tmp;
        this.data.cubeX = tmpC.cubeX;
        this.data.cubeY = tmpC.cubeY;
        this.data.cubeZ = tmpC.cubeZ;

        

    }

    public void SendData(CounterModel newData)
    {
        base.Publish("cube", "sendData", newData);
    }
}
