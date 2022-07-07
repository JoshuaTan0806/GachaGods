using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Language/Translation")]
public class Translation: ScriptableObject
{
    public Translations Translations => translations;
    [SerializeField] Translations translations;
}

[System.Serializable] public class Translations : SerializableDictionary<Language, string> { }
