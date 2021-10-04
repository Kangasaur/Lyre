using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInstrument : MonoBehaviour
{
    public float speed = 3;
    public AudioSource voice1, voice2, voice3, voice4;
    public AudioClip g2, a2, b2, c3, d3, e3, f3, g3;
    int voice = 0;
    bool hasLyre = true;

    Rigidbody2D myBody;
    float hmove, vmove;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DoMovement();

        if (hasLyre) CheckInstrumentInput();
    }

    void DoMovement()
    {
        hmove = Input.GetAxis("Horizontal") * speed;
        vmove = Input.GetAxis("Vertical") * speed;
        myBody.velocity = new Vector2(hmove, vmove);
    }

    void CheckInstrumentInput()
    {
        if (Input.GetKeyDown(KeyCode.F)) PlayNote(g2);
        if (Input.GetKeyDown(KeyCode.G)) PlayNote(a2);
        if (Input.GetKeyDown(KeyCode.H)) PlayNote(b2);
        if (Input.GetKeyDown(KeyCode.J)) PlayNote(c3);
        if (Input.GetKeyDown(KeyCode.K)) PlayNote(d3);
        if (Input.GetKeyDown(KeyCode.L)) PlayNote(e3);
        if (Input.GetKeyDown(KeyCode.Semicolon)) PlayNote(f3);
        if (Input.GetKeyDown(KeyCode.Quote)) PlayNote(g3);
    }

    void PlayNote(AudioClip note)
    {
        switch (voice)
        {
            case 0:
                voice1.clip = note;
                voice1.Play();
                break;
            case 1:
                voice2.clip = note;
                voice2.Play();
                break;
            case 2:
                voice3.clip = note;
                voice3.Play();
                break;
            case 3:
                voice4.clip = note;
                voice4.Play();
                break;

        }
        if (voice < 3) voice++;
        else voice = 0;
    }
}
