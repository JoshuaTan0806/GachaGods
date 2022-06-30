using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Character/Stat")]
public class StatData : ScriptableObject
{
    public Stat stat;

    public bool FlatDefaultsToOne => flatDefaultsToOne;
    [SerializeField] bool flatDefaultsToOne;

    public float Flat
    {
        get
        {
            return flat;
        }
        set
        {
            flat = value;
            CalculateTotal();
        }
    }
    [SerializeField] float flat;

    public float Percent
    {
        get
        {
            return percent;
        }
        set
        {
            percent = value;
            CalculateTotal();
        }
    }
    [SerializeField] float percent;

    public float Multiplier
    {
        get
        {
            return multiplier;
        }
        set
        {
            multiplier = value;
            CalculateTotal();
        }
    }
    [SerializeField]  float multiplier = 1;

    public float Total => total;
    [ReadOnly, SerializeField] float total;

    private void OnValidate()
    {
        CalculateTotal();
    }

    public void CalculateTotal()
    {
        if (!flatDefaultsToOne)
            total = ((flat + (flat * (percent / 100))) * multiplier);
        else
            total = ((1 + (percent / 100)) * multiplier);
    }

    public static StatData operator+ (StatData l, StatData r)
    {
        if (l.stat != r.stat)
            throw new System.Exception("Stats are different");

        StatData stat = StatManager.NullStat(l.stat);

        stat.Flat = l.Flat + r.Flat;
        stat.Percent = l.Percent + r.Percent;
        stat.Multiplier = l.Multiplier * r.Multiplier;

        return stat;
    }

    public static StatData operator - (StatData l, StatData r)
    {
        if (l.stat != r.stat)
            throw new System.Exception("Stats are different");

        StatData stat = StatManager.NullStat(l.stat);

        stat.Flat = l.Flat - r.Flat;
        stat.Percent = l.Percent - r.Percent;
        stat.Multiplier = l.Multiplier * 1 / r.Multiplier;

        return stat;
    }
}

[System.Serializable] public class StatDictionary : SerializableDictionary<Stat, StatData> { }
[System.Serializable] public class StatFloatDictionary : SerializableDictionary<Stat, float> { }

public enum Stat
{
    Health,
    Range
}

public enum StatType
{
    Flat,
    Percent,
    Multiplier
}