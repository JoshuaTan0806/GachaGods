using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] Tile tilePrefab;
    [SerializeField] Vector2 startingPos;
    [SerializeField] int width;
    [SerializeField] int height;

    static LayerMask WhatIsTile;
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
        WhatIsTile = whatIsTile;
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

    static void PickUpCharacter(Tile tile)
    {
        CharacterStats characterStats = tile.Character;

        if (characterStats == null)
            return;

        tile.Character = null;
        HeldCharacter = characterStats;
    }

    static void PlaceCharacter(Tile tile)
    {
        if (!tile.CanBePlaced(HeldCharacter.Character))
            return;

        CharacterStats stats = tile.Character;

        HeldCharacter.transform.position = tile.transform.position;
        tile.Character = HeldCharacter;

        //pick up existing character
        HeldCharacter = stats;
    }

    static void RemoveCharacter(Tile tile)
    {
        CharacterStats characterStats = tile.Character;

        if (characterStats == null)
            return;

        Destroy(tile.Character.gameObject);
        CharacterManager.DeactivateCharacter(characterStats.Character);
        HeldCharacter = null;
    }

    static Tile RaycastTile()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -Camera.main.transform.position.z;
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, WhatIsTile);

        if (hit.collider == null)
            return null;

        return hit.transform.GetComponent<Tile>();
    }

    [Button]
    public void LoadBoardData()
    {
        BoardData boardData = BoardDatabase.LoadBoard(GameManager.RoundNumber);

        foreach (var item in boardData.CharacterDatas)
        {
            CharacterStats stats = Instantiate(item.Character.Prefab, Board[width - 1 - item.Position.x, item.Position.y].transform).GetComponent<CharacterStats>();
            stats.SetStats(item.Stats);
            stats.UpgradeAttack(item.Attack);
            stats.UpgradeSpell(item.Spell);
        }
    }

    [Button]
    public void SaveBoardData()
    {
        List<CharacterData> characterDatas = new List<CharacterData>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (board[i, j].Character == null)
                    continue;

                CharacterStats character = board[i, j].Character;

                CharacterData characterData = new CharacterData(character.Character, character.Attack, character.Spell, character.Stats, new Vector2Int(i, j));
                characterDatas.Add(characterData);
            }
        }

        BoardData boardData = new BoardData(GameManager.RoundNumber, characterDatas);
        BoardDatabase.SaveBoard(boardData);
    }
}
