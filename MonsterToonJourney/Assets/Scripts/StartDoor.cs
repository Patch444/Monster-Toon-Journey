using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDoor : MonoBehaviour
{
    public GameManager gm;
    public Animator anim;
    public AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = this.GetComponent<Animator>();
        Audio = this.GetComponent<AudioSource>();
        //Audio.volume = PlayerPrefs.GetFloat("FxVolume");
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
}
