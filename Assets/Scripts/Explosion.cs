using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _audioSource;
    void Start()
    {
        StartCoroutine(DestroyUs());
    }

    IEnumerator DestroyUs ()
    {
        yield return new WaitForSeconds(2.35f);
        Destroy(gameObject);
    }
}
