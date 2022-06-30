using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Managers/RarityManager")]
public class RarityManager : Factories.FactoryBase
{
    public static List<Rarity> AllRarities => allRarities;
    static List<Rarity> allRarities = new List<Rarity>();

    [SerializeField]
    List<Rarity> rarities;

    public override void Initialize()
    {
        foreach (var item in rarities)
        {
            allRarities.Add(item);
        }

        allRarities = allRarities.OrderBy(x => x.RarityNumber).ToList();
    }
}
