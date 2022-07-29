using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Managers/Board Database")]
public class BoardDatabase : Factories.FactoryBase
{
    public static Dictionary<int, List<BoardData>> Database => database;
    static Dictionary<int, List<BoardData>> database = new Dictionary<int, List<BoardData>>();

    public override void Initialise()
    {

    }

    public static void SaveBoard(BoardData data, int round)
    {
        if (!database.ContainsKey(round))
            database.Add(round, new List<BoardData>());

        if (!Database[round].Contains(data))
            Database[round].Add(data);
    }

    public static BoardData LoadRandomBoardData(int round)
    {
        if (!Database.ContainsKey(round))
            throw new System.Exception("Database is empty");

        if(Database[round].IsNullOrEmpty())
            throw new System.Exception("Database is empty");

        return Database[round].ChooseRandomElementInList();
    }

    [Button]
    public static void ClearDatabase()
    {
        foreach (var item in Database)
        {
            foreach (var scriptables in item.Value)
            {
                DestroyImmediate(scriptables);
            }
        }

        Database.Clear();
    }
}
