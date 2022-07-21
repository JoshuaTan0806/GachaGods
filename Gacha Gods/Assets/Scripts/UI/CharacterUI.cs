using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] GameObject CharacterHolder;
    Dictionary<Character, Image> CharacterDictionary;

    private void Awake()
    {
        CharacterDictionary = new Dictionary<Character, Image>();

        foreach (var item in CharacterManager.Characters)
        {
            Image g = Instantiate(PrefabManager.CharacterIcon, CharacterHolder.transform).GetComponent<Image>();
            g.sprite = item.Icon;
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
    }
}
