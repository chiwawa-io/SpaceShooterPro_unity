using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _sinFrequency = 3f;
    [SerializeField]
    private float _sinAmplitude = 3f;
    private float _sinX;
    private Vector2 _direction;
    private Vector2 _deltaPosition;


    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _enemyDestroyPrefab;
    [SerializeField]
    private UiManager _uiManager;
    [SerializeField]
    private GameObject _player;
    private Rigidbody2D _rb2d;

    [SerializeField]
    private int _randomMovement = 0;
    private bool _offensiveMode = false;

    void Start()
    {
        _uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb2d = GetComponent<Rigidbody2D>();

        if (_uiManager == null) Debug.Log("UI manager on Enemy is null");
        if (_player == null) Debug.Log("Player on Enemy is null");
        if (_rb2d == null) Debug.Log("Rigidbody on Enemy is null");

        _randomMovement = Random.Range(1, 5);

        if (_randomMovement == 2) transform.Rotate(0, 0, 20f);
        if (_randomMovement == 1) transform.Rotate(0, 0, -20f);
        if (_randomMovement == 4) _offensiveMode = true;
    }
    void Update()
    {
       if (_randomMovement <= 2) BasicMovement();
       if (_randomMovement == 3) ZigZagMovement();
    }

    private void FixedUpdate()
    {
        if (_offensiveMode)
        {
            _speed = 3f;
            if (_player != null) _direction = (_player.transform.position - transform.position).normalized;
            _deltaPosition = _speed * _direction * Time.fixedDeltaTime;
            _rb2d.MovePosition(_rb2d.position + _deltaPosition);
        }
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
