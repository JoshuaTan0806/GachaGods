using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Archetype")]
public class Archetype : ScriptableObject
{
    public string Name => name;
    public string Description => description;
    [SerializeField] string description;
}
