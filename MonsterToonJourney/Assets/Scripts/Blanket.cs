using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Blanket : MonoBehaviour
{
    GameManager gm;
    public bool canInteract;
    private PlayerMove pm;
    private Image blanketIcon;
    private GameObject fm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        fm = GameObject.Find("Fear Meter");
        blanketIcon = GameObject.Find("BlanketIcon").GetComponent<Image>();
        blanketIcon.enabled = false;
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

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            //if the player presses E in range set their hasBox and destroy self
            if (Input.GetKeyDown(KeyCode.E) && canInteract )//&& !pm.hasBlanket)
            {
                //Debug.Log("Player picked up a shield");
                // pm.hasBlanket = true;
                // pm.Audio.clip = pm.blanketGrab;
                // pm.Audio.Play();
                blanketIcon.enabled = true;
                blanketIcon.transform.SetParent(fm.transform);

                Destroy(this.gameObject);
            }
        }
    }


}
