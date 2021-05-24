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
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.C)){
            currentCharacter = (currentCharacter + 1) % 2;

            //girl character
            if (currentCharacter == 0){
                girl.GetComponent<Character>().enabled = false;
                girl.GetComponentInChildren<Camera>().enabled = false;
                girl.GetComponent<Character>().blood.RemoveBlood();
                boy.GetComponent<Character>().enabled = true;
                boy.GetComponentInChildren<Camera>().enabled = true;

                // change camera
            }
            //boy character
            else{
                boy.GetComponent<Character>().enabled = false;
                boy.GetComponentInChildren<Camera>().enabled = false;
                boy.GetComponent<Character>().blood.RemoveBlood();
                girl.GetComponent<Character>().enabled = true;
                girl.GetComponentInChildren<Camera>().enabled = true;
                // change camera
            }
        }
  	}
}
