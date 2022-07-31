using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData
{
    public List<CharacterData> CharacterDatas => characterDatas;
    List<CharacterData> characterDatas;
    public int RoundNumber => roundNumber;
    int roundNumber;

    public BoardData(int roundNumber, List<CharacterData> characterDatas)
    {
        this.roundNumber = roundNumber;
        this.characterDatas = characterDatas;
    }
}

public class CharacterData
{
    public Character Character => character;
    Character character;
    public Attack Attack => attack;
    Attack attack;
    public Spell Spell => spell;
    Spell spell;
    public StatDictionary Stats => stats;
    StatDictionary stats;
    public Vector2Int Position => position;
    Vector2Int position;

    public CharacterData(Character character, Attack attack, Spell spell, StatDictionary stats, Vector2Int position)
    {
        this.character = character;
        this.attack = attack;
        this.spell = spell;
        this.stats = stats;
        this.position = position;
    }
}
