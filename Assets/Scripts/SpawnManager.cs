using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerUpContainer;
    [SerializeField]
    private GameObject _trippleShotPowerUp;
    [SerializeField]
    private GameObject _speedPowerUp;
    [SerializeField]
    private GameObject _shieldPowerUp;
    private bool _stopSpawn = false;

    private Vector2 _spawnPos;

    private void Start()
    {
        if (_enemy == null) Debug.Log("Enemy on SpawnManager is NULL");
        if (_enemyContainer == null) Debug.Log("Enemy container on SpawnManager is NULL");
        if (_powerUpContainer == null) Debug.Log("_powerUpContainer on SpawnManager is NULL");
        if (_trippleShotPowerUp == null) Debug.Log("_trippleShotPowerUp on SpawnManager is NULL");
        if (_speedPowerUp == null) Debug.Log("_speedPowerUp on SpawnManager is NULL");
        if (_shieldPowerUp == null) Debug.Log("_shieldPowerUp on SpawnManager is NULL");
    }

    public void StopSpawn()
    {
        _stopSpawn = true;
    }

    IEnumerator SpawnEnemyRoutine()
    {
        
        while (_stopSpawn == false)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            GameObject newGameObject = Instantiate(_enemy, _spawnPos, Quaternion.identity);
            newGameObject.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.5f);
        }
    }
    IEnumerator SpawnTripplePowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(7f, 10f));
        while (_stopSpawn == false)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            GameObject newGameObject = Instantiate(_trippleShotPowerUp, _spawnPos, Quaternion.identity);
            newGameObject.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(7f,20f));
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
    void Restart () 
    { 
        SceneManager.LoadScene(1); //current scene
    }

    public void StartWave() 
    {
        StartCoroutine(StartWaveRoutine());
    }

    IEnumerator StartWaveRoutine()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripplePowerUpRoutine());
        StartCoroutine(SpawnSpeedPowerUpRoutine());
        StartCoroutine(SpawnShieldPowerUpRoutine());
    }
}
