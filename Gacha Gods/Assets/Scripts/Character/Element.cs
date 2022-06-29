using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Element")]
public class Element : ScriptableObject
{
    public string Name => name;
    public string Description => description;
    [SerializeField] string description;
}
