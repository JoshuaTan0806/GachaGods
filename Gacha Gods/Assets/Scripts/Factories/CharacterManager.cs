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

    public override void Initialize()
    {
        foreach (var item in characters)
        {
            allCharacters.Add(item);
        }
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
