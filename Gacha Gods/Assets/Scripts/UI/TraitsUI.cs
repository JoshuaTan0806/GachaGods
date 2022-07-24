using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TraitsUI : MonoBehaviour
{
    TextList TextList;

    private void Awake()
    {
        TextList = GetComponent<TextList>();
    }

    private void OnEnable()
    {
        UpdateList();
        BoardManager.OnHeldCharacterChanged += UpdateList;
    }

    private void OnDisable()
    {
        BoardManager.OnHeldCharacterChanged -= UpdateList;
    }

    void UpdateList()
    {
        TextList.Clear();

        Dictionary<ScriptableObject, int> traits = new Dictionary<ScriptableObject, int>();

        foreach (var item in CharacterManager.ActiveArchetypes)
        {
            traits.Add(item.Key, item.Value);
        }

        foreach (var item in CharacterManager.ActiveRoles)
        {
            traits.Add(item.Key, item.Value);
        }

        List<ScriptableObject> newTraits = traits.Where(x => x.Value > 0).OrderByDescending(x => x.Value).Select(x => x.Key).ToList();

        foreach (var item in newTraits)
        {
            TextList.SpawnText(traits[item] + " " + item.name, 30);
        }
    }
}
