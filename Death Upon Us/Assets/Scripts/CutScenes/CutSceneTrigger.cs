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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
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

    }
}

