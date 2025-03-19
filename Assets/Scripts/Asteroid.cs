using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    [SerializeField]
    private int _livescount = 3;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_manager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        Rotate();
        if (_livescount <= 0) DestroyUs();
    }

    void Rotate ()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "laser")
        {
            Destroy(collision.gameObject);
            _livescount--;
            
        }
    }

    private void DestroyUs()
    {
        _spawnManager.StartWave();
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
