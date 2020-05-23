using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portcullis : MonoBehaviour
{
    private GameManager gm;
    public Animator anim;
    public AudioSource Audio;
    public AudioClip PortcullisOpen;
    public AudioClip PortcullisClose;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        anim = this.GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    }

    public void Rise()
    {
        anim.Play("Portcullis_Rise");
        this.GetComponent<BoxCollider2D>().enabled = false;
        Audio.clip = PortcullisOpen;
        Audio.Play();
    }

    public void Fall()
    {
        anim.Play("Portcullis_Fall");
        this.GetComponent<BoxCollider2D>().enabled = true;
        Audio.clip = PortcullisClose;
        Audio.Play();
    }
}
