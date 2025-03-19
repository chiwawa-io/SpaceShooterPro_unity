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
    private GameObject[] _commonPowerUpArray;
    [SerializeField]
    private GameObject _ammoPowerUp;
    [SerializeField]
    private GameObject[] _rarePowerUpArray;

    private bool _stopSpawn = false;

    private Vector2 _spawnPos;

    private int _selectPowerUp;

    private void Start()
    {
        if (_enemy == null) Debug.Log("Enemy on SpawnManager is NULL");
        if (_enemyContainer == null) Debug.Log("Enemy container on SpawnManager is NULL");
        if (_powerUpContainer == null) Debug.Log("_powerUpContainer on SpawnManager is NULL");
        if (_rarePowerUpArray == null) Debug.Log("_rarePowerUpArray on SpawnManager is NULL");
        if (_commonPowerUpArray == null) Debug.Log("_commonPowerUpArray on SpawnManager is NULL");
        if (_ammoPowerUp == null) Debug.Log("_ammoPowerUp on SpawnManager is NULL");

        Mathf.Clamp(_selectPowerUp, 0, 1);
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
    IEnumerator SpawnCommonPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(7f, 10f));
        while (_stopSpawn == false)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            _selectPowerUp = Random.Range(0, 2);
            GameObject newGameObject = Instantiate(_commonPowerUpArray[_selectPowerUp], _spawnPos, Quaternion.identity);
            newGameObject.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(5f,7f));
        }
    }

    
    IEnumerator SpawnRarePowerUpRoutine()
    {
        yield return new WaitForSeconds(20f);
        while (_stopSpawn == false)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            _selectPowerUp = Random.Range(0, 2);
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
    }

    public void SpawnAmmoSuply()
    {
        _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
        GameObject newGameObject = Instantiate(_ammoPowerUp, _spawnPos, Quaternion.identity);
        newGameObject.transform.parent = _powerUpContainer.transform;
    }

}
