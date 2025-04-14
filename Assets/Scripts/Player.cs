using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private int _health = 3;
    private int _shields = 0;
    [SerializeField]
    private int _ammoCount = 15;
    [SerializeField]
    private float _laserOffset = 1.05f;
    [SerializeField]
    private float _tripleShotOffset = 0.1f;
    
    private Game_manager _gameManager;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _laserContainer;
    [SerializeField]
    private GameObject _laser;
    [SerializeField] 
    private GameObject _trippleShot;
    [SerializeField]
    private GameObject _rocket;
    [SerializeField] 
    private GameObject _shieldVisual;
    [SerializeField] 
    private GameObject _ultraLaserVisual;
    [SerializeField] 
    private GameObject _fireRight;
    [SerializeField] 
    private GameObject _fireLeft;
    [SerializeField]
    private GameObject _electroShockEffect;
    [SerializeField]
    private GameObject _regenEffect;
    [SerializeField]
    private AudioClip _laserSound;
    private AudioClip _rocketSound;
    private AudioSource _playerAudioSource;
    private UiManager _uiManager;
    private CameraScript _cameraScript;
    private Animator _animator;
    private PowerUp _powerUp;

    [SerializeField]
    private float _fireRate = 0.75f;
    [SerializeField]
    private float _sprintRate = 3f;
    private float _canSprint = -1f;
    private float _canFire = -1f;
    
    private float _userVerticalInput;
    private float _userHorizontalInput;
    
    private Vector2 _laserPosition;
    private Vector2 _trippleShotPos;
    private Vector2 _boundaries;
    private Vector2 _direction;

    private bool _isTripleShotOn = false;
    private bool _isShieldOn = false;
    private bool _sprint = false;
    private bool _ultraLaserOn = false;
    private bool _canMove = true;
    private bool _fireRocket = false;

    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _playerAudioSource = GetComponent<AudioSource>();
        _uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        _spawnManager = GameObject.Find("Spawn_manager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<Game_manager>();
        _cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        _animator = GetComponent<Animator>();

        if (_spawnManager == null) Debug.Log("Spawn manager on Player is null");
        if (_gameManager == null) Debug.Log("_game_Manager on Player is null");
        if (_animator == null) Debug.Log("animator on Player is null");
        
        if (_laserContainer == null) Debug.Log("_laserContainer on Player is null");
        if (_laser == null) Debug.Log("_laser on Player is null");
        if (_trippleShot == null) Debug.Log("_trippleShot on Player is null");
        if (_shieldVisual == null) Debug.Log("_shieldVisual on Player is null");
        if (_regenEffect == null) Debug.Log("_regenEffect on Player is null");
        if (_fireRight == null) Debug.Log("_fireRight on Player is null");
        if (_fireLeft == null) Debug.Log("_fireLeft on Player is null");
        if (_shieldVisual == null) Debug.Log("_shieldVisual on Player is null");

        if (_laserSound == null) Debug.Log("_laserSound on Player is null");
        if (_uiManager == null) Debug.Log("UI manager on Player is null");
        if (_playerAudioSource == null) Debug.Log("AudioSource on Player is null");

        StartCoroutine(AmmoSupplyRoutine());
        
    }


    void Update()
    {
        _Movement();
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire && _ammoCount > 0) _Fire();
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > _canSprint) _SprintOn();
        if (Input.GetKeyUp(KeyCode.LeftShift)) _SprintOff();
        if(Input.GetKeyDown(KeyCode.C)) _FindAndGetPowered();
    }

    void _Movement()
    {
        _userHorizontalInput = Input.GetAxis("Horizontal");
        _userVerticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.D)) _animator.SetTrigger("TurningRight");
        if (Input.GetKeyUp(KeyCode.D)) _animator.SetTrigger("FromRightToDefault");
        if (Input.GetKeyDown(KeyCode.A)) _animator.SetTrigger("TurningLeft");
        if (Input.GetKeyUp(KeyCode.A)) _animator.SetTrigger("FromLeftToDefault");


        _direction = new Vector2(_userHorizontalInput, _userVerticalInput);

        if (_sprint && _canMove) transform.Translate(_direction * _speed * 2f * Time.deltaTime);
        else if (_canMove) transform.Translate(_direction * _speed * Time.deltaTime);

        _boundaries = new Vector2(Mathf.Clamp(transform.position.x, -9f, 9f), Mathf.Clamp(transform.position.y, -3f, 5f));
        transform.position = _boundaries;
    }

    void _Fire() {
        _playerAudioSource.clip = _laserSound;
        _ammoCount--;
        _uiManager.AmmoUpdate(_ammoCount);
        _canFire = Time.time + _fireRate;

        if (_ultraLaserOn) _ultraLaserVisual.SetActive(true);
        else if (_fireRocket)
        {
            _playerAudioSource.clip = _rocketSound;
            _playerAudioSource.Play();
            Instantiate(_rocket, transform.position, Quaternion.identity);
            _fireRocket = false;
        }
        else if (_isTripleShotOn)
        {
            _trippleShotPos = new Vector2(transform.position.x + _tripleShotOffset, transform.position.y);
            _playerAudioSource.Play();
            Instantiate(_trippleShot, _trippleShotPos, Quaternion.identity);
        }

        else
        {
            _playerAudioSource.Play();
            _laserPosition = new Vector2(transform.position.x, transform.position.y + _laserOffset);
            Instantiate(_laser, _laserPosition, Quaternion.identity);
        }
    }

    

    void _IsDeath()
    {
        if (Health < 3) _fireRight.SetActive(true);
        if (Health < 2) _fireLeft.SetActive(true);
        if (Health <= 0)
        {
            _spawnManager.StopSpawn();
            _gameManager.PlayerDead();
            Destroy(gameObject);
        }
    }
    
    void _SprintOn()
    {
        StartCoroutine(TurnOffSprint());
        _canSprint = Time.time + _sprintRate;
        _sprint = true;
        _uiManager.SprintOn();
    }
    void _SprintOff()
    {
        _sprint = false;
        _uiManager.SprintOff();
    }

    void _FindAndGetPowered() 
    {
        _powerUp = GameObject.FindGameObjectWithTag("PowerUp").GetComponent<PowerUp>();
        if (_powerUp != null) _powerUp.PowerUpMagnet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "obstacle"  && _isShieldOn == false) 
        {
            _DamageTaken();
            _IsDeath();
        }
        if (other.transform.tag == "obstacle" && _isShieldOn) { Shields--; _ShieldCheck(); }
        if (other.transform.name == "AmmoSupplyPowerUp(Clone)") { _ammoCount += 30; _uiManager.AmmoUpdate(_ammoCount);}
        if (other.transform.name == "HealthPowerUp(Clone)") _Regen();
        if (other.transform.name == "ShieldPowerUp(Clone)") _ShieldActive();
        if (other.transform.name == "TrippleShotPowerUp(Clone)") StartCoroutine(TrippleShotEffect());
        if (other.transform.name == "UltraLaserPowerUp(Clone)") StartCoroutine(_UltraLaserOn());
        if (other.transform.name == "ElectroShock(Clone)") StartCoroutine(_ElectroShock());
        if (other.transform.name == "RocketPowerUp(Clone)") _RocketPowerUp();
       
    }

    
    

    void _ShieldActive()
    {
        Shields++;
        _ShieldCheck();
    }

    void _ShieldCheck()
    {
        if (Shields > 0)
        {
            _isShieldOn = true;
            _shieldVisual.SetActive(true);
            _uiManager.ShieldsUpdate(Shields);
        }
        else
        {
            _isShieldOn = false;
            _shieldVisual.SetActive(false);
            _uiManager.ShieldsUpdate(Shields);
        }
    }

    void _Regen()
    {
        _regenEffect.SetActive(true);
        StartCoroutine(RegenEffect());
        if (Health < 3) Health++; _uiManager.LivesUpdate(Health);
        if (Health == 2) _fireLeft.SetActive(false);
        if (Health == 3) _fireRight.SetActive(false);
    }

    void _DamageTaken() {
        Health -= 1;
        _uiManager.LivesUpdate(Health);
        _cameraScript.CameraShake();
    }
    void _RocketPowerUp ()
    {
        _fireRocket = true;
    }

    public int Health
    {
        get => _health;
        set => _health = Mathf.Clamp(value, 0, 3);
    }

    public int Shields
    {
        get => _shields;
        set => _shields = Mathf.Clamp(value, 0, 3);
    }

    IEnumerator TrippleShotEffect()
    {
        _isTripleShotOn = true;
        yield return new WaitForSeconds(5);
        _isTripleShotOn = false;
    }
    IEnumerator TurnOffSprint()
    {
        yield return new WaitForSeconds(2f);
        _uiManager.SprintOff();
        _sprint = false;
    }
    IEnumerator RegenEffect() 
    {
        yield return new WaitForSeconds(0.5f);
        _regenEffect.SetActive(false);
    }

    IEnumerator _UltraLaserOn () {
        _ultraLaserOn = true;
        yield return new WaitForSeconds(5f);
        _ultraLaserOn = false;
        _ultraLaserVisual.SetActive(false);
    }

    IEnumerator AmmoSupplyRoutine ()
    {
        while (true) {
            yield return new WaitForSeconds(1f);
            if (_ammoCount == 0) _spawnManager.SpawnAmmoSuply();
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator _ElectroShock() 
    {
        _canMove = false;
        _electroShockEffect.SetActive(true);
        yield return new WaitForSeconds(3f);
        _electroShockEffect.SetActive(false);
        _canMove = true;
    }
}
