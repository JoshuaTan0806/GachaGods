using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;

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
    public Attack Attack => Attack;
    [SerializeField] Attack attack;
    public Spell Spell => Spell;
    [SerializeField] Spell spell;

    [Button]
    void CreateScriptables()
    {
        string path = AssetDatabase.GetAssetPath(this);
        int lastSlash = 0;

        for (int i = 0; i < path.Length; i++)
        {
            if (path[i] == '/')
                lastSlash = i;
        }

        path = path.Remove(lastSlash + 1); 

        if(!File.Exists(path + name + " Attack.asset"))
        {
            Attack attack = CreateInstance<Attack>();
            AssetDatabase.CreateAsset(attack, path + name + " Attack.asset");
            this.attack = attack;
        }

        if (!File.Exists(path + name + " Spell.asset"))
        {
            Spell spell = CreateInstance<Spell>();
            AssetDatabase.CreateAsset(spell, path + name + " Spell.asset");
            this.spell = spell;
        }

        for (int i = 0; i < 6; i++)
        {
            if (!File.Exists(path + name + " Mastery " + (i+1) + ".asset"))
            {
                Mastery mastery = CreateInstance<Mastery>();
                AssetDatabase.CreateAsset(mastery, path + name + " Mastery " + (i + 1) + ".asset");
                this.mastery.Add(mastery);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}