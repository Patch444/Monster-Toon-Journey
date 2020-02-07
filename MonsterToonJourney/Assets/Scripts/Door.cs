using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameManager gm;
    public PlayerMove pm;

    public float timer;

    public Animator anim;

    public AudioSource sfx;

    public int sfxIndex;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        anim = this.GetComponent<Animator>();
        timer = 0;
        sfxIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;
        }
        if(sfxIndex == 1)
        {
            sfx.Play();
            sfxIndex++;
        }
    }

    public void StartTimer()
    {
        timer = timer + Time.deltaTime;
    }

    public void Transition()
    {
        StartTimer();
        pm.isByDoor = true;
        anim.Play("Door Open");
        if (timer >= 1)
        {
            gm.AdvanceLevel();
        }
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !gm.isPaused)
        {
            Transition();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !gm.isPaused)
        {
            sfxIndex++;
        }
    }
}
