using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_manager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
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
}
