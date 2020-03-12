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
            Move();
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Audio.Pause();
            Audio.clip = bounce;
            Audio.loop = false;
            Audio.Play();
            moveDirection = 0;
            anim.Play("Slime_Bounce_Jumped");
            other.attachedRigidbody.AddForce(Vector2.up * bounceHeight, ForceMode2D.Impulse);
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
        moveDirection = lastMoveDirection;
    }

}
