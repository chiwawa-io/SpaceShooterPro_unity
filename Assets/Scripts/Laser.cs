﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int _speed;

    void Update()
    {
        transform.Translate (Vector3.up*Time.deltaTime*_speed);

        if (transform.position.y >= 9f)
        {
            Destroy(gameObject);
            if (transform.parent != null) Destroy(transform.parent.gameObject); 
            
        }

    }

    
}
