using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorLocked : MonoBehaviour
{
    GameManager gm;
    private PlayerMove pm;
    private Image keyIcon;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        keyIcon = GameObject.Find("Key Icon").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && pm.hasKey)
        {
            // Will play door opening animation.
            this.gameObject.SetActive(false);
            pm.hasKey = false;
            pm.Audio.clip = pm.keyUse;
            pm.Audio.Play();
            keyIcon.enabled = false;
        }
    }
}
