using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static utils.Configs;

public class OtherMenus : MonoBehaviour
{
    private static bool GameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    public GameObject gameWinMenuUI;
    public GameObject backgroundMusic;


    private void Start()
    {
        GameIsPaused = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void OnHpHungerValueChanged(float newValue)
    {
        if (newValue == 0)
        {
            gameOverMenuUI.SetActive(true);
            backgroundMusic.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        backgroundMusic.SetActive(false);
        SceneManager.LoadScene(MenuScene);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        backgroundMusic.SetActive(false);
        SceneManager.LoadScene(GameScene);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        backgroundMusic.SetActive(true);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        backgroundMusic.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
}
