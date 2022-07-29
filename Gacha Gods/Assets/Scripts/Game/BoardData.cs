using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData : ScriptableObject
{
    public List<CharacterData> CharacterDatas => characterDatas;
    [ReadOnly] List<CharacterData> characterDatas = new List<CharacterData>();

    public void AddCharacter(CharacterData characterData)
    {
        characterDatas.Add(characterData);
    }    
}

[System.Serializable]
public class CharacterData
{
    public Character Character => character;
    Character character;
    public StatDictionary Stats => stats;
    StatDictionary stats;
    public Attack Attack => attack;
    Attack attack;
    public Spell Spell => spell;
    Spell spell;
    public Vector2 Position => position;
    Vector2 position;

    public CharacterData(Character character, StatDictionary stats, Attack attack, Spell spell, Vector2 position)
    {
        this.character = character;
        this.stats = stats;
        this.attack = attack;
        this.spell = spell;
        this.position = position;
    }
}
