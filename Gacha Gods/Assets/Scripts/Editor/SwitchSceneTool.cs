using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SwitchSceneTool : EditorWindow
{
    [MenuItem("Tools/Scenes/Switch To Scene 1 &1")]
    public static void SwitchToScene1()
    {
        SwitchScene(1);
    }

    [MenuItem("Tools/Scenes/Switch To Scene 2 &2")]
    public static void SwitchToScene2()
    {
        SwitchScene(2);
    }

    [MenuItem("Tools/Scenes/Switch To Scene 3 &3")]
    public static void SwitchToScene3()
    {
        SwitchScene(3);
    }

    [MenuItem("Tools/Scenes/Switch To Scene 4 &4")]
    public static void SwitchToScene4()
    {
        SwitchScene(4);
    }

    [MenuItem("Tools/Scenes/Switch To Scene 5 &5")]
    public static void SwitchToScene5()
    {
        SwitchScene(5);
    }

    [MenuItem("Tools/Scenes/Switch To Scene 6 &6")]
    public static void SwitchToScene6()
    {
        SwitchScene(6);
    }

    [MenuItem("Tools/Scenes/Switch To Scene 7 &7")]
    public static void SwitchToScene7()
    {
        SwitchScene(7);
    }

    [MenuItem("Tools/Scenes/Switch To Scene 8 &8")]
    public static void SwitchToScene8()
    {
        SwitchScene(8);
    }

    [MenuItem("Tools/Scenes/Switch To Scene 9 &9")]
    public static void SwitchToScene9()
    {
        SwitchScene(9);
    }

    [MenuItem("Tools/Scenes/Switch To Scene 0 &0")]
    public static void SwitchToScene0()
    {
        SwitchScene(0);
    }

    static void SwitchScene(int i)
    {
        SceneHotswap sceneHotswap = EditorExtensionMethods.GetAllInstances<SceneHotswap>()[0];

        if (sceneHotswap == null)
        {
            Debug.LogError("Scene Hotswap does not exist.");
        }
        else
        {
            if (!File.Exists(sceneHotswap.SceneNames[i - 1]))
            {
                Debug.LogError("Scene Name does not exist.");
            }
            else
            {
                EditorSceneManager.OpenScene(sceneHotswap.SceneNames[i - 1]);
            }
        }
    }
}
