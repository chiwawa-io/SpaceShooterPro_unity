using System.Collections;
using UnityEngine;

public class EnemyShootingLasers : MonoBehaviour
{
    private UiManager _uiManager;
    private GameObject _player;
    private GameObject _powerUp;
    [SerializeField]
    private GameObject _blowUp;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _powerUpAttack;
    private GameObject _playerAttack;
    private Rigidbody2D _rb2d;

    private float _previusSpeed;
    private float _distanceSqr;
    private float _dotProductPlayerForward;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _health = 2;
    [SerializeField]
    private int _ID = 0;

    private Vector3 _directionToPlayer;
    private Vector2 _avoidDirection;
    private Vector2 _deltaPosition;
    private Vector2 _powerUpDestroyerLocalPosition;

    private bool _isPowerUpNear = false;
    private bool _avoidPlayerAttack = false;
    private bool _basicMovement = true;
    private bool _isNearLaser = false;

    void Start()
    {
        _uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb2d = GetComponent<Rigidbody2D>();

        if (_uiManager == null) Debug.Log("UI manager on Enemy is null");
        if (_player == null) Debug.Log("Player on Smart Enemy is null");
        if (_rb2d == null) Debug.Log("Rigidbody on Enemy is null");

        if (_ID == 1) StartCoroutine(ShootLaserForward());
        if (_ID == 2) StartCoroutine(_BehindPlayerCheck());

        StartCoroutine(_PowerUpNearCheck());
        StartCoroutine(_PlayerAttackCheck());

        _previusSpeed = _speed;
        _powerUpDestroyerLocalPosition = _powerUpAttack.transform.localPosition;
    }


    void Update()
    {
        if (_ID == 1) BasicMovement();
        if (_ID == 2) SmartEnemyMovement();
        
        if (_isPowerUpNear) _powerUpAttack.transform.localPosition = new Vector2(0, -1.8f);
    }

    private void FixedUpdate()
    {
        if (_avoidPlayerAttack)
        {
            if (_playerAttack != null) _avoidDirection = (_playerAttack.transform.position - transform.position).normalized;
            _deltaPosition = _speed * _avoidDirection * Time.fixedDeltaTime;
            _rb2d.MovePosition(_rb2d.position - _deltaPosition);
        }
    }
    void BasicMovement()
    {
        if (_basicMovement) transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -4f)
        {
            Destroy(gameObject);
        }
    }

    void SmartEnemyMovement()
    {
        if (_basicMovement) transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y <= -15f)
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
            if (_player != null) _directionToPlayer = (_player.transform.position - transform.position).normalized;
            if (_player != null) _dotProductPlayerForward = Vector3.Dot(_directionToPlayer, _player.transform.right);
            //Debug.Log(_dotProductPlayerForward);

            if (_dotProductPlayerForward <= 0.1f) Instantiate(_laser, transform.position, Quaternion.identity); //Debug.Log("Enemy when shoot: " + "X: " + transform.position.x + "Y: " + transform.position.y); 
            }
    }
    IEnumerator _PlayerAttackCheck() {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            _playerAttack = GameObject.FindGameObjectWithTag("laser");
            if (_playerAttack != null) { _distanceSqr = (_playerAttack.transform.position - transform.position).sqrMagnitude; _isNearLaser = _distanceSqr < 4f * 4f; } //Debug.Log(_playerAttack.transform.name); 
            if (_isNearLaser) { _basicMovement = false; _avoidPlayerAttack = true; _speed = 5f; }
            yield return new WaitForSeconds(0.2f);
            _playerAttack = null; _isNearLaser = false; //Debug.Log("Player attack is null");
            if (_playerAttack == null) _basicMovement = true; _avoidPlayerAttack = false; _speed = _previusSpeed;
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

    IEnumerator _PowerUpNearCheck()
    {
        while (true) 
        {
            yield return new WaitForSeconds(2f);
            _powerUp = GameObject.FindGameObjectWithTag("PowerUp");
            if (_powerUp != null) { _isPowerUpNear = (_powerUp.transform.position.x - transform.position.x < 5f) && (_powerUp.transform.position.y - transform.position.y < 5f); } //Debug.Log("PowerUp found"); 
            if (_isPowerUpNear) { _powerUpAttack.SetActive(true); } //Debug.Log("powerUp is near"); 
            yield return new WaitForSeconds(0.5f);
            _powerUpAttack.SetActive(false);
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
            _health -= 3;
            _IsDeath();
        }

    }
}
