using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Managers/Character Manager")]
public class CharacterManager : Factories.FactoryBase
{
    public static CharacterMastery CharacterMastery => characterMastery;
    static CharacterMastery characterMastery;

    public override void Initialise()
    {
        characterMastery = new CharacterMastery();
        GameManager.OnGameEnd -= Clear;
        GameManager.OnGameEnd += Clear;
    }

    void Clear()
    {
        CharacterMastery.Clear();
    }

    public static void AddCharacter(Character character)
    {
        if(CharacterMastery.ContainsKey(character))
        {
            if (CharacterMastery[character] < 5)
                CharacterMastery[character] += 1;
        }
        else
        {
            CharacterMastery.Add(Instantiate(character), 0);
        }
    }
}

public class CharacterMastery : SerializableDictionary<Character, int> { }
