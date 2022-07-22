using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    None,
    All,
    Assassin
}

public class Tile : MonoBehaviour
{
    public UnitType UnitType => unitType;
    [SerializeField] UnitType unitType;

    public Character Character;

    public void SetUnitType(UnitType unitType)
    {
        this.unitType = unitType;
    }
}
