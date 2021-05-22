using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static utils.Configs;

public class PauseMenu : MonoBehaviour
{
    private bool active;

    private void Start()
    {
        active = false;
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchVisibleStatus();
        }
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MenuScene);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void SwitchVisibleStatus()
    {
        if (active)
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            active = !active;
    }
}