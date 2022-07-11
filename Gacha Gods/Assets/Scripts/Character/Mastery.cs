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
    Skill
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
            case MasteryType.Skill:
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
            case MasteryType.Skill:
                break;
            default:
                break;
        }
    }
}
