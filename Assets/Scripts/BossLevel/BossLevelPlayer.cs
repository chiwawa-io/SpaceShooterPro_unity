using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelPlayer : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private int _health = 3;
    private int _shields = 0;
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
    [SerializeField]
    private AudioClip _powerUpSound;
    private AudioSource _playerAudioSource;
    private CanvasScript _canvasScript;
    private Game_manager _gameManager;
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
        transform.position = new Vector3(0, 0, 0);
        
        _playerAudioSource = GetComponent<AudioSource>();
        _canvasScript = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        _gameManager = GameObject.Find("GameManager").GetComponent<Game_manager>();

        if (_playerAudioSource == null) Debug.Log("AudioSource is null");
        if (_canvasScript == null) Debug.Log("CanvasScript is null");
        if (_gameManager == null) Debug.Log("GameManager is null");

        Mathf.Clamp(_shields, 0, 3f); // making shields limited to three
    }


    void Update()
    {
        _Movement();
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire) _Fire();
        _IsDeath();
        _ShieldCheck();
    }

    void _Movement()
    {
        _userHorizontalInput = Input.GetAxis("Horizontal");
        _userVerticalInput = Input.GetAxis("Vertical");

        _direction = new Vector2(_userHorizontalInput, _userVerticalInput);

        if (_isSpeedUpOn) transform.Translate(_direction * _speed * 2f * Time.deltaTime);
        else transform.Translate(_direction * _speed * Time.deltaTime);

        _boundaries = new Vector2(Mathf.Clamp(transform.position.x, -9f, 9f), Mathf.Clamp(transform.position.y, -3f, 5f));
        transform.position = _boundaries;
    }

    void _Fire()
    {

        _playerAudioSource.clip = _laserSound;
        _laserPosition = new Vector2(transform.position.x, transform.position.y + 1.05f);
        _trippleShotPos = new Vector2(transform.position.x + 0.1f, transform.position.y);
        _canFire = Time.time + _fireRate;
        if (_isTripleShotOn == true)
        {
            GameObject newGameObject = Instantiate(_trippleShot, _laserPosition, Quaternion.identity);
            _playerAudioSource.Play();
        }
        else
        {
            GameObject newGameObject = Instantiate(_laser, _laserPosition, Quaternion.identity);
            _playerAudioSource.Play();
        }        
    }
    void _IsDeath()
    {
        if (_health < 3) _fireRight.SetActive(true);
        if (_health < 2) _fireLeft.SetActive(true);
        if (_health <= 0)
        {
            _gameManager.PlayerDead();
            Destroy(gameObject);
        }
    }

    void _ShieldCheck() 
    {
        if (_shields > 0) _isShieldOn = true;
        else
        {
            _isShieldOn = false;
            _shieldVisual.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "BossAtacks" && _isShieldOn == false)
        {
            _health --;
            _canvasScript.LivesUpdate(_health);
        }
        if (other.transform.tag == "BossAtacks" && _isShieldOn == true) { _shields--; _canvasScript.ShieldsUpdate(_shields); }
        if (other.transform.tag == "tripleShot") _TrippleShotActive();
        if (other.transform.tag == "speedPower") _SpeedUpActive();
        if (other.transform.tag == "shield") _ShieldActive();
    }

    void _TrippleShotActive()
    {
        AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);

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
        AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);
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
        AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);
        _shields++;
        _canvasScript.ShieldsUpdate(_shields);
        _shieldVisual.SetActive(true);
    }

}
