using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject ParticleOnClickPrefab;
    public GameObject ParticleOnMovePrefab;
    GameObject ParticleOnMove;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (ParticleOnClickPrefab)
                Instantiate(ParticleOnClickPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

            if (ParticleOnMove == null && ParticleOnMovePrefab)
                ParticleOnMove = Instantiate(ParticleOnMovePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }

        if(Input.GetMouseButton(0))
        {
            if (ParticleOnMove)
                ParticleOnMove.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
