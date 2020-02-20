using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLaunch : MonoBehaviour
{
    GameManager gm;
    public float upSpeed = .05f;
    public float downSpeed = .05f;
    public float upTimer;
    public float downTimer;
    public float thrust = 9.0f;

    public bool movingUp;
    public bool movingDown;
    private Rigidbody2D boxBody;
    public Vector2 startPosition;

    public AudioClip BoxHit;
    private AudioSource Audio;
    private GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        box = this.gameObject;
        startPosition = transform.position;
        boxBody = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!gm.isPaused)
        {
            if (movingUp)
            {
                upTimer = upTimer + Time.deltaTime;
                transform.Translate(Vector2.up * upSpeed);
            }
            if (upTimer >= .8f && movingUp)
            {
                movingUp = false;
                upTimer = 0f;
                movingDown = true;
            }
            if (movingDown)
            {
                downTimer = downTimer + Time.deltaTime;
                transform.Translate(Vector2.down * downSpeed);
            }
            if (downTimer >= .8f && movingDown)
            {
                movingDown = false;
                downTimer = 0f;
            }
        }*/
    }

    public void Launch()
    {
        boxBody.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        //movingUp = true;
        Audio.clip = BoxHit;
        Audio.Play();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //when player is on box increase thrust
        if (other.tag == "Player")
        {
            thrust = 19.0f;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //when player is off box decrease thrust
        if (other.tag == "Player")
        {
            thrust = 9.0f;
        }
    }
    public void BoxReset()
    {
        transform.position = startPosition;
        this.gameObject.SetActive(false);
    }
}
