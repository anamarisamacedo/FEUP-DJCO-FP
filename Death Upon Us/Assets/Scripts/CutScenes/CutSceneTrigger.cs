using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static utils.Configs;

public class CutSceneTrigger : MonoBehaviour
{
    public GameObject cutSceneCamera;
    public GameObject gameWinMenuUI;
    public Canvas boyCanvas, girlCanvas;
    public GameObject backgroundMusic;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.name);
        if (collider.CompareTag("Player") && (collider.name == "GirlCharacter"))
        {
            cutSceneCamera.SetActive(true);
            StartCoroutine(DisableCharacters());
            StartCoroutine(ShowGameWinScreen());
        }
    }

    public IEnumerator DisableCharacters()
    {
        yield return new WaitForSeconds(0.1f);
        boyCanvas.enabled = false;
        girlCanvas.enabled = false;
    }

    public IEnumerator ShowGameWinScreen()
    {
        yield return new WaitForSeconds(10f);
        gameWinMenuUI.SetActive(true);
        backgroundMusic.SetActive(false);
    }
}

