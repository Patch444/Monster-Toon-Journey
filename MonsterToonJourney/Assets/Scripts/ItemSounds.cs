using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSounds : MonoBehaviour
{
    public AudioSource Audio;

    public AudioClip boxGrab;
    public AudioClip boxPlace;
    public AudioClip keyGrab;
    public AudioClip keyUse;
    public AudioClip shieldSlimeUp;
    public AudioClip shieldSlimeDown;
    public AudioClip blanketGrab;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
