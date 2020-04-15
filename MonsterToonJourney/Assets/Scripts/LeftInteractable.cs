using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftInteractable : MonoBehaviour
{
    GameManager gm;
    public bool canInteract;
    private PlayerMove pm;
    public GameObject box;
    private Image boxIcon;
    private GameObject fm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        boxIcon = GameObject.Find("Box Icon").GetComponent<Image>();
        fm = GameObject.Find("Fear Meter");
    }

    // Update is called once per frame
    void Update()
    {
        //if the player presses E in range set their hasBox and destroy self
        if (Input.GetKeyDown(KeyCode.E) && canInteract && !pm.hasBox && pm.lastDirection == 1)
        {
            //Debug.Log("Player picked up a box");
            pm.hasBox = true;
            pm.Audio.clip = pm.boxGrab;
            pm.Audio.Play();
            boxIcon.enabled = true;
            boxIcon.transform.SetParent(fm.transform);
            Destroy(box);
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
