using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Role")]
public class Role : ScriptableObject
{
    public string Name => name;
    public string Description => description;
    [SerializeField] string description;
}
