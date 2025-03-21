﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject _powerUpContainer;
    [SerializeField]
    private GameObject[] _powerUpArray;
    private bool _stopSpawn = false;
    private int _selectPowerUp = 0;

    private Vector2 _spawnPos;

    void Start()
    {
        StartWave();

        Mathf.Clamp(_selectPowerUp, 0, 2);
    }

    public void StopSpawn()
    {
        _stopSpawn = true;
    }


    
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(7f, 10f));
        while (_stopSpawn == false)
        {
            _spawnPos = new Vector2(Random.Range(-8f, 8f), 7f);
            _selectPowerUp = Random.Range(0, 3);
            GameObject newGameObject = Instantiate(_powerUpArray[_selectPowerUp], _spawnPos, Quaternion.identity);
            newGameObject.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 7f));
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

        StartCoroutine(SpawnPowerUpRoutine());
    }
}
