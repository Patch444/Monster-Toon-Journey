using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    private PlayerMove pm;
    public BoxCollider2D wallCollider;
    private GameManager gm;
    public Animator anim;
    public bool isShielded;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = this.GetComponent<Animator>();
        isShielded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShielded == false)
        {
            anim.Play("Flamethrower");
        }
        if (gm.isPaused == false)
        {
            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && pm.hasShield == true && pm.isShielding == false)
        {
            //pm.beenHit = true;
            gm.LoseLife();
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player" && pm.hasShield == true && pm.isShielding == true)
        {
            isShielded = true;
            // wallCollider.enabled = false;
            anim.Play("Flamethrower_Shielded");
        }
        else
        {
            //pm.beenHit = true;
            gm.LoseLife();
        }

    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && pm.hasShield)
        {
            isShielded = false;
        }
    }

}
