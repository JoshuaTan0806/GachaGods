using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Sirenix.OdinInspector;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] GameObject UIBackground;
    [SerializeField] GameObject CharacterHolder;
    Dictionary<Character, Image> CharacterDictionary;

    private void Awake()
    {
        CharacterDictionary = new Dictionary<Character, Image>();

        foreach (var item in CharacterManager.Characters)
        {
            Image g = Instantiate(PrefabManager.CharacterIcon, CharacterHolder.transform).GetComponent<Image>();
            g.name = item.name;
            g.sprite = item.Icon;
            g.GetComponent<ActivateCharacterButton>().SetCharacter(item);
            CharacterDictionary.Add(item, g);
        }
    }

    private void OnEnable()
    {
        foreach (var item in CharacterDictionary)
        {
            if (CharacterManager.CharacterMastery.ContainsKey(item.Key))
                item.Value.SetTransparency(1);
            else
                item.Value.SetTransparency(0.5f);
        }

        BoardManager.OnHeldCharacterChanged += ToggleUI;
    }

    private void OnDisable()
    {
        BoardManager.OnHeldCharacterChanged -= ToggleUI;
    }

    private void Sort(List<Character> characters)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (!CharacterDictionary.ContainsKey(characters[i]))
                throw new System.Exception("Character not in dictionary.");
            else
                CharacterDictionary[characters[i]].transform.SetSiblingIndex(i);
        }
    }

    void ToggleUI()
    {
        if (BoardManager.HeldCharacter != null)
            UIBackground.SetActive(false);
        else
            UIBackground.SetActive(true);
    }
}
