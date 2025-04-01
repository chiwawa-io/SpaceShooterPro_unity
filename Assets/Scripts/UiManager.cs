using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField] 
    private Text _ammoText;
    [SerializeField]
    private Image _sprint;
    [SerializeField]
    private int _ammoCount = 15;
    private int _scoreNum;

    void Start()
    {
        _scoreText.text = "Score: ";
        _ammoText.text = "Ammo: " + _ammoCount;

        if (_liveSprites == null) Debug.Log("livesprites on Ui manager is null!");
        if (_gameOverText == null) Debug.Log("_gameOverText on Ui manager is null!");
        if (_restartText == null) Debug.Log("_restartText on UI manager is null!");
        if (_sprint == null) Debug.Log("_sprint on UI manager is null!");
        if (_scoreText == null) Debug.Log("_scoreText on UI manager is null!");
        if (_ammoText == null) Debug.Log("_ammoText on UI manager is null!");

    }

    public void ScoreUpdate()
    {
        _scoreNum += 10;
        _scoreText.text = "Score: " + _scoreNum;
    }

    public void BigScoreUpdate()
    {
        _scoreNum += 30;
        _scoreText.text = "Score: " + _scoreNum;
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
        _ammoText.text = "Ammo: " + _ammoCount;
    }
    public void SprintOn()
    {
        _sprint.gameObject.SetActive(true);
    }
    public void SprintOff() 
    {
        _sprint.gameObject.SetActive(false);
    }
}

    

