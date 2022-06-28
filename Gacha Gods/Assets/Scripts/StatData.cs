using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Character/Stat")]
public class StatData : ScriptableObject
{
    public Stat stat;

    [SerializeField] bool hasFlat;

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
        if (hasFlat)
            total = ((flat + (flat * (percent / 100))) * multiplier);
        else
            total = ((1 + (percent / 100)) * multiplier);
    }

    public static StatData operator+ (StatData l, StatData r)
    {
        if (l.stat != r.stat)
            throw new System.Exception("Stats are different");

        StatData stat = StatManager.NullStat(l.stat);

        stat.flat = l.flat + r.flat;
        stat.percent = l.percent + r.percent;
        stat.multiplier = l.multiplier * r.multiplier;

        return stat;
    }

    public static StatData operator - (StatData l, StatData r)
    {
        if (l.stat != r.stat)
            throw new System.Exception("Stats are different");

        StatData stat = StatManager.NullStat(l.stat);

        stat.flat = l.flat - r.flat;
        stat.percent = l.percent - r.percent;
        stat.multiplier = l.multiplier * 1 / r.multiplier;

        return stat;
    }
}

public class StatDictionary : SerializableDictionary<Stat, StatData> { }

public enum Stat
{
    Health
}

public enum StatType
{
    Flat,
    Percent,
    Multiplier
}