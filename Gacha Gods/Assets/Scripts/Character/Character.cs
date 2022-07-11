using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Character")]
public class Character : ScriptableObject
{
    public GameObject Prefab => prefab;
    [SerializeField] GameObject prefab;
    public Sprite Icon => icon;
    [SerializeField] Sprite icon;
    public List<Role> Role => role;
    [SerializeField] List<Role> role;
    public List<Archetype> Archetype => archetype;
    [SerializeField] List<Archetype> archetype;
    public Rarity Rarity => rarity;
    [SerializeField] Rarity rarity;
    public StatFloatDictionary BaseStats => baseStats;
    [SerializeField] StatFloatDictionary baseStats;
    public List<Mastery> Mastery => mastery;
    [SerializeField] List<Mastery> mastery;
}

