using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] GameObject graphics;
    // Start is called before the first frame update
    void Awake()
    {
        graphics.SetActive(false);
    }


}
