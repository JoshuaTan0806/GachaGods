using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public Buff(StatData statData, System.Action conditionToRemove)
    {
        Stat = statData;

        conditionToRemove -= RemoveBuff;
        conditionToRemove += RemoveBuff;
    }

    public void RemoveBuff()
    {
        OnConditionHit?.Invoke(this);
    }

    public StatData Stat;
    public System.Action<Buff> OnConditionHit;
}
