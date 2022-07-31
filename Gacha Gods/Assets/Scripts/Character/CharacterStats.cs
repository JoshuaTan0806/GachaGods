using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterStats : MonoBehaviour
{
    public StatDictionary Stats => stats;
    StatDictionary stats = new StatDictionary();
    [ReadOnly, SerializeField] StatFloatDictionary totalStats = new StatFloatDictionary();
    public System.Action OnStatsChanged;
    public Character Character => character;
    [SerializeField] Character character;

    List<Buff> buffs = new List<Buff>();
    public System.Action OnDeath;

    public Attack Attack => attack;
    Attack attack;
    public Spell Spell => spell;
    Spell spell;

    float currentHealth;
    float currentMana;

    void Start()
    {
        foreach (var item in StatManager.StatDictionary)
        {
            AddStat(item.Value);
        }

        foreach (var item in Character.BaseStats)
        {
            AddStat(StatManager.CreateStat(item.Key, StatType.Flat, item.Value));
        }
    }

    public void InitialiseCharacter(Character character)
    {
        this.character = character;
        spell = character.Spell;
        attack = character.Attack;
    }

    public void SetStats(StatDictionary stats)
    {
        this.stats = stats;
    }

    public float GetStat(Stat stat)
    {
        return stats.ContainsKey(stat) ? stats[stat].Total : StatManager.NullStat(stat).Total;
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

        totalStats[stat.stat] = stats[stat.stat].Total;

        OnStatsChanged?.Invoke();
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

        totalStats[stat.stat] = stats[stat.stat].Total;

        OnStatsChanged?.Invoke();
    }

    public bool IsDead()
    {
        return currentHealth > 0;
    }

    public void UpgradeAttack(Attack attack)
    {
        this.attack = attack;
    }

    public void UpgradeSpell(Spell spell)
    {
        this.spell = spell;
    }

    public void AddBuff(Buff buff)
    {
        if(buffs.Contains(buff))
            throw new System.Exception("Trying to add buff that character already has.");

        buffs.Add(buff);
        AddStat(buff.Stat);

        buff.OnConditionHit -= RemoveBuff;
        buff.OnConditionHit += RemoveBuff;
    }

    void RemoveBuff(Buff buff)
    {
        if (!buffs.Contains(buff))
            throw new System.Exception("Trying to remove buff that character does not have.");

        buffs.Remove(buff);
        RemoveStat(buff.Stat);
        buff.OnConditionHit -= RemoveBuff;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead())
            return;

        currentHealth -= damage;

        if (IsDead())
            OnDeath?.Invoke();
    }

    [Button]
    public void RandomiseColour()
    {
        Color color = new Vector4(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
        GetComponent<SpriteRenderer>().color = color;
    }

    [Button]
    public void Buff()
    {
        Buff buff = new(StatManager.CreateStat(Stat.Health, StatType.Flat, 100), ref GameManager.OnRoundEnd);
        AddBuff(buff);
    }
}
