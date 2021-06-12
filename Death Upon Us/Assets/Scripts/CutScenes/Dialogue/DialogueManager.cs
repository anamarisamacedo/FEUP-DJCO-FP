using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogue;
    public Image speakerSprite;
    public Button nextButton;
    public Text playButton;
    GameObject girl;
    GameObject boy;
    int currentCharacter;
    private Coroutine typing;

    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    Scene currentScene;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        girl = GameObject.Find("GirlCharacter");
        boy = GameObject.Find("BoyCharacter");

        currentCharacter = 1;
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentConvo != null)
        {
            if (currentIndex > currentConvo.GetLength())
            {
                if (currentScene.name != "SampleScene")
                {
                    Destroy(nextButton);
                    playButton.text = "Play";
                }
            }
        }
    }

    public static void StartConversation(Conversation convo)
    {
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.dialogue.text = "";

        instance.ReadNext();
    }

    public void ReadNext()
    {
        Character character = girl.GetComponent<Character>();

        if (currentScene.name != "SampleScene")
        {
            if (currentIndex > 1)
            {
                Switch();
            }
        }

        if (currentIndex > currentConvo.GetLength())
        {
            if (currentScene.name == "SampleScene")
            {
                character.CloseDialogue();
            }
        }
        else
        {

            if (typing == null)
            {
                typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
            }
            else
            {
                instance.StopCoroutine(typing);
                typing = null;
                typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
            }

            speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();

            currentIndex++;
        }
    }

    void Switch()
    {
        currentCharacter = (currentCharacter + 1) % 2;
        bool isGirl = currentCharacter == 0;

        girl.GetComponentInChildren<Camera>().enabled = !isGirl;
        boy.GetComponentInChildren<Camera>().enabled = isGirl;
    }

    private IEnumerator TypeText(string text)
    {
        dialogue.text = "";
        bool complete = false;
        int index = 0;

        while (!complete)
        {
            dialogue.text += text[index];
            index++;
            yield return new WaitForSeconds(0.04f);

            if(index == text.Length)
            {
                complete = true;
            }
        }
    }
}
