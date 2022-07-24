using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCharacterButton : MonoBehaviour
{
    Character character;

    private void Awake()
    {
        gameObject.AddListenerToButton(InstantiateCharacter);
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    void InstantiateCharacter()
    {
        if (!CharacterManager.CharacterMastery.ContainsKey(character))
            return;

        if (CharacterManager.ActiveCharacters.ContainsKey(character))
            return;

        if (CharacterManager.ActiveCharacters.Count == GameManager.Level)
            return;

        if (BoardManager.HeldCharacter != null)
            return;

        CharacterStats stats = Instantiate(character.Prefab).GetComponent<CharacterStats>();
        stats.InitialiseCharacter(character);
        CharacterManager.ActivateCharacter(stats);
        BoardManager.HeldCharacter = stats;
    }
}
