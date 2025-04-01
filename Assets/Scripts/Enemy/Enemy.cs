using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private float _Xposition;
    private float _Yposition;
    private Vector2 _position;

    [SerializeField]
    private float _sinFrequency = 3f;
    [SerializeField]
    private float _sinAmplitude = 3f;
    private float _sinX;


    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _enemyDestroyPrefab;
    [SerializeField]
    private UiManager _uiManager;

    private int _randomMovement = 0;

    void Start()
    {
        _uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();

        if (_uiManager == null) Debug.Log("UI manager on Enemy is null");

        _randomMovement = Random.Range(0, 4);

        if (_randomMovement == 2) transform.Rotate(0, 0, 20f);
        if (_randomMovement == 1) transform.Rotate(0, 0, -20f);
    }
    void Update()
    {
       if (_randomMovement <= 2) BasicMovement();
       if (_randomMovement > 2) ZigZagMovement();
    }

    void BasicMovement() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -4f)
        {
            Destroy(gameObject);
        }
    }

    void ZigZagMovement() {
        _sinX = Mathf.Sin(Time.time * _sinFrequency) * _sinAmplitude;
        transform.position = new Vector2(_sinX, transform.position.y - _speed * Time.deltaTime);
        if (transform.position.y <= -4f)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag== "laser")
        {
            _uiManager.ScoreUpdate();
            Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
            Destroy (other.gameObject);
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Player") {
            _uiManager.ScoreUpdate();
            Instantiate(_enemyDestroyPrefab, transform.position, Quaternion.identity);
            Destroy (gameObject);
        }

    } 
}
