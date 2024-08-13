using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataExtension
{
    public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

    public static string ToJson(this object obj) =>
        JsonUtility.ToJson(obj);
}
