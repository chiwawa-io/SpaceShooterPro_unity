using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    [SerializeField]
    private Text _bossHealth;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField] 
    private Sprite[] _shieldSprites;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Image _shieldsImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private BossLevelSpawn _bossLevelSpawn;
    private int _bossHp = 1500;


    void Start()
    {
        _bossHealth.text = "Boss HP: 2000";
        

        if (_bossLevelSpawn == null) Debug.Log("SpawnManager on Canvas is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        _bossHealth.text = "Boss HP: " + _bossHp;
    }

    public void LivesUpdate(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            _bossLevelSpawn.PlayerDead();
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
        }
    }
    public void BossHpUpdate()
    {
        _bossHp --;
    }

    public void ShieldsUpdate(int currentShields) 
    { 
        _shieldsImg.sprite= _shieldSprites[currentShields];
    }
}
