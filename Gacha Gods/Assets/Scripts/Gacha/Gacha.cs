using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Gacha : MonoBehaviour
{
    [ReadOnly] public List<Character> characters;

    [SerializeField] List<RectTransform> Positions;
    [SerializeField] RectTransform CharacterArt;
    RectTransform Destination;
    [SerializeField] float speed;

    int Index;


    private void Start()
    {
        Destination = Positions[0];
        CharacterArt.position = Positions[0].position;
        Index = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MoveOffScreen();

        if (Vector3.Distance(CharacterArt.position, Positions[2].position) < 0.1f)
            CharacterArt.position = Positions[0].position;

        if (Vector3.Distance(CharacterArt.position, Positions[0].position) < 0.1f)
            Destination = Positions[1];

        CharacterArt.transform.MoveToPosition(Destination.position, speed);
    }

    void MoveOffScreen()
    {
        if (Destination == Positions[1])
        {
            Destination = Positions[2];
        }
    }
}
