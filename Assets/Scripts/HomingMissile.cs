using System.Collections;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    private GameObject _enemy;
    [SerializeField]
    private GameObject _explosion;
    private Rigidbody2D _rb2d;

    [SerializeField]
    private float _speed = 7f;
    private int _hp = 2;

    private Vector2 _direction;
    private Vector2 _deltaPosition;
    void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("obstacle");
        _rb2d = GetComponent<Rigidbody2D>();


        if (_enemy != null) Debug.Log("Enemy on HomingMissile is found");
        if (_enemy == null)
        {
            StartCoroutine(WaitForEnemy());
        }
    }

    private void Update()
    {
        if (transform.position.y >= 9f  || transform.position.y <= -9f)
        {
            Debug.Log("Missile out of bounds");
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (_enemy != null) _direction = (_enemy.transform.position - transform.position).normalized; 
        _deltaPosition = _speed * _direction * Time.fixedDeltaTime;
        _rb2d.MovePosition(_rb2d.position + _deltaPosition);
    }

    IEnumerator WaitForEnemy ()
    {
        while (_enemy == null)
        {
            _enemy = GameObject.FindGameObjectWithTag("obstacle");
            if (_enemy != null) Debug.Log("Enemy found");
            else Debug.Log("Enemy not found");
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "obstacle")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _hp--;
            if (_hp <= 0)
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
