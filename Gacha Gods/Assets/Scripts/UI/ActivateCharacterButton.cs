using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCharacterButton : MonoBehaviour
{
    Character character;

    private void Awake()
    {
        gameObject.AddListenerToButton(ActivateCharacter);
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    void ActivateCharacter()
    {
        if (!CharacterManager.CharacterMastery.ContainsKey(character))
            return;

        CharacterManager.ActivateCharacter(character);
    }
}
