using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

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
    public void SaveBoardData()
    {
        string path = "Assets/BoardDatabase/Round " + GameManager.RoundNumber + "/";
        string name;

        if (BoardDatabase.Database.ContainsKey(GameManager.RoundNumber))
        {
            name = "Round " + GameManager.RoundNumber + " ID " + (BoardDatabase.Database[GameManager.RoundNumber].Count + 1).ToString();
        }
        else
        {
            name = "Round " + GameManager.RoundNumber + " ID 0";
        }

        if (!File.Exists(path + name))
        {
            BoardData boardData = ScriptableObject.CreateInstance<BoardData>();
            AssetDatabase.CreateAsset(boardData, path + name);
            boardData.name = name;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(board[i,j].Character != null)
                    {
                        CharacterStats character = board[i, j].Character;
                        CharacterData data = new CharacterData(character.Character, character.Stats, character.Attack, character.Spell, new Vector2Int(i, j));
                        boardData.AddCharacter(data);
                    }
                }
            }

            BoardDatabase.SaveBoard(boardData, GameManager.RoundNumber);

            EditorExtensionMethods.SaveAsset(boardData);
        }
    }

    [Button]
    public void LoadRandomBoardData()
    {
        BoardData boardData = BoardDatabase.LoadRandomBoardData(GameManager.RoundNumber);

        for (int i = 0; i < boardData.CharacterDatas.Count; i++)
        {
            CharacterData characterData = boardData.CharacterDatas[i];

            Vector3 spawnPos = Board[width - characterData.Position.x, characterData.Position.y].transform.position;

            CharacterStats g = Instantiate(characterData.Character.Prefab, spawnPos, Quaternion.identity).GetComponent<CharacterStats>();
            g.UpgradeAttack(characterData.Attack);
            g.UpgradeSpell(characterData.Spell);
            g.SetStats(characterData.Stats);
        }
    }
}