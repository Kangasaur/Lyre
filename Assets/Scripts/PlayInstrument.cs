using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayInstrument : MonoBehaviour
{
    public float speed = 3;
    public GameObject canvas;
    public AudioSource voice1, voice2, voice3, voice4, song, echo;
    public AudioClip g2, a2, b2, c3, d3, e3, f3, g3, pickup;
    int voice = 0;
    bool hasLyre = false;
    bool canMove = false;
    bool isAtSpellPlace = false;
    List<AudioClip> playedNotes = new List<AudioClip>();
    AudioClip[] sunSong;
    Rigidbody2D myBody;
    Animator animator;
    float hmove, vmove;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sunSong = new AudioClip[] { g2, d3, c3, g3 };
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Room 2")
        {
            hasLyre = true;
            canMove = true;
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Environment"))
        {
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.y);
        }
    }

    void Update()
    {
        if (canMove) DoMovement();

        if (hasLyre) CheckInstrumentInput();
        transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
    }

    void DoMovement()
    {
        hmove = Input.GetAxisRaw("Horizontal") * speed;
        vmove = Input.GetAxisRaw("Vertical") * speed;
        myBody.velocity = new Vector2(hmove, vmove);
        animator.SetFloat("hdir", hmove);
        animator.SetFloat("vdir", vmove);
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

        if (note == g2) playedNotes.Clear();
        playedNotes.Add(note);
        if (playedNotes.ToArray().SequenceEqual(sunSong) && isAtSpellPlace)
        {
            GameObject stone = GameObject.Find("Spell stone");
            stone.GetComponent<AudioSource>().Play();
            stone.GetComponent<Animator>().SetBool("isActivated", true);
            GameObject door = GameObject.Find("Door");
            door.GetComponent<Animator>().SetBool("open", true);
            door.GetComponent<AudioSource>().Play();
            door.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void AllowMove()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.name)
        {
            case "old man":
                canMove = false;
                animator.SetFloat("hdir", 0f);
                animator.SetFloat("vdir", 0f);
                myBody.velocity = Vector2.zero;
                collision.gameObject.GetComponent<AudioSource>().loop = false;
                collision.gameObject.GetComponent<AudioSource>().volume = 0.6f;
                foreach (AudioSource source in collision.gameObject.GetComponentsInChildren<AudioSource>()) source.Stop();
                Destroy(GameObject.Find("Sound trigger"));
                canvas.SendMessage("StartDialogue");
                break;
            case "Lyre Item":
                canvas.SendMessage("ActivateLyre");
                hasLyre = true;
                voice4.clip = pickup;
                voice4.Play();
                animator.SetBool("hasLyre", true);
                animator.SetFloat("hdir", 0f);
                animator.SetFloat("vdir", 0f);
                myBody.velocity = Vector2.zero;
                Destroy(collision.gameObject);
                break;
            case "Room Portal":
                SceneManager.LoadScene(1);
                break;
            case "Spell place":
                if (hasLyre)
                {
                    isAtSpellPlace = true;
                    GameObject.Find("Prompt").GetComponent<SpriteRenderer>().enabled = true;
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Sound trigger")
        {
            if (transform.position.x < other.transform.position.x)
            {
                song.maxDistance = 46.3f;
                echo.maxDistance = 21f;
            }
            else
            {
                song.maxDistance = 74.1f;
                echo.maxDistance = 44.25f;
            }
        }
        else if (other.gameObject.name == "Spell place")
        {
            isAtSpellPlace = false;
            GameObject.Find("Prompt").GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
