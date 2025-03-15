using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject _powerUpContainer;
    [SerializeField]
    private GameObject _trippleShotPowerUp;
    [SerializeField]
    private GameObject _speedPowerUp;
    [SerializeField]
    private GameObject _shieldPowerUp;
    private bool _stopSpawn = false;
    private bool _playerDead = false;
    private Vector2 _spawnPos;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R) && _playerDead == true) Restart();
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
    }

    public void StopSpawn()
    {
        _stopSpawn = true;
    }

    public void PlayerDead()
    {
        _playerDead = true;
    }

    
    IEnumerator SpawnTripplePowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(7f, 10f));
        while (_stopSpawn == false)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            GameObject newGameObject = Instantiate(_trippleShotPowerUp, _spawnPos, Quaternion.identity);
            newGameObject.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(7f, 20f));
        }
    }

    IEnumerator SpawnSpeedPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(10f, 15f));
        while (_stopSpawn == false)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            GameObject newGameObject = Instantiate(_speedPowerUp, _spawnPos, Quaternion.identity);
            newGameObject.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(14f, 15f));
        }
    }

    IEnumerator SpawnShieldPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(5f, 13f));
        while (_stopSpawn == false)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            GameObject newGameObject = Instantiate(_shieldPowerUp, _spawnPos, Quaternion.identity);
            newGameObject.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(7f, 20f));
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(2); //current scene
    }

    public void StartWave()
    {
        StartCoroutine(StartWaveRoutine());
    }

    IEnumerator StartWaveRoutine()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(SpawnTripplePowerUpRoutine());
        StartCoroutine(SpawnSpeedPowerUpRoutine());
        StartCoroutine(SpawnShieldPowerUpRoutine());
    }
}
