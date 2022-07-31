using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/BoardDatabase")]
public class BoardDatabase : Factories.FactoryBase
{
    public static Dictionary<int, List<BoardData>> Database => database;
    static Dictionary<int, List<BoardData>> database = new Dictionary<int, List<BoardData>>();

    public override void Initialise()
    {
    }

    public static void SaveBoard(BoardData boardData)
    {
        if (database.ContainsKey(boardData.RoundNumber))
            database[boardData.RoundNumber].Add(boardData);
        else
        {
            List<BoardData> boardDatas = new List<BoardData>();
            boardDatas.Add(boardData);
            database.Add(boardData.RoundNumber, boardDatas);
        }
    }

    public static BoardData LoadBoard(int roundNumber)
    {
        if (!database.ContainsKey(roundNumber))
            throw new System.Exception("Database for round " + roundNumber + " is empty.");

        if (database[roundNumber].IsNullOrEmpty())
            throw new System.Exception("Database for round " + roundNumber + " is empty.");

        return database[roundNumber].ChooseRandomElementInList();
    }

    [Button]
    public void ClearDatabase()
    {
        database.Clear();
    }
}
