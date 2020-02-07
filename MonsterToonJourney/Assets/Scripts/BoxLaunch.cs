using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLaunch : MonoBehaviour
{
    public float thrust = 9.0f;
    private Rigidbody2D boxBody;
    public AudioClip BoxHit;
    private AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        boxBody = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        boxBody.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        Audio.clip = BoxHit;
        Audio.Play();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            thrust = 19.0f;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            thrust = 9.0f;
        }
    }
}
