using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Managers/Character Manager")]
public class CharacterManager : Factories.FactoryBase
{
    public static CharacterMastery CharacterMastery => characterMastery;
    static CharacterMastery characterMastery = new CharacterMastery();
    public static ActiveCharacters ActiveCharacters => activeCharacters;
    static ActiveCharacters activeCharacters = new ActiveCharacters();
    public static ActiveRoles ActiveRoles => activeRoles;
    static ActiveRoles activeRoles;
    public static ActiveArchetypes ActiveArchetypes => activeArchetypes;
    static ActiveArchetypes activeArchetypes;
    public static List<StatData> GlobalBuffs => globalBuffs;
    static List<StatData> globalBuffs;

    public override void Initialise()
    {
        GameManager.OnGameEnd -= Clear;
        GameManager.OnGameEnd += Clear;
    }

    void Clear()
    {
        CharacterMastery.Clear();
        ActiveCharacters.Clear();
        ActiveRoles.Clear();
        ActiveArchetypes.Clear();
    }

    public static void AddCharacter(Character character)
    {
        if (CharacterMastery.ContainsKey(character))
        {
            if (CharacterMastery[character] < 5)
                CharacterMastery[character] += 1;
        }
        else
        {
            CharacterMastery.Add(Instantiate(character), 0);
        }
    }

    public static void ActivateCharacter(Character character)
    {
        if (activeCharacters.ContainsKey(character))
            throw new System.Exception("Can't add a character which is already active");
        else
        {
            CharacterStats characterStats = Instantiate(character.Prefab).GetComponent<CharacterStats>();
            AddAllGlobalBuffs(characterStats);

            activeCharacters.Add(character, characterStats);

            for (int i = 0; i < character.Role.Count; i++)
            {
                AddRole(character.Role[i]);
            }

            for (int i = 0; i < character.Archetype.Count; i++)
            {
                AddArchetype(character.Archetype[i]);
            }
        }
    }

    public static void DeactivateCharacter(Character character)
    {
        if (!activeCharacters.ContainsKey(character))
            throw new System.Exception("Can't remove a character that is inactive");
        else
        {
            activeCharacters.Remove(character);

            for (int i = 0; i < character.Role.Count; i++)
            {
                RemoveRole(character.Role[i]);

            }
    
            for (int i = 0; i < character.Archetype.Count; i++)
            {
                RemoveArchetype(character.Archetype[i]);
            }
        }
    }
    
    public static void AddRole(Role role)
    {
        if (!ActiveRoles.ContainsKey(role))
            ActiveRoles.Add(role, 1);
        else
            ActiveRoles[role]++;

        if (role.SetData.ContainsKey(ActiveRoles[role]))
            AddGlobalBuff(role.SetData[ActiveRoles[role]]);
    }

    public static void AddArchetype(Archetype archetype)
    {
        if (!ActiveArchetypes.ContainsKey(archetype))
            ActiveArchetypes.Add(archetype, 1);
        else
            ActiveArchetypes[archetype]++;

        if (archetype.SetData.ContainsKey(ActiveArchetypes[archetype]))
            AddGlobalBuff(archetype.SetData[ActiveArchetypes[archetype]]);
    }

    public static void RemoveRole(Role role)
    {
        if (role.SetData.ContainsKey(ActiveRoles[role]))
            RemoveGlobalBuff(role.SetData[activeRoles[role]]);

        if (!ActiveRoles.ContainsKey(role))
            throw new System.Exception("Trying to remove a role not in the dictionary");
        else
            ActiveRoles[role]--;
    }

    public static void RemoveArchetype(Archetype archetype)
    {
        if (archetype.SetData.ContainsKey(ActiveArchetypes[archetype]))
            RemoveGlobalBuff(archetype.SetData[ActiveArchetypes[archetype]]);

        if (!ActiveArchetypes.ContainsKey(archetype))
            throw new System.Exception("Trying to remove a archetype not in the dictionary");
        else
            ActiveArchetypes[archetype]--;
    }

    public static void AddGlobalBuff(StatData statData)
    {
        if (globalBuffs.Contains(statData))
            throw new System.Exception("Trying to add a buff that has already been added");
        else
            globalBuffs.Add(statData);

        foreach (var item in ActiveCharacters)
        {
            item.Value.AddStat(statData);
        }
    }

    public static void RemoveGlobalBuff(StatData statData)
    {
        if (!globalBuffs.Contains(statData))
            throw new System.Exception("Trying to remove a buff that has not been added");
        else
            globalBuffs.Remove(statData);

        foreach (var item in ActiveCharacters)
        {
            item.Value.RemoveStat(statData);
        }
    }

    public static void AddAllGlobalBuffs(CharacterStats characterStats)
    {
        foreach (var item in globalBuffs)
        {
            characterStats.AddStat(item);
        }
    }
}

public class ActiveCharacters : SerializableDictionary<Character, CharacterStats> { }
public class CharacterMastery : SerializableDictionary<Character, int> { }
public class ActiveRoles : SerializableDictionary<Role, int> { }
public class ActiveArchetypes : SerializableDictionary<Archetype, int> { }
public class SetData : SerializableDictionary<int, StatData> { }
