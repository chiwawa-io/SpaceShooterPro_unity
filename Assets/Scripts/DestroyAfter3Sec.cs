using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter3Sec : MonoBehaviour
{
    private AudioSource _audioSource;
    void Start()
    {
        StartCoroutine(DestroyUs());
    }

    IEnumerator DestroyUs ()
    {
        yield return new WaitForSeconds(2.35f);
        if (transform.parent != null) Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }
}
