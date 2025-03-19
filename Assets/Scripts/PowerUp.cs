using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private AudioClip _powerUpSound;

    private void Start()
    {
        if (_powerUpSound == null) Debug.Log("Powerup sound is null");
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(_powerUpSound, transform.position);
        if (collision.tag == "Player" ) Destroy(gameObject);
    }
}
