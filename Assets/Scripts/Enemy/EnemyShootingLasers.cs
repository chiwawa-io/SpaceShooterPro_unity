using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingLasers : MonoBehaviour
{
    private UiManager _uiManager;
    [SerializeField]
    private GameObject _blowUp;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private float _speed = 5f;

    void Start()
    {
        _uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();

        if (_uiManager == null) Debug.Log("UI manager on Enemy is null");

        StartCoroutine(ShootLaser());
    }


    void Update()
    {
        BasicMovement();
    }

    void BasicMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -4f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ShootLaser ()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            Instantiate(_laser, transform.position , Quaternion.identity); //new Vector2(transform.position.y - 1.95f, transform.position.x + 0.5f) idk why this cause bug
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "laser")
        {
            _uiManager.BigScoreUpdate();
            Instantiate(_blowUp, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Player")
        {
            _uiManager.BigScoreUpdate();
            Instantiate(_blowUp, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
