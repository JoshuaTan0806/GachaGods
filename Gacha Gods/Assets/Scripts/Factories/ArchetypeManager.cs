using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/ArchetypeManager")]
public class ArchetypeManager : Factories.FactoryBase
{
    public static List<Archetype> AllArchetypes => allArchetypes;
    static List<Archetype> allArchetypes = new List<Archetype>();

    [SerializeField]
    List<Archetype> archetype;

    public override void Initialize()
    {
        foreach (var item in archetype)
        {
            allArchetypes.Add(item);
        }
    }
}
