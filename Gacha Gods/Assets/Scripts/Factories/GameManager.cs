using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Managers/GameManager")]
public class GameManager : Factories.FactoryBase
{
    public static System.Action OnGameStart;
    public static System.Action OnGameEnd;

    public override void Initialize()
    {

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
