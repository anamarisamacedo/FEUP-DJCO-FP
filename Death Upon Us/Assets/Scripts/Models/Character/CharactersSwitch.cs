using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static utils.Configs;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CharactersSwitch : MonoBehaviour
{
    GameObject girl;
    GameObject boy;
    int currentCharacter;
    Scene currentScene;
    bool isGirl;
    private string GeneratedCode;

    void Start()
    {
        girl = GameObject.Find("GirlCharacter");
        boy = GameObject.Find("BoyCharacter");
        GeneratedCode = GenerateRandomCode(4);

        currentCharacter = 1;
        isGirl = currentCharacter == 0;

        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "SampleScene")
        {
            girl.GetComponent<Character>().SetGeneratedCode(GeneratedCode);
            boy.GetComponent<Character>().SetGeneratedCode(GeneratedCode);
            boy.GetComponentInChildren<Canvas>().enabled = false;
        }
        SetCharacterActive(isGirl);
    }

    void Update()
    {
        if (currentScene.name == "SampleScene")
        {
            if (girl.GetComponent<Character>().inputEnabled && boy.GetComponent<Character>().inputEnabled)
                if (Input.GetKeyDown(KeyCode.X))
                {
                    currentCharacter = (currentCharacter + 1) % 2;
                    isGirl = currentCharacter == 0;

                    if (isGirl && currentScene.name == "SampleScene")
                    {
                        girl.GetComponent<Character>().blood.RemoveBlood();
                    }
                    else
                    {
                        boy.GetComponent<Character>().blood.RemoveBlood();
                    }

                    girl.GetComponent<Character>().enabled = !isGirl;
                    girl.GetComponentInChildren<Camera>().enabled = !isGirl;
                    if (currentScene.name == "SampleScene")
                    {
                        girl.GetComponentInChildren<Canvas>().enabled = !isGirl;
                        boy.GetComponentInChildren<Canvas>().enabled = isGirl;
                    }
                    boy.GetComponent<Character>().enabled = isGirl;
                    boy.GetComponentInChildren<Camera>().enabled = isGirl;
                    SetCharacterActive(!isGirl);
                }
        }
    }

    private void SetCharacterActive(bool girl)
    {
        if (girl)
        {
            this.boy.transform.localScale = new Vector3(0f, 1f, 0f);
            this.girl.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
        else
        {
            this.boy.transform.localScale = new Vector3(1f, 1f, 1f);
            this.girl.transform.localScale = new Vector3(0f, 2.5f, 0f);
        }
    }

    public static string GenerateRandomCode(int length)
    {
        string myString = "";
        for (int i = 0; i < length; i++)
        {
            myString += Chars[Random.Range(0, Chars.Length)];
        }
        return myString;
    }
}