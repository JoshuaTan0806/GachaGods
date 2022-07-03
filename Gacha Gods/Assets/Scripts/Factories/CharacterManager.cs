using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

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
    List<Archetype> archetypes;

    public static List<Element> AllElements => allElements;
    static List<Element> allElements = new List<Element>();
    [SerializeField]
    List<Element> elements;

    public static List<Rarity> AllRarities => allRarities;
    static List<Rarity> allRarities = new List<Rarity>();
    [SerializeField]
    List<Rarity> rarities;


    public static List<OddsDictionary> AllOdds => allOdds;
    static List<OddsDictionary> allOdds;
    [SerializeField] List<OddsDictionary> odds;

    //[Button]
    //public void InitialiseDictionary()
    //{
    //    odds.Clear();

    //    for (int i = 0; i < 11; i++)
    //    {
    //        odds.Add(new OddsDictionary());

    //        for (int j = 0; j < rarities.Count; j++)
    //        {
    //            odds[i].Add(rarities[j], 0);
    //        }
    //    }
    //}

    public override void Initialize()
    {
        allCharacters = characters;
        allRoles = roles;
        allArchetypes = archetypes;
        allElements = elements;
        allRarities = rarities.OrderBy(x => x.RarityNumber).ToList();
        allOdds = odds;
    }

    public static List<Character> FilterCharacters(List<Character> characters, Rarity rarity)
    {
        return characters.Where(x => x.Rarity == rarity).ToList();
    }

    public static List<Character> FilterCharacters(List<Character> characters, Role role)
    {
        return characters.Where(x => x.Role.Contains(role)).ToList();
    }

    public static List<Character> FilterCharacters(List<Character> characters, Archetype archetype)
    {
        return characters.Where(x => x.Archetype.Contains(archetype)).ToList();
    }

    public static List<Character> FilterCharacters(List<Character> characters, Element element)
    {
        return characters.Where(x => x.Element.Contains(element)).ToList();
    }

    public static Role RandomRole()
    {
        return allRoles.ChooseRandomElementInList();
    }

    public static Archetype RandomArchetype()
    {
        return allArchetypes.ChooseRandomElementInList();
    }

    public static Element RandomElement()
    {
        return allElements.ChooseRandomElementInList();
    }

}
[System.Serializable] public class OddsDictionary : SerializableDictionary<Rarity, int> { }
