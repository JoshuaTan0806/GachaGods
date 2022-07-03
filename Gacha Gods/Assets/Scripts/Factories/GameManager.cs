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
    public static System.Action OnRoundStart;
    public static System.Action OnRoundEnd;
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

    [Button]
    public void StartRound()
    {
        OnRoundStart?.Invoke();
    }

    [Button]
    public void EndRound()
    {
        OnRoundEnd?.Invoke();
    }
}
