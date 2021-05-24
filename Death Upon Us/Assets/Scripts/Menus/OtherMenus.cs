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

        if (Input.GetKeyDown(KeyCode.G))
        {
            gameWinMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnHpHungerValueChanged(float newValue)
    {
        if (newValue == 0)
        {
            gameOverMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(MenuScene);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameScene);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }
}
