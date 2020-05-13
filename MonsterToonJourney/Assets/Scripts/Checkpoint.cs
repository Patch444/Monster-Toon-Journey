using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerMove player;
    public bool beenReached;
    // Start is called before the first frame update
    void Start()
    {
        beenReached = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && beenReached == false)
        {
            player.spawnPosition = this.transform.position;
            beenReached = true;
        }
    }
}
