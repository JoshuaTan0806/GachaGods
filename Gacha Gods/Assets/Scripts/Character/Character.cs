using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Character")]
public class Character : ScriptableObject
{
    public GameObject Prefab => prefab;
    [SerializeField] GameObject prefab;
    public List<Role> Role => role;
    [SerializeField] List<Role> role;
    public List<Archetype> Archetype => archetype;
    [SerializeField] List<Archetype> archetype;
    public List<Element> Element => element;
    [SerializeField] List<Element> element;
    public Rarity Rarity => rarity;
    [SerializeField] Rarity rarity;
    public StatFloatDictionary BaseStats => baseStats;
    [SerializeField] StatFloatDictionary baseStats;
    public List<StatData> Mastery => mastery;
    [SerializeField] List<StatData> mastery;
}

