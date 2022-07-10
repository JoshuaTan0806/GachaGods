using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Managers/Character Manager")]
public class CharacterManager : Factories.FactoryBase
{
    public static CharacterMastery CharacterMastery => characterMastery;
    static CharacterMastery characterMastery = new CharacterMastery();
    public static List<Character> ActiveCharacters => activeCharacters;
    static List<Character> activeCharacters;
    public static ActiveRoles ActiveRoles => activeRoles;
    static ActiveRoles activeRoles;
    public static ActiveArchetypes ActiveArchetypes => activeArchetypes;
    static ActiveArchetypes activeArchetypes;
    public static ActiveElements ActiveElements => activeElements;
    static ActiveElements activeElements;
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
        ActiveElements.Clear();
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
        if (activeCharacters.Contains(character))
            throw new System.Exception("Can't add a character which is already active");
        else
        {
            activeCharacters.Add(character);

            for (int i = 0; i < character.Role.Count; i++)
            {
                AddRole(character.Role[i]);
            }

            for (int i = 0; i < character.Element.Count; i++)
            {
                AddElement(character.Element[i]);
            }

            for (int i = 0; i < character.Archetype.Count; i++)
            {
                AddArchetype(character.Archetype[i]);
            }
        }
    }

    public static void DeactivateCharacter(Character character)
    {
        if (!activeCharacters.Contains(character))
            throw new System.Exception("Can't remove a character that is inactive");
        else
        {
            activeCharacters.Remove(character);

            for (int i = 0; i < character.Role.Count; i++)
            {
                RemoveRole(character.Role[i]);

            }

            for (int i = 0; i < character.Element.Count; i++)
            {
                RemoveElement(character.Element[i]);
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
            globalBuffs.Add(role.SetData[ActiveRoles[role]]);
    }

    public static void AddArchetype(Archetype archetype)
    {
        if (!ActiveArchetypes.ContainsKey(archetype))
            ActiveArchetypes.Add(archetype, 1);
        else
            ActiveArchetypes[archetype]++;

        if (archetype.SetData.ContainsKey(ActiveArchetypes[archetype]))
            globalBuffs.Add(archetype.SetData[ActiveArchetypes[archetype]]);
    }

    public static void AddElement(Element element)
    {
        if (!ActiveElements.ContainsKey(element))
            ActiveElements.Add(element, 1);
        else
            ActiveElements[element]++;

        if (element.SetData.ContainsKey(ActiveElements[element]))
            globalBuffs.Add(element.SetData[ActiveElements[element]]);
    }

    public static void RemoveRole(Role role)
    {
        if (role.SetData.ContainsKey(ActiveRoles[role]))
        {
            if (!globalBuffs.Contains(role.SetData[ActiveRoles[role]]))
                throw new System.Exception("Trying to remove a buff that has not been added");
            else
                globalBuffs.Remove(role.SetData[ActiveRoles[role]]);
        }

        if (!ActiveRoles.ContainsKey(role))
            throw new System.Exception("Trying to remove a role not in the dictionary");
        else
            ActiveRoles[role]--;
    }

    public static void RemoveArchetype(Archetype archetype)
    {
        if (archetype.SetData.ContainsKey(ActiveArchetypes[archetype]))
        {
            if (!globalBuffs.Contains(archetype.SetData[ActiveArchetypes[archetype]]))
                throw new System.Exception("Trying to remove a buff that has not been added");
            else
                globalBuffs.Remove(archetype.SetData[ActiveArchetypes[archetype]]);
        }

        if (!ActiveArchetypes.ContainsKey(archetype))
            throw new System.Exception("Trying to remove a archetype not in the dictionary");
        else
            ActiveArchetypes[archetype]--;
    }

    public static void RemoveElement(Element element)
    {
        if (element.SetData.ContainsKey(ActiveElements[element]))
        {
            if (!globalBuffs.Contains(element.SetData[ActiveElements[element]]))
                throw new System.Exception("Trying to remove a buff that has not been added");
            else
                globalBuffs.Remove(element.SetData[ActiveElements[element]]);
        }

        if (!ActiveElements.ContainsKey(element))
            throw new System.Exception("Trying to remove an element not in the dictionary");
        else
            ActiveElements[element]--;
    }



    public Character Character;

    [Button]
    public void ActivateTest()
    {
        ActivateCharacter(Character);
    }

    [Button]
    public void DeactivateTest()
    {
        DeactivateCharacter(Character);
    }
}

public class CharacterMastery : SerializableDictionary<Character, int> { }
public class ActiveElements : SerializableDictionary<Element, int> { }
public class ActiveRoles : SerializableDictionary<Role, int> { }
public class ActiveArchetypes : SerializableDictionary<Archetype, int> { }
public class SetData : SerializableDictionary<int, StatData> { }
