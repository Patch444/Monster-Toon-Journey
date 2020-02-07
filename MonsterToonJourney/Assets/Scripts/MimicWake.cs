using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicWake : MonoBehaviour
{
    private bool isAsleep = true;
    public GameManager gm;
    public BoxCollider2D headCollider;
    public BoxCollider2D obstacleCollider;
    public BoxCollider2D runCollider;
    public Rigidbody2D bounceBody;
    public float speed = 1f;
    private Animator anim;

    public AudioClip mimicWake;
    public AudioClip mimicRun;
    public AudioClip mimicBonk;
    public AudioClip mimicSleep;

    public PlayerMove pm;

    private AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent <Animator> ();
        audio = GetComponent<AudioSource>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            anim.enabled = true;
            if (!isAsleep)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            anim.enabled = false;
        }
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //when player jumps on the head start wakeup
        if (other.tag == "Player" && isAsleep)
        {
            WakeUp();
        }
        else if (other.tag == "Player" && !isAsleep)
        {
            pm.beenHit = true;
            gm.TakeHit();
        }
        else if (other.tag == "MimicWall" && !isAsleep)
        {
            other.GetComponent<BreakableWall>().WallBreak();
        }
        else if (other.tag == "MimicStop" && !isAsleep)
        {
            StartCoroutine(DespawnDelay());
        }
    }
    public void WakeUp()
    {
        //start wakeup delay
        StartCoroutine(WakeDelay());
        audio.Stop();
        audio.loop = false;
        audio.clip = mimicWake;
        audio.Play();
        //play wakeup animation
        
        /*isAsleep = false;
        headCollider.enabled = false;
        obstacleCollider.enabled = false;
        runCollider.enabled = true;
        */

    }
    public IEnumerator WakeDelay()
    {
        anim.Play("Mimic Wake");
        headCollider.enabled = false;
        yield return new WaitForSecondsRealtime(.65f);
        audio.Stop();
        audio.loop = true;
        audio.clip = mimicRun;
        audio.Play();
        isAsleep = false;
        obstacleCollider.enabled = false;
        runCollider.enabled = true;
        anim.Play("Mimic Run");
    }
    public IEnumerator DespawnDelay()
    {
        audio.Stop();
        audio.loop = false;
        audio.clip = mimicBonk;
        audio.Play();
        anim.Play("Mimic Fall");
        runCollider.enabled = false;
        isAsleep = true;
        yield return new WaitForSecondsRealtime(0.5f);
        anim.Play("Mimic Unconscious");
        yield return new WaitForSecondsRealtime(0.5f);
        /*audio.Stop();
        audio.loop = true;
        audio.clip = mimicSleep;
        audio.Play();*/
        // Destroy(this.gameObject);
    }
}
