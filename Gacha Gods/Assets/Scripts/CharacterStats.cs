using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public StatDictionary Stats => stats;
    StatDictionary stats = new StatDictionary();

    public Character Character => character;
    [SerializeField] Character character;
    void Start()
    {
        foreach (var item in StatManager.StatDictionary)
        {
            AddStat(item.Value);
        }

        foreach (var item in Character.BaseStats)
        {
            AddStat(item.Value);
        }
    }

    public void AddStat(StatData stat)
    {
        if(stats.ContainsKey(stat.stat))
        {
            stats[stat.stat] += stat;
        }
        else
        {
            stats.Add(stat.stat, StatManager.NullStat(stat.stat));
            stats[stat.stat] += stat;
        }
    }

    public void RemoveStat(StatData stat)
    {
        if (stats.ContainsKey(stat.stat))
        {
            stats[stat.stat] -= stat;
        }
        else
        {
            stats.Add(stat.stat, StatManager.NullStat(stat.stat));
            stats[stat.stat] -= stat;
        }
    }
}
