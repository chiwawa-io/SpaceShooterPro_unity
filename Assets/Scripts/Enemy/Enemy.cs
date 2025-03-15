using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private float _Xposition;
    private float _Yposition;
    private Vector2 _position;

    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _enemyDestroyPrefab;
    [SerializeField]
    private Canvas _canvas;

    void Start()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    void Update()
    {
        Movement();
    }

    void Movement() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -4f)
        {
            Respawn();
        }
    }

    void Respawn ()
    {
        _Xposition = Random.Range(-8f, 8f);
        _Yposition = 6f;
        _position = new Vector2(_Xposition, _Yposition);
        transform.position = _position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag== "laser")
        {
            _canvas.ScoreUpdate();
            Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
            Destroy (other.gameObject);
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Player") {
            _canvas.ScoreUpdate();
            Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
            Destroy (gameObject);
        }
    }
}
