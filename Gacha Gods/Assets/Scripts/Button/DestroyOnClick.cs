using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnClick : MonoBehaviour
{
    [SerializeField] GameObject GameObjectToDestroy;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(DestroyGameObject);
    }

    void DestroyGameObject()
    {
        if (GameObjectToDestroy == null)
            Destroy(gameObject);
        else
            Destroy(GameObjectToDestroy);
    }
}
