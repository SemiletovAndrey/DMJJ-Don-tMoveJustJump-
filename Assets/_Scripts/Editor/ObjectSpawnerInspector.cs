using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectSpawner))]
public class ObjectSpawnerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ObjectSpawner spawner = (ObjectSpawner)target;

        if (GUILayout.Button("Spawn Objects"))
        {
            spawner.SpawnObjects();
        }
    }
}
