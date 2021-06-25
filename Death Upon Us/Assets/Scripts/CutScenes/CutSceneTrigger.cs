using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static utils.Configs;

public class CutSceneTrigger : MonoBehaviour
{
    public GameObject cutSceneCamera;
    public GameObject gameWinMenuUI;
    public GameObject backgroundMusic;
    Character boy;
    Character girl;
    public void Start()
    {
        boy = GameObject.Find("BoyCharacter").GetComponent<Character>();
        girl = GameObject.Find("GirlCharacter").GetComponent<Character>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (girl.GetHasMap() && boy.GetHasMapBoy())
            {
                cutSceneCamera.SetActive(true);
                StartCoroutine(DisableCharacters(collider));
                StartCoroutine(ShowGameWinScreen());
                DespawnAllMonsters();
            }
        }
    }

    public void DespawnAllMonsters()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        for (var i = 0; i < monsters.Length; i++)
            Destroy(monsters[i]);
    }

    public IEnumerator DisableCharacters(Collider collider)
    {
        yield return new WaitForSeconds(0.1f);
        collider.GetComponent<Character>().GetComponentInChildren<Canvas>().enabled = false;
    }

    public IEnumerator ShowGameWinScreen()
    {
        yield return new WaitForSeconds(10f);
        gameWinMenuUI.SetActive(true);
        Cursor.visible = true;
        backgroundMusic.SetActive(false);
    }
}

