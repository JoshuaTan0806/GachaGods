using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static System.Action OnGameStart;
    public static System.Action OnGameEnd;
    public static System.Action OnRoundStart;
    public static System.Action OnRoundEnd;

    public static int RoundNumber;

    public static int Level => level;
    static int level;
    public static int Gold => gold;
    static int gold;
    public static int Gems => gems;
    static int gems;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    [Button]
    public void StartGame()
    {
        OnGameStart?.Invoke();
        RoundNumber = 0;
        level = 3;
        gold = 10;
        gems = 0;
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
        RoundNumber++;
        OnRoundEnd?.Invoke();
    }

    public static void AddGold(int num)
    {
        gold += num;
    }

    public static void RemoveGold(int num)
    {
        gold -= num;
    }

    public static void AddGems(int num)
    {
        gems += num;
    }

    public static void RemoveGems(int num)
    {
        gems -= num;
    }
}
