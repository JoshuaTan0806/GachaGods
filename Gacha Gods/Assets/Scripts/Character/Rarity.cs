using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Rarity")]
public class Rarity : ScriptableObject
{
    public string Name => _name;
    [SerializeField] string _name;
    public int RarityNumber => rarityNumber;
    [SerializeField] int rarityNumber;
    public Gradient Gradient => gradient;
    [SerializeField] Gradient gradient;
    public Sprite Icon => icon;
    [SerializeField] Sprite icon;
}
