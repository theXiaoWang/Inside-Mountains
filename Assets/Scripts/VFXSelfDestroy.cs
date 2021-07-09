using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSelfDestroy : MonoBehaviour
{
    [SerializeField] private float timeTillDestroy = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }
}
