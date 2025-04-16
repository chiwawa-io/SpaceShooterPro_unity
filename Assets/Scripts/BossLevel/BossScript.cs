using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField]
    private int _health = 1500;

    [SerializeField]
    private GameObject _driftingLeftMob;
    [SerializeField]
    private GameObject _driftingRightMob;
    [SerializeField]
    private GameObject _mobsWave;
    [SerializeField]
    private GameObject _laserWave;
    [SerializeField]
    private GameObject _superMobsWave;
    [SerializeField]
    private GameObject _superLaserWave;
    [SerializeField]
    private GameObject _explosion;

    private UiManager _uiManager;
    private Game_manager _gameManager;

    void Start()
    {
        _uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<Game_manager>();

        if (_uiManager == null) Debug.Log("UIManager is NULL on BossScript");
        if (_gameManager == null) Debug.Log("GameManager is NULL on BossScript");

        StartCoroutine(SpawnLaserWave());
        StartCoroutine(SpawnMobsWave());
        StartCoroutine(HealthCheck()); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "laser" || collision.transform.tag == "Player") _health--; 
    }


    IEnumerator HealthCheck ()
    {
        while (_health > 0)
        {
            yield return new WaitForSeconds(1f);
            if (_health <= 1350 && _health > 1330)
            {
                _uiManager.BossHpUpdate(9);
                Instantiate(_explosion, transform.position, Quaternion.identity);
            }
            else if (_health <= 1200 && _health > 1180)
            {
                _uiManager.BossHpUpdate(8);
                Instantiate(_explosion, transform.position, Quaternion.identity);
            }
            else if (_health <= 1050 && _health > 1030)
            {
                _uiManager.BossHpUpdate(7);
                Instantiate(_explosion, transform.position, Quaternion.identity);
            }
            else if (_health <= 900 && _health > 880)
            {
                _uiManager.BossHpUpdate(6);
                Instantiate(_explosion, transform.position, Quaternion.identity);
            }
            else if (_health <= 750 && _health > 730)
            {
                _uiManager.BossHpUpdate(5);
                Instantiate(_explosion, transform.position, Quaternion.identity);
            }
            else if (_health <= 600 && _health > 580)
            {
                _uiManager.BossHpUpdate(4);
                Instantiate(_explosion, transform.position, Quaternion.identity);
            }
            else if (_health <= 450 && _health > 430)
            {
                _uiManager.BossHpUpdate(3);
                Instantiate(_explosion, transform.position, Quaternion.identity);
            }
            else if (_health <= 300 && _health > 280)
            {
                _uiManager.BossHpUpdate(2);
                Instantiate(_explosion, transform.position, Quaternion.identity);
            }
            else if (_health <= 150 && _health > 130)
            {
                _uiManager.BossHpUpdate(1);
                Instantiate(_explosion, transform.position, Quaternion.identity);
            }
            else if (_health <= 0)
            {
                _uiManager.BossHpUpdate(0);
                Instantiate(_explosion, transform.position, Quaternion.identity);
                _gameManager.RoundWon();
                _uiManager.RoundWon();  
                Destroy(gameObject);
            }
        }
    }
    IEnumerator SpawnLaserWave ()
    {
        while (_health > 0)
        {
            if (_health > 1200) 
            {
                yield return new WaitForSeconds(Random.Range(7f,10f));
                Instantiate(_driftingLeftMob, transform.position, Quaternion.identity);
                Instantiate(_driftingRightMob, transform.position, Quaternion.identity);
                Instantiate(_laserWave, new Vector2(-5.37f, 2.43f), Quaternion.identity);
            }
            else 
            {
                yield return new WaitForSeconds(Random.Range(7f, 10f));
                Instantiate(_driftingLeftMob, transform.position, Quaternion.identity);
                Instantiate(_driftingRightMob, transform.position, Quaternion.identity);
                Instantiate(_laserWave, new Vector2(-5.37f, 2.43f), Quaternion.identity);
                Instantiate(_superLaserWave, new Vector2(-1.62f, 9.5f), Quaternion.identity);
            }
        }
    }
    IEnumerator SpawnMobsWave()
    {
        while (_health > 0)
        {
            yield return new WaitForSeconds(Random.Range(7f, 10f));
            if (_health > 1200) Instantiate(_mobsWave, new Vector2 (-6.94f, 8.2f), Quaternion.identity);
            else Instantiate(_superMobsWave, new Vector2(-7.02f, 8.23f), Quaternion.identity);
            
        }
    }
}
