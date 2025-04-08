using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private AudioClip _powerUpSound;

    private GameObject _player;
    private Rigidbody2D _rb2d;

    private Vector2 _deltaPosition;
    private Vector2 _direction;


    private bool _basicMovement = true;
    private bool _toPlayerMovement = false;
    private bool _isNearPlayer = false;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb2d = GetComponent<Rigidbody2D>();

        if (_powerUpSound == null) Debug.Log("Powerup sound is null");
        if (_player == null) Debug.Log("Player on power up is null");
        if (_player == null) Debug.Log("Rigidbody on power up is null");
    }

    void Update()
    {
        if (_basicMovement) transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f) Destroy(gameObject);
       
    }

    private void FixedUpdate()
    {
        if (_toPlayerMovement)
        {
            if (_player != null) _direction = (_player.transform.position - transform.position).normalized;
            _deltaPosition = _speed * _direction * Time.fixedDeltaTime;
            _rb2d.MovePosition(_rb2d.position + _deltaPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);
        if (collision.tag == "Player" ) Destroy(gameObject);
        if (collision.tag == "PowerUpDestroyer") Destroy(gameObject);
    }

    public void PowerUpMagnet()
    {
        _isNearPlayer = 
            (_player.transform.position.x - transform.position.x < 3f) && (_player.transform.position.x - transform.position.x > -3f) &&
            (_player.transform.position.x - transform.position.y < 3f) && (_player.transform.position.x - transform.position.y > -3f);
        Debug.Log(_isNearPlayer);
        if (_isNearPlayer) { _toPlayerMovement = true; _basicMovement = false; _speed = 10; }
        Debug.Log("PowerUpMagnet Called");
    }
}
