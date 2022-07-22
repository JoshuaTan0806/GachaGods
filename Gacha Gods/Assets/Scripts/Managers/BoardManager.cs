using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] Tile tilePrefab;
    [SerializeField] Vector2 startingPos;
    [SerializeField] int width;
    [SerializeField] int height;

    public Tile[,] Board => board;
    Tile[,] board;

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
 
}
