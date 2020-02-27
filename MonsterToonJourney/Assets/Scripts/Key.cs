using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    GameManager gm;
    public bool canInteract;
    private PlayerMove pm;
    private Image keyIcon;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        keyIcon = GameObject.Find("Key Icon").GetComponent<Image>();
        keyIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            //if the player presses E in range set their hasBox and destroy self
            if (Input.GetKeyDown(KeyCode.E) && canInteract && !pm.hasShield)
            {
                //Debug.Log("Player picked up a shield");
                pm.hasKey = true;
                pm.Audio.clip = pm.keyGrab;
                pm.Audio.Play();
                keyIcon.enabled = true;
                Destroy(this.gameObject);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            canInteract = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //when player moves out of range reset canInteract
        if (other.tag == "Player")
        {
            canInteract = false;
        }
    }
}
