using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Language/Keyword")]
public class Keyword : ScriptableObject
{
    public Translations Translations => translations;
    [SerializeField] Translations translations;
}
