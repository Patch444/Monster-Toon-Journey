using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSlime : MonoBehaviour
{
    public GameManager gm;
    public PlayerMove pm;
    public Animator anim;
    public PlayerShieldSlime pSS;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = this.GetComponent<Animator>();
        pSS = GameObject.Find("PlayerShieldSlime").GetComponent<PlayerShieldSlime>();
        Spawn();


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
    /*
    public void CleanUp()
    {
        Destroy(gameObject);
    }
    */


    public IEnumerator Spawn()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        anim.Play("Slime_Shield_Spawn");
        yield return new WaitForSecondsRealtime(.24f);
        //anim.Play("Slime_Shield_Idle");
    }

    public void CleanUp()
    {
        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerMove>().hasShieldSlime == false && pm.beenHit == false)
        {
            pm.hasShieldSlime = true;
            pm.Audio.clip = pm.shieldSlimeUp;
            pm.Audio.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            pSS.sourceSlime = this.gameObject;
            pSS.anim.SetBool("startNew", true);
            //Invoke("CleanUp", 2);
        }
    }

}
