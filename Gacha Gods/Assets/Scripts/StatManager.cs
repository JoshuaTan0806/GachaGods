using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    public static StatDictionary StatDictionary => statDictionary;
    static StatDictionary statDictionary = new StatDictionary();

    [SerializeField]
    List<StatData> stats;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        for (int i = 0; i < stats.Count; i++)
        {
            if (!StatDictionary.ContainsKey(stats[i].stat))
                statDictionary.Add(stats[i].stat, stats[i]);
        }
    }

    public static StatData NullStat(Stat stat)
    {
        return Instantiate(statDictionary[stat]);
    }

    public static StatData NewStat(Stat stat, StatType statType, float value)
    {
        StatData newStat = NullStat(stat);

        switch (statType)
        {
            case StatType.Flat:
                newStat.Flat += value;
                break;
            case StatType.Percent:
                newStat.Percent += value;
                break;
            case StatType.Multiplier:
                if (value <= 0)
                    Debug.LogError("Final multiplier should not be less than or equal to 0");
                newStat.Multiplier *= value;
                break;
            default:
                break;
        }
   
        return newStat;
    }
}
