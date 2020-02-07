using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowBehavior : MonoBehaviour
{
    // Sets up the game manager.
    public GameManager gm;

    // Sets up the arrows' speed.
    public float arrowSpeed = 5f;
    // Sets up the timer.
    public float timer = 0f;

    // Sets up the direction the arrow will move.
    int moveDirection = 0;

    public GameObject rightPlayerArrow;
    public GameObject leftPlayerArrow;

    // Need to access PlayerMove script.
    private PlayerMove pm;

    // Sets up the arrow's rigidbody.
    public Rigidbody2D arrowRB;

    // Sets up the arrow's animator.
    public Animator anim;

    // Sets up a bool to determine if the arrow is a left-going player arrow.
    public bool isLeftPlayerArrow;
    // Sets up a bool to determine if the arrow is a right-going player arrow.
    public bool isRightPlayerArrow;
    // Sets up a bool to determine if the arrow has collided with a wall.
    public bool hitWall;
    // Sets up a bool to determine if the arrow has collided with an arrow stone.
    public bool hitArrowStone;

    public bool playedSound;

    // Sets up the arrow's sprite renderer.
    public SpriteRenderer arrowSprite;

    // Sets up the normal arrow sprite.
    public Sprite arrowNormal;
    // Sets up the broken arrow sprite.
    public Sprite arrowBroken;

    // Sets up the player's Game Object
    public GameObject player;

    public AudioClip arrowHitSound;
    public AudioClip arrowFallSound;

    public AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        // Sets GameManager.
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // Notifies the game that the arrow hasn't hit a wall yet.
        hitWall = false;

        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMove>();

        // Resets the timer.
        timer = 0f;

        // Sets arrow as a trigger.
        GetComponent<Collider2D>().isTrigger = true;

        anim = this.GetComponent<Animator>();

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            MovePlayerArrowRight();
            
        }
        if (!gm.isPaused)
        {
            anim.enabled = true;

            if (isRightPlayerArrow == true)
            {
                // Moves right-going arrows.
                MovePlayerArrowRight();
            }

            if (isLeftPlayerArrow == true)
            {
                // Moves left-going arrows.
                MovePlayerArrowLeft();
            }
        }
        else
        {
            anim.enabled = false;
        }


    }

    // Starts the timer.
    public void StartTimer()
    {
        // Updates the timer.
        timer = timer + Time.deltaTime;
    }

    // Spawns an arrow the player has collected.
    public void SpawnPlayerArrow()
    {
        // Checks if the player is facing right.
        if (pm.lastDirection == 1)
        {
            // Spawns a right-going player arrow.
            Instantiate(rightPlayerArrow, player.transform.position, Quaternion.identity);
        }
        // Checks if the player is facing left.
        if (pm.lastDirection == -1)
        {
            // Spawns a left-going player arrow.
            Instantiate(leftPlayerArrow, player.transform.position, Quaternion.identity);
        }
    }

    // Moves right-going player arrows.
    public void MovePlayerArrowRight()
    {
        // Checks if the arrow is a right-going arrow 
        if (isRightPlayerArrow == true && isLeftPlayerArrow == false && hitWall == false && hitArrowStone == false)
        {
            // Sets the initial arrow sprite.
            anim.Play("Right Arrow_Normal");
            // Moves the arrow to the right
            transform.position += Vector3.right * Time.deltaTime * arrowSpeed;
            moveDirection = 1;
        }
        if (hitWall == true)
        {
            // Starts the timer.
            StartTimer();

            // Prevents the arrow from falling.
            arrowRB.constraints = RigidbodyConstraints2D.FreezeAll;
            // Changes the arrow's sprite.
            if (timer > 0f && timer < 6.0f)
            {
                anim.Play("Right Arrow_Broken");
            }

            // Checks if 6.0 seconds have passed.
            if (timer >= 6.0f && timer < 8.35f)
            {
                anim.Play("Arrow Wiggle_Right");
            }
            // Checks if 8.0 seconds have passed.
            if (timer >= 8.0f)
            {
                PlayFallSound();
                // Unfreezes the arrow.
                arrowRB.constraints = RigidbodyConstraints2D.None;
                anim.Play("Right Arrow_Broken");
                // Makes the arrow fall
                transform.position += Vector3.down * Time.deltaTime * 3;
                moveDirection = -1;

            }
            // Checks if 8.35 seconds have passed.
            if (timer >= 8.35f)
            {
                // Spawns another arrow.
                // SpawnArrow();
                // Destroys the arrow.
                Destroy(gameObject);
            }
        }
        if (hitArrowStone == true)
        {
            StartTimer();
            anim.Play("Right Arrow_Broken");
            if (timer >= 0f)
            {
                transform.position += Vector3.down * Time.deltaTime * 3;
                moveDirection = -1;
            }
            if (timer >= 0.35f)
            {
                Destroy(gameObject);
            }
        }
    }

    // Moves left-going player arrows.
    public void MovePlayerArrowLeft()
    {
        // Checks if the arrow is a left-going arrow 
        if (isLeftPlayerArrow == true && isRightPlayerArrow == false && hitWall == false && hitArrowStone == false)
        {
            // Sets the initial arrow sprite.
            anim.Play("Left Arrow_Normal");
            // Moves the arrow to the left.
            transform.position += Vector3.left * Time.deltaTime * arrowSpeed;
            moveDirection = -1;
        }
        if (hitWall == true)
        {
            // Starts the timer.
            StartTimer();
            // Prevents the arrow from falling.
            arrowRB.constraints = RigidbodyConstraints2D.FreezeAll;
            // Changes the arrow's sprite.
            if (timer > 0f && timer < 6.0f)
            {
                anim.Play("Left Arrow_Broken");
            }
            // Checks if 6.0 seconds have passed.
            if (timer >= 6.0f && timer < 8.35f)
            {
                anim.Play("Arrow Wiggle_Left");
            }
            // Checks if 8.0 seconds have passed.
            if (timer >= 8.0f)
            {
                PlayFallSound();
                anim.Play("Left Arrow_Broken");
                // Unfreezes the arrow.
                arrowRB.constraints = RigidbodyConstraints2D.None;
                // Makes the arrow fall
                transform.position += Vector3.down * Time.deltaTime * 3;
                moveDirection = -1;
                // Play broken arrow animation?
            }
            // Checks if 8.35 seconds have passed.
            if (timer >= 8.35f)
            {
                // Spawns another arrow.
                // SpawnArrow();
                // Destroys the arrow.
                Destroy(gameObject);

            }
            if (hitArrowStone == true)
            {
                StartTimer();
                anim.Play("Left Arrow_Broken");
                if (timer >= 0f)
                {
                    PlayFallSound();
                    transform.position += Vector3.down * Time.deltaTime * 3;
                    moveDirection = -1;
                }
                if (timer >= 0.35f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void PlayFallSound()
    {
        if (!playedSound)
        {
            Audio.clip = arrowFallSound;
            Audio.Play();
            playedSound = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Checks if the arrow collided with a wall.
        if (collider.tag == "Ground")
        {
            hitWall = true;
            GetComponent<Collider2D>().isTrigger = false;
            Audio.clip = arrowHitSound;
            Audio.Play();
            transform.gameObject.tag = "Ground";
        }
        // Checks to see if the the arrow collided with an arrow stone.
        if (collider.tag == "ArrowStone")
        {
            hitArrowStone = true;
        }


    }

}
