using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSlime : MonoBehaviour
{
    public Animator anim;
    public Animator playerAnim;
    public float bounceHeight;
    public float slimeSpeed;
    public int moveDirection;
    public int lastMoveDirection;
    public GameManager gm;

    public bool isIdleSlime;
    public bool playIdleJump;

    public AudioClip moveLoop;
    public AudioClip bounce;

    private AudioSource Audio;
    

    public PlayerMove pm;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            anim.enabled = true;
            if (isIdleSlime == false)
            {
                Move();
            }
            if (isIdleSlime == true)
            {
                Idle();
            }
           
        }
        else
        {
            anim.enabled = false;
        }
        
    }

    public void Move()
    {
        if (moveDirection == 1)
        {
            lastMoveDirection = 1;
            anim.Play("Slime_Bounce_Move_Right");
            transform.position += Vector3.right * Time.deltaTime * slimeSpeed;
        }
        if (moveDirection == -1)
        {
            lastMoveDirection = -1;
            anim.Play("Slime_Bounce_Move_Left");
            transform.position += Vector3.left * Time.deltaTime * slimeSpeed;
        }

    }

    public void Idle()
    {
        if (playIdleJump == false)
        {
            anim.Play("Slime_Bounce_Idle");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !gm.isPaused)
        {
            Audio.Pause();
            Audio.clip = bounce;
            Audio.loop = false;
            Audio.Play();
            anim.Play("Slime_Bounce_Jumped");
            other.attachedRigidbody.AddForce(Vector2.up * bounceHeight, ForceMode2D.Impulse);
            if (isIdleSlime == false)
            {
                moveDirection = 0;
            }
            if (isIdleSlime == true)
            {
                playIdleJump = true;
            }
            StartCoroutine(JumpedDelay());
        }
    }

    public void OnCollision(Collision2D collision)
    {
        if(collision.otherCollider.tag == "Player" && !gm.isPaused)
        {
            moveDirection = moveDirection * -1;
        }
    }

    public IEnumerator JumpedDelay()
    {
        
        yield return new WaitForSecondsRealtime(.64f);
        Audio.Pause();
        Audio.clip = moveLoop;
        Audio.loop = true;
        Audio.Play();
        if (isIdleSlime == false)
        {
            moveDirection = lastMoveDirection;
        }
        if (isIdleSlime == true)
        {
            playIdleJump = false;
        }
    }

}
