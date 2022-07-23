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
    [SerializeField] Role Assassin;
    public CharacterStats Character;
    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        BoardManager.OnHeldCharacterChanged += SetTransparency;
    }

    private void OnDisable()
    {
        BoardManager.OnHeldCharacterChanged -= SetTransparency;
    }

    void SetTransparency()
    {
        if (BoardManager.HeldCharacter == null)
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        else
        {
            if (CanBePlaced(BoardManager.HeldCharacter.Character))
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            else
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.8f);
        }
    }

    public void SetUnitType(UnitType unitType)
    {
        this.unitType = unitType;
    }

    public bool CanBePlaced(Character character)
    {
        if (Character != null)
            return false;

        switch (unitType)
        {
            case UnitType.None:
                return false;
            case UnitType.All:
                return true;
            case UnitType.Assassin:
                return character.Role.Contains(Assassin);
            default:
                return false;
        }
    }
}
