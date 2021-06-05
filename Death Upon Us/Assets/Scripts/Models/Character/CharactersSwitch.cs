using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static utils.Configs;

public class CharactersSwitch : MonoBehaviour
{
    GameObject girl;
    GameObject boy;
    int currentCharacter;
    
    void Start(){
        girl = GameObject.Find("GirlCharacter");
        boy = GameObject.Find("BoyCharacter");

        currentCharacter = 0;   
        boy.GetComponentInChildren<Canvas>().enabled = false;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.X)){
            currentCharacter = (currentCharacter + 1) % 2;

            //girl character
            if (currentCharacter == 0){
                girl.GetComponent<Character>().enabled = false;
                girl.GetComponentInChildren<Camera>().enabled = false;
                girl.GetComponentInChildren<Canvas>().enabled = false;
                girl.GetComponent<Character>().blood.RemoveBlood();
                boy.GetComponent<Character>().enabled = true;
                boy.GetComponentInChildren<Camera>().enabled = true;
                boy.GetComponentInChildren<Canvas>().enabled = true;
                SetCharacterActive(boy, true);
                SetCharacterActive(girl, false);
                // change camera
            }
            //boy character
            else{
                boy.GetComponent<Character>().enabled = false;
                boy.GetComponentInChildren<Camera>().enabled = false;
                boy.GetComponentInChildren<Canvas>().enabled = false;
                boy.GetComponent<Character>().blood.RemoveBlood();
                girl.GetComponent<Character>().enabled = true;
                girl.GetComponentInChildren<Camera>().enabled = true;
                girl.GetComponentInChildren<Canvas>().enabled = true;
                SetCharacterActive(boy, false);
                SetCharacterActive(girl, true);
                // change camera
            }
        }
  	}

    private void SetCharacterActive(GameObject character, bool active) {
        int scaleFactor = active? 1 : 0;
        character.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    } 
}
