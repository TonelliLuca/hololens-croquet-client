using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModelData
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }


   
    [System.Serializable]
    public class ScoreModel
    {
        public string score;

        public static ScoreModel CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<ScoreModel>(jsonString);
        }
    }
}
