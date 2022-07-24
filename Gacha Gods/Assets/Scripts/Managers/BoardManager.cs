using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] Tile tilePrefab;
    [SerializeField] Vector2 startingPos;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] LayerMask whatIsTile;
    public Tile[,] Board => board;
    Tile[,] board;

    public static CharacterStats HeldCharacter
    {
        get
        {
            return heldCharacter;
        }
        set
        {
            heldCharacter = value;
            OnHeldCharacterChanged?.Invoke();
        }
    }
    static CharacterStats heldCharacter;
    public static System.Action OnHeldCharacterChanged;

    private void Awake()
    {
        board = new Tile[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                board[i, j] = Instantiate(tilePrefab, new Vector3(startingPos.x + i, startingPos.y + j), Quaternion.identity, transform);

                if (i >= width / 2)
                {
                    if (i == width - 1)
                        board[i, j].SetUnitType(UnitType.Assassin);
                    else
                        board[i, j].SetUnitType(UnitType.None);
                }
                else
                {
                    if (i == 0)
                        board[i, j].SetUnitType(UnitType.None);
                    else
                        board[i, j].SetUnitType(UnitType.All);
                }
            }
        }
    }

    private void Update()
    {
        HeldCharacterFollowMouse();
        CheckForClick();
    }

    void HeldCharacterFollowMouse()
    {
        if (HeldCharacter != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -Camera.main.transform.position.z;
            HeldCharacter.transform.position = pos;
        }
    }

    void CheckForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Tile tile = RaycastTile();

            if (tile == null)
                return;

            if (HeldCharacter != null)
            {
                PlaceCharacter(tile);
            }
            else
            {
                PickUpCharacter(tile);
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            if (heldCharacter != null)
                return;

            Tile tile = RaycastTile();

            if (tile == null)
                return;

            RemoveCharacter(tile);
        }
    }

    void PickUpCharacter(Tile tile)
    {
        CharacterStats characterStats = tile.Character;

        if (characterStats == null)
            return;

        tile.Character = null;
        HeldCharacter = characterStats;
    }

    void PlaceCharacter(Tile tile)
    {
        if (!tile.CanBePlaced(HeldCharacter.Character))
            return;

        CharacterStats stats = tile.Character;

        HeldCharacter.transform.position = tile.transform.position;
        tile.Character = HeldCharacter;

        //pick up existing character
        HeldCharacter = stats;
    }

    void RemoveCharacter(Tile tile)
    {
        CharacterStats characterStats = tile.Character;

        if (characterStats == null)
            return;

        Destroy(tile.Character.gameObject);
        CharacterManager.DeactivateCharacter(characterStats.Character);
        HeldCharacter = null;
    }

    Tile RaycastTile()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -Camera.main.transform.position.z;
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, whatIsTile);

        if (hit.collider == null)
            return null;

        return hit.transform.GetComponent<Tile>();
    }
}
