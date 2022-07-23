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
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -Camera.main.transform.position.z;
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, whatIsTile);

            if (hit.collider == null)
            {
                if (heldCharacter != null)
                    Destroy(heldCharacter.gameObject);

                return;
            }

            Tile tile = hit.transform.GetComponent<Tile>();

            if (tile == null)
                return;

            if (HeldCharacter != null)
            {
                if (!tile.CanBePlaced(HeldCharacter.Character))
                    return;

                HeldCharacter.transform.position = hit.transform.position;
                tile.Character = HeldCharacter;
                CharacterManager.ActivateCharacter(HeldCharacter);
                HeldCharacter = null;
            }
            else
            {
                CharacterStats characterStats = tile.Character;

                if (characterStats == null)
                    return;

                tile.Character = null;
                HeldCharacter = characterStats;
                CharacterManager.DeactivateCharacter(characterStats.Character);
            }
        }
    }
}
