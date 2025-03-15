using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private float _laserOffset = 1.05f;
    [SerializeField]
    private float _tripleShotOffset = 0.1f;

    
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _laserContainer;
    [SerializeField]
    private GameObject _laser;
    [SerializeField] 
    private GameObject _trippleShot;
    [SerializeField] 
    private GameObject _shieldVisual;
    [SerializeField] 
    private GameObject _fireRight;
    [SerializeField] 
    private GameObject _fireLeft;
    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource _playerAudioSource;
    private Canvas _canvas;

    [SerializeField]
    private float _fireRate = 0.75f;
    private float _canFire = -1f;
    private float _userVerticalInput;
    private float _userHorizontalInput;
    
    private Vector2 _laserPosition;
    private Vector2 _trippleShotPos;
    private Vector2 _boundaries;
    private Vector2 _direction;

    private bool _isTripleShotOn = false;
    private bool _isSpeedUpOn = false;
    private bool _isShieldOn = false;


    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _playerAudioSource = GetComponent<AudioSource>();
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _spawnManager = GameObject.Find("Spawn_manager").GetComponent<SpawnManager>();

        if (_spawnManager == null) Debug.Log("Spawn manager on Player is null");
        
        if (_laserContainer == null) Debug.Log("_laserContainer on Player is null");
        if (_laser == null) Debug.Log("_laser on Player is null");
        if (_trippleShot == null) Debug.Log("_trippleShot on Player is null");
        if (_shieldVisual == null) Debug.Log("_shieldVisual on Player is null");
        if (_fireRight == null) Debug.Log("_fireRight on Player is null");
        if (_fireLeft == null) Debug.Log("_fireLeft on Player is null");
        if (_shieldVisual == null) Debug.Log("_shieldVisual on Player is null");

        if (_laserSound == null) Debug.Log("_laserSound on Player is null");
        if (_canvas == null) Debug.Log("UI manager on Player is null");
        if (_playerAudioSource == null) Debug.Log("AudioSource on Player is null");

    }


    void Update()
    {
        Movement();
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire) Fire();

        
    }

    void Movement()
    {
        _userHorizontalInput = Input.GetAxis("Horizontal");
        _userVerticalInput = Input.GetAxis("Vertical");

        _direction = new Vector2(_userHorizontalInput, _userVerticalInput);

        if (_isSpeedUpOn) transform.Translate(_direction * _speed * 2f * Time.deltaTime);
        else transform.Translate(_direction * _speed * Time.deltaTime);

        _boundaries = new Vector2(Mathf.Clamp(transform.position.x, -9f, 9f), Mathf.Clamp(transform.position.y, -3f, 5f));
        transform.position = _boundaries;
    }

    void Fire() {
        _playerAudioSource.clip = _laserSound;
        _laserPosition = new Vector2(transform.position.x, transform.position.y + _laserOffset);
        _trippleShotPos = new Vector2(transform.position.x + _tripleShotOffset, transform.position.y);
        _canFire = Time.time + _fireRate;
        if (_isTripleShotOn == true)
        {
            GameObject newGameObject = Instantiate(_trippleShot, _laserPosition, Quaternion.identity);
    
        }
        else {
            GameObject newGameObject = Instantiate(_laser, _laserPosition, Quaternion.identity);
            
        }
        _playerAudioSource.Play();
    }

    

    void IsDeath()
    {
        if (_health == 2) _fireRight.SetActive(true);
        if (_health == 1) _fireLeft.SetActive(true);
        if (_health <= 0)
        {
            _spawnManager.StopSpawn();

            Destroy(gameObject);
        }
    }
    
    

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "obstacle"  && _isShieldOn == false) 
        { 
            _health -= 1; 
            IsDeath();
            _canvas.LivesUpdate(_health); 
        }
        if (other.transform.tag == "obstacle") { _isShieldOn = false; _shieldVisual.SetActive(false); }
        if (other.transform.tag == "tripleShot") _TrippleShotActive();
        if (other.transform.tag == "speedPower") _SpeedUpActive();
        if (other.transform.tag == "shield") _ShieldActive();
    }

    void _TrippleShotActive()
    {
        
        
        _isTripleShotOn = true;
        StartCoroutine(TrippleShotEffect());
    }

    IEnumerator TrippleShotEffect()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotOn = false;
    }

    void _SpeedUpActive() 
    {

        _isSpeedUpOn = true;
        StartCoroutine(SpeedUpEffect());
    }

    IEnumerator SpeedUpEffect()
    {
        yield return new WaitForSeconds(5);
        _isSpeedUpOn = false;
    }

    void _ShieldActive()
    {

        _isShieldOn = true;
        _shieldVisual.SetActive(true);
    }

    
}
