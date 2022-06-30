using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Managers/GameManager")]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static System.Action OnGameStart;
    public static System.Action OnGameEnd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    [Button]
    public void StartGame()
    {
        OnGameStart?.Invoke();
    }

    [Button]
    public void EndGame()
    {
        OnGameEnd?.Invoke();
    }
}
