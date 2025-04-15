using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_manager : MonoBehaviour
{
    private bool _playerDead = false;
    private int _sceneId;


    private void Start()
    {
        _sceneId = SceneManager.GetActiveScene().buildIndex;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && _playerDead == true) Restart();
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
    }
    public void PlayerDead() 
    {
        _playerDead= true;
    }
    public void NewGameButton () 
    {
        SceneManager.LoadScene(1);
    }

    public void BossLevelButton() 
    {
        SceneManager.LoadScene(2);
    }

    public void QuitButton() 
    { 
        Application.Quit();
    }

    void Restart() 
    { 
        SceneManager.LoadScene(_sceneId);
    }
}
