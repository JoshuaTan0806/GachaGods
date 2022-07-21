using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Prefab Manager")]
public class PrefabManager : Factories.FactoryBase
{
    public static GameObject StandardText => standardText;
    static GameObject standardText;
    [SerializeField] GameObject _standardText;
    public static GameObject Gacha => gacha;
    static GameObject gacha;
    [SerializeField] GameObject _gacha;
    public static GameObject CharacterOdds => characterOdds;
    static GameObject characterOdds;
    [SerializeField] GameObject _characterOdds;
    public static GameObject CharacterIcon => characterIcon;
    static GameObject characterIcon;
    [SerializeField] GameObject _characterIcon;

    public override void Initialise()
    {
        standardText = _standardText;
        gacha = _gacha;
        characterOdds = _characterOdds;
        characterIcon = _characterIcon;
    }
}
