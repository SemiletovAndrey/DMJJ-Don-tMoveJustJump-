using UnityEngine;

public static class DataExtension
{
    public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

    public static string ToJson(this object obj) =>
        JsonUtility.ToJson(obj);

    public static Vector3Serial AsVectorSeril(this Vector3 vector) =>
            new Vector3Serial(vector.x, vector.y, vector.z);

    public static Vector3 AsUnityVector(this Vector3Serial vector3Data) =>
        new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);

    public static Vector3 AddY(this Vector3 vector, float y)
    {
        vector.y += y;
        return vector;
    }


}
