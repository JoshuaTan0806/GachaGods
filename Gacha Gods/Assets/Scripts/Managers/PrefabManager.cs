using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Prefab Manager")]
public class PrefabManager : Factories.FactoryBase
{
    public static GameObject StandardText;
    [SerializeField] GameObject standardText;
    public static GameObject Gacha;
    [SerializeField] GameObject gacha;
    public static GameObject CharacterOdds;
    [SerializeField] GameObject characterOdds;

    public override void Initialise()
    {
        StandardText = standardText;
        Gacha = gacha;
        CharacterOdds = characterOdds;
    }
}
