using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Managers/Gacha Manager")]
public class GachaManager : Factories.FactoryBase
{
    public static List<Character> Characters => characters;
    static List<Character> characters = new List<Character>();
    [SerializeField] List<Character> _characters;

    public static List<Role> Roles => roles;
    static List<Role> roles = new List<Role>();
    [SerializeField] List<Role> _roles;

    public static List<Archetype> Archetypes => archetypes;
    static List<Archetype> archetypes = new List<Archetype>();
    [SerializeField] List<Archetype> _archetypes;

    public static List<Element> Elements => elements;
    static List<Element> elements = new List<Element>();
    [SerializeField] List<Element> _elements;

    public static List<Rarity> Rarities => rarities;
    static List<Rarity> rarities = new List<Rarity>();
    [SerializeField] List<Rarity> _rarities;

    public static List<OddsDictionary> Odds => odds;
    static List<OddsDictionary> odds = new List<OddsDictionary>();
    List<int> _odds = new List<int>()
    {
        100, 0, 0, 0, 0,
        100, 0, 0, 0, 0,
        100, 0, 0, 0, 0,
        75, 25, 0, 0, 0,
        55, 30, 15, 0, 0,
        45, 33, 20, 2, 0,
        25, 40, 30, 5, 0,
        19, 30, 35, 15, 1,
        16, 20, 35, 15, 1,
        9, 15, 30, 30, 16,
        5, 10, 20, 40, 25
    };

    public override void Initialise()
    {
        characters = _characters;
        roles = _roles;
        archetypes = _archetypes;
        elements = _elements;
        rarities = _rarities.OrderBy(x => x.RarityNumber).ToList();
        InitialiseOdds();
    }

    public void InitialiseOdds()
    {
        odds.Clear();
        int counter = 0;

        for (int i = 0; i < 11; i++)
        {
            odds.Add(new OddsDictionary());

            for (int j = 0; j < _rarities.Count; j++)
            {
                odds[i].Add(_rarities[j], _odds[counter]);
                counter++;
            }
        }
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
        return roles.ChooseRandomElementInList();
    }

    public static Archetype RandomArchetype()
    {
        return archetypes.ChooseRandomElementInList();
    }

    public static Element RandomElement()
    {
        return elements.ChooseRandomElementInList();
    }

}
[System.Serializable] public class OddsDictionary : SerializableDictionary<Rarity, int> { }
