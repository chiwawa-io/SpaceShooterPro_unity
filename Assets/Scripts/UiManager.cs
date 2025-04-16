using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreNumber;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _shieldSprites;
    [SerializeField]
    private Image _shieldsImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _roundWonText;
    [SerializeField]
    private Text _restartText;
    [SerializeField] 
    private Text _ammoText;
    [SerializeField]
    private Image _sprint;

    [SerializeField]
    private Image _bossHpBarImage;
    [SerializeField] 
    private Sprite[] _bossHpLineSprites;

    [SerializeField]
    private int _ammoCount = 30;
    private int _scoreNum;

    [SerializeField]
    private bool _bossMode = false;

    void Start()
    {
        if (_liveSprites == null) Debug.Log("livesprites on Ui manager is null!");
        if (_gameOverText == null) Debug.Log("_gameOverText on Ui manager is null!");
        if (_restartText == null) Debug.Log("_restartText on UI manager is null!");
        if (_sprint == null) Debug.Log("_sprint on UI manager is null!");
        if (!_bossMode && _scoreNumber == null) Debug.Log("_scoreText on UI manager is null!");
        if (!_bossMode && _ammoText == null) Debug.Log("_ammoText on UI manager is null!");
    }


    public void ScoreUpdate()
    {
        _scoreNum += 10;
        _scoreNumber.text = "" + _scoreNum;
        if (_scoreNum.ToString().Length == 2) _scoreNumber.transform.localPosition = new Vector3(13.34f, 199.59f, 0);
        else if (_scoreNum.ToString().Length == 3) _scoreNumber.transform.localPosition = new Vector3(6.5f, 199.59f, 0);
        else if (_scoreNum.ToString().Length == 4) _scoreNumber.transform.localPosition = new Vector3(1.06f, 199.59f, 0);

    }

    public void BigScoreUpdate()
    {
        _scoreNum += 30;
        _scoreNumber.text = "" + _scoreNum;
        if (_scoreNum.ToString().Length == 2) _scoreNumber.transform.localPosition = new Vector3(13.34f, 199.59f, 0);
        else if (_scoreNum.ToString().Length == 3) _scoreNumber.transform.localPosition = new Vector3(7.08f, 199.59f, 0);
        else if (_scoreNum.ToString().Length == 4) _scoreNumber.transform.localPosition = new Vector3(1.06f, 199.59f, 0);
    }

    public void LivesUpdate(int currentLives) {
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
        }
    }
    public void AmmoUpdate(int ammoNumber) 
    {
        _ammoCount = ammoNumber;
        _ammoText.text = "" + _ammoCount;
    }
    public void SprintOn()
    {
        _sprint.gameObject.SetActive(true);
    }
    public void SprintOff() 
    {
        _sprint.gameObject.SetActive(false);
    }
    public void ShieldsUpdate(int currentShields)
    {
        _shieldsImg.sprite = _shieldSprites[currentShields];
    }
    public void BossHpUpdate(int currentHpPercentage)
    {
        _bossHpBarImage.sprite = _bossHpLineSprites[currentHpPercentage];
    }
    public void RoundWon ()
    {
        _roundWonText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
    }
}

    

