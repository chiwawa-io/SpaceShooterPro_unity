using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyWithLasers;
    [SerializeField]
    private GameObject _smartEnemy;
    [SerializeField] 
    private float _enemySpawnInterval = 3f;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerUpContainer;
    [SerializeField]
    private GameObject[] _commonPowerUpArray;
    [SerializeField]
    private GameObject _ammoPowerUp;
    [SerializeField]
    private GameObject[] _rarePowerUpArray;

    private bool _stopSpawn = false;

    private Vector2 _spawnPos;

    private int _selectPowerUp;
    private float _enemyCount = 1;


    private void Start()
    {
        if (_enemy == null) Debug.Log("Enemy on SpawnManager is NULL");
        if (_enemyContainer == null) Debug.Log("Enemy container on SpawnManager is NULL");
        if (_powerUpContainer == null) Debug.Log("_powerUpContainer on SpawnManager is NULL");
        if (_rarePowerUpArray == null) Debug.Log("_rarePowerUpArray on SpawnManager is NULL");
        if (_commonPowerUpArray == null) Debug.Log("_commonPowerUpArray on SpawnManager is NULL");
        if (_ammoPowerUp == null) Debug.Log("_ammoPowerUp on SpawnManager is NULL");


    }

    public void StopSpawn()
    {
        _stopSpawn = true;
    }

    IEnumerator SpawnEnemyRoutine()
    {
        
        while (_stopSpawn == false)
        {
            _enemyCount += 0.5f;
            _enemySpawnInterval += 0.3f;
            Debug.Log("Enemy count: " + _enemyCount);
            for (float i = 0; i < _enemyCount; i++)
            {
                _spawnPos = new Vector2(Random.Range(-8f, 8f), Random.Range(7f, 11f));
                GameObject newGameObject = Instantiate(_enemy, _spawnPos, Quaternion.identity);
                newGameObject.transform.parent = _enemyContainer.transform;
            }
            yield return new WaitForSeconds(_enemySpawnInterval);
            
        }
    }

    IEnumerator SpawnEnemyWithLasersRoutine()
    {
        while (!_stopSpawn)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), Random.Range(7f, 11f));
            Instantiate(_enemyWithLasers, _spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator SpawnSmartEnemies()
    {
        while (!_stopSpawn) 
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), Random.Range(7f, 11f));
            Instantiate(_smartEnemy, _spawnPos, Quaternion.Euler(0, 0, 180f));
            yield return new WaitForSeconds(15f);
        }
    }

    IEnumerator SpawnCommonPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        while (!_stopSpawn)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            _selectPowerUp = Random.Range(0,2);
            GameObject newGameObject = Instantiate(_commonPowerUpArray[_selectPowerUp], _spawnPos, Quaternion.identity);
            newGameObject.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(5f,7f));
        }
    }

    
    IEnumerator SpawnRarePowerUpRoutine()
    {
        yield return new WaitForSeconds(20f);
        while (!_stopSpawn)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            _selectPowerUp = Random.Range(0, 4);
            GameObject newGameObject = Instantiate(_rarePowerUpArray[_selectPowerUp], _spawnPos, Quaternion.identity);
            newGameObject.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(15f, 20f));
        }
    }

    public void StartWave() 
    {
        StartCoroutine(StartWaveRoutine());
    }

    IEnumerator StartWaveRoutine()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnCommonPowerUpRoutine());
        StartCoroutine (SpawnRarePowerUpRoutine());
        StartCoroutine(SpawnEnemyWithLasersRoutine());
        StartCoroutine(SpawnSmartEnemies());
    }

    public void SpawnAmmoSuply()
    {
        _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
        GameObject newGameObject = Instantiate(_ammoPowerUp, _spawnPos, Quaternion.identity);
        newGameObject.transform.parent = _powerUpContainer.transform;
    }

}
