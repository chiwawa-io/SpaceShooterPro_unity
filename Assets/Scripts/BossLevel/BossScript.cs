using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _mobs;
    [SerializeField]
    private GameObject _hugeBlowUp;
    [SerializeField]
    private GameObject _laserWave;
    [SerializeField]
    private GameObject _drifMobLeft;
    [SerializeField]
    private GameObject _drifMobRight;
    [SerializeField]
    private GameObject _superLasersWave;
    [SerializeField]
    private GameObject _superMobsWave;
    [SerializeField]
    private int _health = 1000;
    private CanvasScript _canvasScript;

    void Start()
    {
        _canvasScript = GameObject.Find("Canvas").GetComponent<CanvasScript>();

        StartCoroutine(WavesSpawnRoutine());
        StartCoroutine(LaserWave());

        if (_canvasScript == null) Debug.Log("CanvasScript is null on Boss");
    }

    
    void Update()
    {

        if (_health <= 0) DestroyingBoss();
    }



    void DestroyingBoss ()
    {
        Instantiate(_hugeBlowUp, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _health--;
            _canvasScript.BossHpUpdate();
        }
        if (collision.tag == "laser")
        {
            _health--;
            _canvasScript.BossHpUpdate();
        }
    }


    IEnumerator WavesSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 15f));
            if (_health>800) Instantiate(_mobs);
            else Instantiate(_superMobsWave);
        }
    }

    IEnumerator LaserWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 15f));
            Instantiate(_laserWave);
            yield return new WaitForSeconds(2f);
            Instantiate(_drifMobLeft);
            Instantiate(_drifMobRight);
            if (_health < 800) Instantiate(_superLasersWave);
        }
    }



}
