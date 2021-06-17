using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using static utils.Configs;
using UnityEngine.SceneManagement;

public class WalkieTalkieCutScene : MonoBehaviour
{

    private CharacterState state;
    private string walkieMessage = ". . . . . . . .";
    public Text textWalkie;
    public string messageWalkie;
    public float delayWalkie = 0.2f;
    public Conversation convo;

    private void Start()
    {
        StartCoroutine(Waking());
        StartCoroutine(Walkie());
        DialogueManager.StartConversation(convo);
    }

    private void Update()
    {
        textWalkie.text = messageWalkie;
    }

    IEnumerator Waking()
    {
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator Walkie()
    {
        for (int i = 0; i < walkieMessage.Length; i++)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Misc/Walkie-talkie");
            messageWalkie = walkieMessage.Substring(0, i);
            yield return new WaitForSeconds(delayWalkie);
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene(GameScene);
    }
}