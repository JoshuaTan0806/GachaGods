using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterArt : MonoBehaviour
{
    [SerializeField] List<Image> rolesAndArchetypes;
    [SerializeField] Image rarity;

    public void Initialise(Character character)
    {
        return;

        rarity.sprite = character.Rarity.Icon;

        for (int i = 0; i < rolesAndArchetypes.Count; i++)
        {
            rolesAndArchetypes[i].gameObject.SafeSetActive(false);
        }

        int counter = 0;

        for (int i = 0; i < character.Role.Count; i++)
        {
            rolesAndArchetypes[counter].gameObject.SafeSetActive(true);
            rolesAndArchetypes[counter].sprite = character.Role[i].Icon;
            counter++;
        }

        for (int i = 0; i < character.Archetype.Count; i++)
        {
            rolesAndArchetypes[counter].gameObject.SafeSetActive(true);
            rolesAndArchetypes[counter].sprite = character.Archetype[i].Icon;
            counter++;
        }
    }
}
