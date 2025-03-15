using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWave : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Attack());
    }


    void Update()
    {
        
    }

    IEnumerator Attack ()
    {

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
