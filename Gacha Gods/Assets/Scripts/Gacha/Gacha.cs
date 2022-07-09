using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    [ReadOnly, SerializeField] List<Character> characters;

    [SerializeField] Button skipButton;
    [SerializeField] Image Background;
    [SerializeField] List<RectTransform> Positions;
    [SerializeField] RectTransform CharacterArt;
    RectTransform Destination;
    [SerializeField] float speed;
    int Index;

    private void Awake()
    {
        skipButton.AddListenerToButton(Skip);
    }

    private void Start()
    {
        Index = 0;
        Destination = Positions[0];
        CharacterArt.position = Positions[0].position;
        InitialiseCharacterArt();
    }

    public void InitialiseCharacters(List<Character> characters)
    {
        this.characters = new List<Character>(characters);
    }

    void Skip()
    {
        Index = characters.Count;
        CharacterArt.position = Positions[2].position;
        skipButton.gameObject.SafeSetActive(false);
    }

    private void Update()
    {
        if (Index < characters.Count + 1)
        {
            Background.DecreaseTransparency(1f);

            if (skipButton)
                skipButton.DecreaseTransparency(1f);
        }
        else
        {
            Background.IncreaseTransparency(1f);

            if (skipButton)
                skipButton.IncreaseTransparency(1f);

            if (Background.color.a == 0)
                Destroy(gameObject);
        }

        if (!Background.IsOpaque())
            return;

        if (Input.GetMouseButtonDown(0))
            MoveOffScreen();

        if (Vector3.Distance(CharacterArt.position, Positions[2].position) < 0.1f)
            SnapToStart();

        if (Vector3.Distance(CharacterArt.position, Positions[0].position) < 0.1f)
            MoveOnScreen();

        CharacterArt.transform.MoveToPosition(Destination.position, speed);
    }

    void SnapToStart()
    {
        CharacterArt.position = Positions[0].position;
        InitialiseCharacterArt();
    }

    void MoveOnScreen()
    {
        Destination = Positions[1];
    }

    void MoveOffScreen()
    {
        if (Destination == Positions[1])
        {
            Destination = Positions[2];
        }
    }

    void InitialiseCharacterArt()
    {
        if (Index < characters.Count - 1)
            CharacterArt.GetComponent<CharacterArt>().Initialise(characters[Index]);

        Index++;
    }
}
