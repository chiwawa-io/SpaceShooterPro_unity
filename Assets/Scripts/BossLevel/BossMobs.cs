using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossMobs : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyDestroyPrefab;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.tag == "laser")
        {
            Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
