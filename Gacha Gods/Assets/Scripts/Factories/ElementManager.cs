using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/ElementManager")]
public class ElementManager : Factories.FactoryBase
{
    public static List<Element> AllElements => allElements;
    static List<Element> allElements = new List<Element>();

    [SerializeField]
    List<Element> elements;

    public override void Initialize()
    {
        foreach (var item in elements)
        {
            allElements.Add(item);
        }
    }
}
