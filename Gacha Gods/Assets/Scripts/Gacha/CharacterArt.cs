using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterArt : MonoBehaviour
{
    [SerializeField] List<Image> roles;
    [SerializeField] List<Image> archetypes;
    [SerializeField] List<Image> elements;
    [SerializeField] Image rarity;


    public void Initialise(Character character)
    {
        return;

        rarity.sprite = character.Rarity.Icon;

        for (int i = 0; i < character.Role.Count; i++)
        {
            roles[i].sprite = character.Role[i].Icon;
        }

        for (int i = 0; i < character.Archetype.Count; i++)
        {
            roles[i].sprite = character.Archetype[i].Icon;
        }

        for (int i = 0; i < character.Element.Count; i++)
        {
            roles[i].sprite = character.Element[i].Icon;
        }
    }
}
