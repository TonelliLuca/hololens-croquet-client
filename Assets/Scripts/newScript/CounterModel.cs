using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CounterModel : ModelData
{
    //public int counter = 0;

    public float cubeX = 0;
    public float cubeY = 0;
    public float cubeZ = 0;

    public static CounterModel CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<CounterModel>(jsonString);
    }

    public CounterModel(float x, float y, float z)
    {
        this.cubeX = x;
        this.cubeY = y;
        this.cubeZ = z;
    }
}
