using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static utils.Configs;
using UnityEngine.SceneManagement;

public class CharactersSwitch : MonoBehaviour
{
    GameObject girl;
    GameObject boy;
    int currentCharacter;
    Scene currentScene;
    
    void Start()
    {
        girl = GameObject.Find("GirlCharacter");
        boy = GameObject.Find("BoyCharacter");

        currentCharacter = 1;
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "GameScene")
        {
            boy.GetComponentInChildren<Canvas>().enabled = false;
        }
        SetCharacterActive(boy, false);
    }

    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentCharacter = (currentCharacter + 1) % 2;
            bool isGirl = currentCharacter == 0;
           
            if (isGirl && currentScene.name == "GameScene")
            {
                girl.GetComponent<Character>().blood.RemoveBlood();
            }
            else
            {
                boy.GetComponent<Character>().blood.RemoveBlood();
            }

            girl.GetComponent<Character>().enabled = !isGirl;
            girl.GetComponentInChildren<Camera>().enabled = !isGirl;
            if (currentScene.name == "GameScene")
            {
                girl.GetComponentInChildren<Canvas>().enabled = !isGirl;
                boy.GetComponentInChildren<Canvas>().enabled = isGirl;
            }
                boy.GetComponent<Character>().enabled = isGirl;
            boy.GetComponentInChildren<Camera>().enabled = isGirl;
           
            SetCharacterActive(boy, isGirl);
            SetCharacterActive(girl, !isGirl);
        }
  	}

    private void SetCharacterActive(GameObject character, bool active) 
    {
        int scaleFactor = active? 1 : 0;
        character.transform.localScale = new Vector3(scaleFactor, 1, scaleFactor);
    } 
}
