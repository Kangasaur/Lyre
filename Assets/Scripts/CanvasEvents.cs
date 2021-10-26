using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasEvents : MonoBehaviour
{
    public GameObject dialogueText, lyre, lyreSmall, continueText, oldMan, player;
    string[] dialogue = new string[]
    {
        "You finally made it here.",
        "You wanted to play the lyre, yes?",
        "It is a good desire, and you are finally ready.",
        "You have built up your body to be worthy of the responsibility of music.",
        "Meanwhile, I am old, and this lyre has become mere company to me.",
        "I will give it to you.",
        "I expect you will pick up its magic soon enough.",
        "You will now forge your own path.",
        "Or rather, you will take the path that the lyre shows you.",
        "The Door of the Sun is close by. It will start your journey.",
        "That is all. Take this, and go forth."
    };
    public AudioClip[] dialogueSounds;
    int diaIndex = 0;

    private void Start()
    {
        dialogueText.GetComponent<TextMeshProUGUI>().text = dialogue[0];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Deactivate();
        }
    }

    void ActivateLyre()
    {
        lyre.SetActive(true);
    }

    void StartDialogue()
    {
        dialogueText.SetActive(true);
        continueText.SetActive(true);
        oldMan.GetComponent<AudioSource>().clip = dialogueSounds[diaIndex];
        oldMan.GetComponent<AudioSource>().Play();
    }

    void Deactivate()
    {
        if (dialogueText.activeSelf)
        {
            diaIndex++;
            if (diaIndex == dialogue.Length)
            {
                dialogueText.SetActive(false);
                continueText.SetActive(false);
                Destroy(oldMan);
                lyreSmall.SetActive(true);
                player.SendMessage("AllowMove");
            }
            else
            {
                oldMan.GetComponent<AudioSource>().clip = dialogueSounds[diaIndex];
                oldMan.GetComponent<AudioSource>().Play();
                dialogueText.GetComponent<TextMeshProUGUI>().text = dialogue[diaIndex];
            }
        }
    }
}
