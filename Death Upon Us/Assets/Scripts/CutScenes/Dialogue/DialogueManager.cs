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

    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;

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
        if(currentIndex > currentConvo.GetLength())
        {
            Destroy(nextButton);
            playButton.text = "Play";
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
        if(currentIndex > 1)
        {
            Switch();
        }
        dialogue.text = currentConvo.GetLineByIndex(currentIndex).dialogue;
        speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
        currentIndex++;
    }

    void Switch()
    {
        currentCharacter = (currentCharacter + 1) % 2;
        bool isGirl = currentCharacter == 0;

        girl.GetComponentInChildren<Camera>().enabled = !isGirl;
        boy.GetComponentInChildren<Camera>().enabled = isGirl;
    }
}
