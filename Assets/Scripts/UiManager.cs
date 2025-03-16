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
    private int _scoreNum;

    void Start()
    {
        _scoreText.text = "Score: ";
        if (_liveSprites == null) Debug.Log("livesprites on Ui manager is null!");
        if (_gameOverText == null) Debug.Log("_gameOverText on Ui manager is null!");
        if (_restartText == null) Debug.Log("_restartText on UI manager is null!");
    }

    public void ScoreUpdate()
    {
        _scoreNum += 10;
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
}

    

