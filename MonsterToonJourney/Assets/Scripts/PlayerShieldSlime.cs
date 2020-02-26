using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldSlime : MonoBehaviour
{
    public GameManager gm;
    public PlayerMove pm;
    public Animator anim;
    public GameObject sourceSlime;

    public bool hasEnveloped;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            anim.enabled = true;
            if (pm.hasShieldSlime == true && hasEnveloped == false)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                anim.Play("Slime_Shield_Envelope");
                hasEnveloped = true;
//                StartCoroutine(Envelope());
            }
            if (pm.isDead == true)
            {
                Disperse();
            }
        }
        else
        {
            anim.enabled = false;
        }
    }

    public IEnumerator Envelope()
    {
        hasEnveloped = true;
        anim.Play("Slime_Shield_Envelope");
        yield return new WaitForSecondsRealtime(.24f);
        anim.Play("Slime_Shield_Envelope_Idle");
    }

    public void Disperse()
    {
        anim.SetBool("wasUsed", true);
        anim.SetBool("startNew", false);
        //      anim.Play("Slime_Shield_Disperse");
        if (sourceSlime != null)
        {
            sourceSlime.GetComponent<ShieldSlime>().Invoke("CleanUp", 1.0f);
        }
    }


}
