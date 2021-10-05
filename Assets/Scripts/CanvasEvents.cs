using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasEvents : MonoBehaviour
{
    public GameObject letter, dialogueText, lyre, lyreSmall, continueText, oldMan, player;
    string[] dialogue = new string[]
    {
        "You finally made it here.",
        "You wanted to play the lyre, yes?",
        "It is a good desire, and you are finally ready.",
        "You have built up your body to be worthy of the responsibility of music.",
        "Meanwhile, I am old, and this lyre has become mere company to me.",
        "I will give it to you. I expect you will pick up its magic soon enough.",
        "You will now forge your own path. Or rather...",
        "You will take the path that the lyre shows you.",
        "That is all. Take this, and go forth."
    };
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

    void ActivateLetter()
    {
        letter.SetActive(true);
        continueText.SetActive(true);
    }

    void ActivateLyre()
    {
        lyre.SetActive(true);
        continueText.SetActive(true);
    }

    void StartDialogue()
    {
        dialogueText.SetActive(true);
        continueText.SetActive(true);
    }

    void Deactivate()
    {
        if (letter.activeSelf)
        {
            letter.SetActive(false);
            continueText.SetActive(false);
            player.SendMessage("AllowMove");
        }
        else if (lyre.activeSelf)
        {
            lyre.SetActive(false);
            continueText.SetActive(false);
            player.SendMessage("AllowMove");
        }
        else if (dialogueText.activeSelf)
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
            else dialogueText.GetComponent<TextMeshProUGUI>().text = dialogue[diaIndex];
        }
    }
}
