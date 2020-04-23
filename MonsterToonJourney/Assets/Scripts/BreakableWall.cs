using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public AudioClip[] smashSounds = new AudioClip[2];
    public GameObject wallSounds;

    public AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        Audio = wallSounds.GetComponent<AudioSource>();
        //Audio.volume = PlayerPrefs.GetFloat("FxVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WallBreak()
    {
        //pick a break sound
        //int index = Random.Range(0, smashSounds.Length);
        //Audio.clip = smashSounds[index];
        Audio.Play();
        //disable collider
        //this.GetComponent<BoxCollider2D>().enabled = false;

        //temp function
        Destroy(this.gameObject);
    }
}
