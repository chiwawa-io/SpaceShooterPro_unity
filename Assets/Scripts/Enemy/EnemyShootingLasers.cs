using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingLasers : MonoBehaviour
{
    private UiManager _uiManager;
    private GameObject _player;
    private Vector2 _directionToPlayer;
    private float _dotProductPlayerForward;
    [SerializeField]
    private GameObject _blowUp;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _health = 2;
    [SerializeField]
    private int _ID = 0;

    void Start()
    {
        _uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_uiManager == null) Debug.Log("UI manager on Enemy is null");
        if (_player == null) Debug.Log("Player on Smart Enemy is null");

        if (_ID == 1) StartCoroutine(ShootLaserForward());
        if (_ID == 2) StartCoroutine(_BehindPlayerCheck());
    }


    void Update()
    {
        if (_ID == 1) BasicMovement();
        if (_ID == 2) SmartEnemyMovement();
        
    }

    void BasicMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -4f)
        {
            Destroy(gameObject);
        }
    }

    void SmartEnemyMovement()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y <= -10f)
        {
            Destroy(gameObject);
        }
    }

    void _IsDeath()
    {
        if (_health <= 0)
        {
            _uiManager.BigScoreUpdate();
            Instantiate(_blowUp, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator _BehindPlayerCheck() {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (_player != null) _directionToPlayer = _player.transform.position - transform.position;
            _dotProductPlayerForward = Vector2.Dot(_directionToPlayer.normalized, _player.transform.forward);

            if (_dotProductPlayerForward < 0.5f) { Instantiate(_laser, transform.position, Quaternion.identity); Debug.Log("Enemy when shoot: " + "X: " + transform.position.x + "Y: " + transform.position.y); }
            }
    }

    IEnumerator ShootLaserForward ()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Instantiate(_laser, transform.position , Quaternion.identity); //new Vector2(transform.position.y - 1.95f, transform.position.x + 0.5f) idk why this cause bug
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "laser")
        {
            _health--;
            _IsDeath();
            Destroy(other.gameObject);
        }
        else if (other.transform.tag == "Player")
        {
            _health--;
            _IsDeath();
        }

    }
}
