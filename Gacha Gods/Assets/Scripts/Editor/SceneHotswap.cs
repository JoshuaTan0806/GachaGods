using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Hotswap", menuName = "Instances/Scene Hotswap", order = 0)]
public class SceneHotswap : ScriptableObject 
{
    public static SceneHotswap instance;
    public string[] SceneNames = new string[10];

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Object already has an instance");
            DestroyImmediate(this, true);
        }
    }
}
