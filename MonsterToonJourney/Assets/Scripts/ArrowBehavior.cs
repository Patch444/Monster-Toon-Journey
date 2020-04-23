using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Need to access other scripts.
using UnityEngine.UI;

public class ArrowBehavior : MonoBehaviour
{
    // Sets up the game manager.
    public GameManager gm;
    // Need to access PlayerMove script.
    public PlayerMove pm;

    // Sets up the arrows' speed.
    public float arrowSpeed = 5f;
    // Sets up the timer.
    public float timer = 0f;

    // Sets up the direction the arrow will move.
    int moveDirection = 0;

    // Sets up a bool to determine if the arrow is a left-going arrow.
    public bool isLeftArrow;
    // Sets up a bool to determine if the arrow is a right-going arrow.
    public bool isRightArrow;
    // Sets up a bool to determine if the arrow has collided with a wall.
    public bool hitWall;
    // Sets up a bool to determine if the arrow has collided with the player before becoming lodged in a wall.
    public bool hitPlayer;
    // Sets up a bool to determine if the arrow has collided with an arrow stone.
    public bool hitArrowStone;

    public bool playedSound;


    // Sets up the arrow's rigidbody.
    public Rigidbody2D arrowRB;

    // Sets up the arrow's animator.
    public Animator anim;

    // Sets up the normal arrow sprite.
    public Sprite arrowNormal;
    // Sets up the broken arrow sprite.
    public Sprite arrowBroken;

    // Sets up the arrows.
    public GameObject rightArrow;
    public GameObject leftArrow;
    // Sets up the player's Game Object
    public GameObject player;

    public AudioClip arrowHitSound;
    public AudioClip arrowFallSound;

    public AudioSource Audio;

    // Sets up the arrow's starting position as a Transformation.
    // public Transform startPosition;
    // Sets up the arrow's starting position as a Vector3.
    // public Vector3 launchPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Sets GameManager.
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        // Notifies the game that the arrow hasn't hit a wall yet.
        hitWall = false;
        // Gets the arrow's starting position.
        // startPosition = this.transform;
        // Save's the arrow's starting position as a vector 3.
        // launchPosition = startPosition.position;

        anim = this.GetComponent<Animator>();

        // Finds the player and accesses the player move script.
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMove>();

        // Resets the timer.
        timer = 0f;

        // Sets arrow as a trigger.
        GetComponent<Collider2D>().isTrigger = true;

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        Audio = GetComponent<AudioSource>();
        //Audio.volume = PlayerPrefs.GetFloat("FxVolume");

    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            anim.enabled = true;

            if (isRightArrow == true)
            {
                // Moves right-going arrows.
                MoveArrowRight();
            }
            
            if (isLeftArrow == true)
            {
                // Moves left-going arrows.
                MoveArrowLeft();
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

    // Spawns an arrow.
    /*
    public void SpawnArrow()
    {
        // Checks if the launcher will fire right.
        if (isRightArrow == true)
        {
            // Spawns a right-going arrow.
            Instantiate(rightArrow, launchPosition, Quaternion.identity);
        }
        // Spawns a left-going arrow.
        else
        {
            Instantiate(leftArrow, launchPosition, Quaternion.identity);
        }
    }
    */




    // Moves right-going arrows.
    public void MoveArrowRight()
    {
        // Checks if the arrow is a right-going arrow 
        if (isRightArrow == true && isLeftArrow == false && hitWall == false && hitArrowStone == false)
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
                // Unfreezes the arrow.
                PlayFallSound();
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



    // Moves left-going arrows.
    public void MoveArrowLeft()
    {
        // Checks if the arrow is a left-going arrow 
        if (isLeftArrow == true && isRightArrow == false && hitWall == false && hitArrowStone == false)
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


    public void CleanUp()
    {
        // Destroys the original arrow.
        Destroy(gameObject);
    }


    //
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log(collider.tag);
        // Checks if the arrow collided with a wall.
        if (collider.tag == "Ground" || collider.tag == "MimicWall")
        {
            hitWall = true;
            Audio.clip = arrowHitSound;
            Audio.Play();
            GetComponent<Collider2D>().isTrigger = false;
            transform.gameObject.tag = "Ground";
        }
        // Checks if the arrow collided with the player before becoming lodged in a wall.
        if (collider.tag == "Player" && hitWall == false && pm.beenHit == false && hitArrowStone == false)
        {
            //this.GetComponent<SpriteRenderer>().enabled = false;
            //this.GetComponent<BoxCollider2D>().enabled = false;
            // Decrements player's lives.
            pm.beenHit = true;
            gm.TakeHit();

            // Disables the arrow.
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            // Spawns a new arrow.
            // Invoke("SpawnArrow", 2);
            // Cleans up the old arrow.
            Invoke("CleanUp", 3);
            // timer += 5000f;

            // Invoke("Destroy(gameObject)", 8.35f - timer);
            //SpawnArrow();
            // Destroys the arrow.
            //Destroy(obj: gameObject);
            // Player loses a life.

            // Spawns another arrow.
        }
        // Checks to see if the the arrow collided with an arrow stone.
        if (collider.tag == "ArrowStone" || collider.tag == "MovingPlatform")
        {
            hitArrowStone = true;
            Audio.clip = arrowHitSound;
            Audio.Play();
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
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isLeftArrow == true && isRightArrow == false && collision.tag == "RightGrabBox" && pm.arrowCount < 2 && hitWall == false && Input.GetKeyDown(KeyCode.E))
        {
            // Disables the arrow.
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            // Adds arrow to player's inventory.
            pm.ArrowGet();
            // Spawns a new arrow.
            Invoke("SpawnArrow", 2);
            // Cleans up the old arrow.
            Invoke("CleanUp", 3);
        }

        if (isRightArrow == true && isLeftArrow == false && collision.tag == "LeftGrabBox" && pm.arrowCount < 2 && hitWall == false && Input.GetKeyDown(KeyCode.E))
        {
            // Disables the arrow.
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            // Adds arrow to player's inventory.
            pm.ArrowGet();
            // Spawns a new arrow.
            Invoke("SpawnArrow", 2);
            // Cleans up the old arrow.
            Invoke("CleanUp", 3);
        }
    }
    */
}
