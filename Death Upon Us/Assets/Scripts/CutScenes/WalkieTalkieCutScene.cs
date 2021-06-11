using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static utils.Configs;

public class WalkieTalkieCutScene : MonoBehaviour
{

    private CharacterState state;
    public Text textElement;
    public string message;
    private string fullText = "Hello??.... Somebody there?....Hello?.....";
    public float delay = 0.1f;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod(MonoBehaviour handle)
    {
        Debug.Log("CS");
       //StartCoroutine(Dialogue());
    }

    IEnumerator Dialogue()
    {
         for (int i = 0; i < fullText.Length; i++)
         {
             DisplayMessage(fullText.Substring(0, i));
             yield return new WaitForSeconds(delay);
         }
        
    }

    public void DisplayMessage(string new_message)
    {
        message = new_message;
    }
}