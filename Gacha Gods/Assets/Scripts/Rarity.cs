using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Rarity")]
public class Rarity : ScriptableObject
{
    public string Name => name;
    public string Description => description;
    [SerializeField] string description;
}
