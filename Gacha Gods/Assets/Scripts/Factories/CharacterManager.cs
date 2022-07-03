using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Managers/CharacterManager")]
public class CharacterManager : Factories.FactoryBase
{
    public static List<Character> AllCharacters => allCharacters;
    static List<Character> allCharacters = new List<Character>();
    [SerializeField]
    List<Character> characters;

    public static List<Role> AllRoles => allRoles;
    static List<Role> allRoles = new List<Role>();
    [SerializeField]
    List<Role> roles;

    public static List<Archetype> AllArchetypes => allArchetypes;
    static List<Archetype> allArchetypes = new List<Archetype>();
    [SerializeField]
    List<Archetype> archetype;

    public static List<Element> AllElements => allElements;
    static List<Element> allElements = new List<Element>();
    [SerializeField]
    List<Element> elements;

    public static List<Rarity> AllRarities => allRarities;
    static List<Rarity> allRarities = new List<Rarity>();
    [SerializeField]
    List<Rarity> rarities;

    public override void Initialize()
    {
        foreach (var item in characters)
        {
            allCharacters.Add(item);
        }

        foreach (var item in roles)
        {
            allRoles.Add(item);
        }

        foreach (var item in archetype)
        {
            allArchetypes.Add(item);
        }

        foreach (var item in elements)
        {
            allElements.Add(item);
        }

        foreach (var item in rarities)
        {
            allRarities.Add(item);
        }

        allRarities = allRarities.OrderBy(x => x.RarityNumber).ToList();
    }

    public List<Character> FilterCharacters(Rarity rarity)
    {
        return allCharacters.Where(x => x.Rarity == rarity).ToList();
    }

    public List<Character> FilterCharacters(Role role)
    {
        return allCharacters.Where(x => x.Role.Contains(role)).ToList();
    }

    public List<Character> FilterCharacters(Archetype archetype)
    {
        return allCharacters.Where(x => x.Archetype.Contains(archetype)).ToList();
    }

    public List<Character> FilterCharacters(Element element)
    {
        return allCharacters.Where(x => x.Element.Contains(element)).ToList();
    }
}
