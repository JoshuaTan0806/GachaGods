using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
