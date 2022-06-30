using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public Buff(StatData statData, ref System.Action conditionToRemove)
    {
        Stat = statData;

        conditionToRemove -= RemoveBuff;
        conditionToRemove += RemoveBuff;

        GameManager.OnGameEnd -= RemoveBuff;
        GameManager.OnGameEnd += RemoveBuff;
    }

    public Buff(StatData statData, ref List<System.Action> conditionsToRemove)
    {
        Stat = statData;

        for (int i = 0; i < conditionsToRemove.Count; i++)
        {
            conditionsToRemove[i] -= RemoveBuff;
            conditionsToRemove[i] += RemoveBuff;
        }

        GameManager.OnGameEnd -= RemoveBuff;
        GameManager.OnGameEnd += RemoveBuff;
    }

    public void RemoveBuff()
    {
        OnConditionHit?.Invoke(this);
    }

    public StatData Stat;
    public System.Action<Buff> OnConditionHit;
}
