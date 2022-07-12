using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum MasteryType
{
    Role,
    Archetype,
    Stat,
    GlobalStat,
    Attack,
    Spell
}

[CreateAssetMenu(menuName = "Character/Mastery")]
public class Mastery : ScriptableObject
{
    public MasteryType MasteryType;

    [ShowIf("MasteryType", MasteryType.Role), SerializeField]
    Role Role;

    [ShowIf("MasteryType", MasteryType.Archetype), SerializeField]
    Archetype Archetype;

    [ShowIf("MasteryType", MasteryType.Stat), SerializeField]
    StatData Stat;

    [ShowIf("MasteryType", MasteryType.GlobalStat), SerializeField]
    StatData GlobalStat;

    [ShowIf("MasteryType", MasteryType.Attack), SerializeField]
    GameObject AttackPrefab;

    [ShowIf("MasteryType", MasteryType.Spell), SerializeField]
    GameObject SpellPrefab;


    public void ActivateMastery(CharacterStats stats)
    {
        switch (MasteryType)
        {
            case MasteryType.Role:
                CharacterManager.AddRole(Role);
                break;
            case MasteryType.Archetype:
                CharacterManager.AddArchetype(Archetype);
                break;
            case MasteryType.Stat:
                stats.AddStat(Stat);
                break;
            case MasteryType.GlobalStat:
                CharacterManager.AddGlobalBuff(GlobalStat);
                break;
            case MasteryType.Attack:
                stats.UpgradeAttack(AttackPrefab);
                break;
            case MasteryType.Spell:
                stats.UpgradeSpell(SpellPrefab);
                break;
            default:
                break;
        }
    }

    public void DeactiveMastery(CharacterStats stats)
    {
        switch (MasteryType)
        {
            case MasteryType.Role:
                CharacterManager.RemoveRole(Role);
                break;
            case MasteryType.Archetype:
                CharacterManager.RemoveArchetype(Archetype);
                break;
            case MasteryType.Stat:
                stats.RemoveStat(Stat);
                break;
            case MasteryType.GlobalStat:
                CharacterManager.RemoveGlobalBuff(GlobalStat);
                break;
            case MasteryType.Attack:
                break;
            case MasteryType.Spell:
            default:
                break;
        }
    }
}
