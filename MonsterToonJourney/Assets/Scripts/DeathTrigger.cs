using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public PlayerMove pm;
    public GameManager gm;
    public PlayerShieldSlime pSS;
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pm.beenHit = true;
            //collision.GetComponent<PlayerMove>().isDead = true;
            gm.LoseLife();
            ;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //pm.beenHit = true;
            //collision.GetComponent<PlayerMove>().isDead = true;
            gm.LoseLife();
            ;
        }
    }
}
