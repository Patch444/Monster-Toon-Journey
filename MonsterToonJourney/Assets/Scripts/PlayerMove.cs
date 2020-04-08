using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    GameManager gm;
    [SerializeField]
    GameObject groundCheck;
    [SerializeField]
    float groundCheckSize;

    [SerializeField]
    float walkSpeed;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    float glideFactor;
    [SerializeField]
    float maximumDrag;

    //public float releaseDelay;

    // Sets up the animator.
    private Animator anim;

    // Sets up sprite renderer.
    private SpriteRenderer sprite;

    public Vector3 spawnPosition;
    public Vector3 rigidbody;
    //public Vector3 releasePosition;

    Rigidbody2D playerBody;
    public int moveDirection;
    int previousMoveDirection;
    int beforeJumpDirection;
    // public int arrowCount;
    public int lastDirection;

    public int qPress;

    public Sprite idleLeft;
    public Sprite idleRight;

    public Sprite shieldIdleLeft;
    public Sprite shieldIdleLeftYellow;
    public Sprite shieldIdleLeftRed;

    public Sprite shieldIdleRight;
    public Sprite shieldIdleRightYellow;
    public Sprite shieldIdleRightRed;

    public float shieldTimer;
    public float shieldDelay;
    public float maxVelocity;

    public bool isAirborne;
    public bool justLanded;
    public bool canJump;

    public bool hasBlanket;
    public bool canGlide;
    bool hasPlayedGlide;
    public bool hasGlided;
    public bool isGliding;

    public bool canWalk;
    bool isWalking;

    public bool immune;
    public bool hasShield;
    public bool isShielding;
    public bool hasShieldSlime;

    public bool hasBox;
    public bool hasKey;

    public bool isDead;
    public bool beenHit;
    public bool isSwinging;

    public bool isByDoor;
    
    

    public Image shieldIcon;

    public AudioClip boxGrab;
    public AudioClip ShieldUp;
    public AudioClip ShieldDown;
    public AudioClip ShieldGrab;
    public AudioClip keyGrab;
    public AudioClip keyUse;
    public AudioClip shieldSlimeUp;
    public AudioClip shieldSlimeDown;

    public AudioClip[] landingSounds = new AudioClip[4];
    public AudioClip[] glideSounds = new AudioClip[3];

    public AudioSource Audio;



    //public bool hasBackpackOfHolding;

    //public GameObject grabBoxLeft;
    //public GameObject grabBoxRight;
    //public GameObject leftPlayerArrow;
    //public GameObject rightPlayerArrow;



    // Start is called before the first frame update
    void Start()
    {
        hasBlanket = false;
        immune = false;
        hasGlided = false;
        //arrowCount = 0;
        // Assigns animator.
        anim = GetComponent<Animator>();

        // Sets up the release delay.
        //releaseDelay = 1;

        shieldDelay = 1;

        // Assigns the sprite renderer.
        sprite = GetComponent<SpriteRenderer>();
        lastDirection = 1;
        if (GameObject.Find("ShieldIcon") != null)
        {
            shieldIcon = GameObject.Find("ShieldIcon").GetComponent<Image>();
        }

        //Sets up the Audio Source
        Audio = GetComponent<AudioSource>();


        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        playerBody = gameObject.GetComponent<Rigidbody2D>();
        spawnPosition = this.transform.position;
        moveDirection = 0;
        previousMoveDirection = 0;
        isByDoor = false;
        hasKey = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!gm.isPaused)
        {
            // ReleaseArrow();
            Shielding();
            IdleDirection();
            anim.enabled = true;
            AudioListener.pause = false;

            if(playerBody.velocity.magnitude > maxVelocity)
            {
                playerBody.velocity = playerBody.velocity.normalized * maxVelocity;
            }

            if (GetComponent<Rigidbody2D>().constraints == RigidbodyConstraints2D.FreezeAll)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                GetComponent<Rigidbody2D>().velocity = rigidbody;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundCheckSize, LayerMask.GetMask("Ground"));
            if (colliders != null)
            {
                int count = 0;
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].tag != "Ground" && colliders[i].tag != "MovingPlatform")
                    {
                        count++;

                    }
                }
                if (count == colliders.Length)
                {
                    canJump = false;
                    isWalking = false;
                    isAirborne = true;
                    if (isShielding == false)
                        canGlide = true;
                }
            }
            if (canWalk)
            {
                Walk();
            }
            if (canJump)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    Jump();
            }
            if (canGlide && hasBlanket == true)
            {
                Glide();
            }
            if (!isAirborne && !isWalking && canJump)
            {
                IdleDirection();

            }
            if (isDead)
            {
                canWalk = false;
                canJump = false;
                canGlide = false;

                gm.LoseLife();


            }
            if (!isAirborne && hasShield)
            {

            }


        }
        else
        {
            rigidbody = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            anim.enabled = false;
            AudioListener.pause = true;
        }
        // GrabDirection();


    }

    // Increases the player's arrow count.
    /*public void ArrowGet()
    {
        if(arrowCount < 2)
        {
            arrowCount++;
        }
    }
    */
    /*
    // Starts the delay countdown.
    /public void DelayCountdown()
    {
        releaseDelay = releaseDelay - Time.deltaTime;
    }
    */

    /*public void ReleaseArrow()
    {
        // Gets the position of the release point.
        releasePosition = GameObject.Find("ReleasePosition").transform.position;
        // Spawns left-going player arrow.
        if (lastDirection == -1 && arrowCount >= 1 && releaseDelay == 1 && Input.GetKeyDown(KeyCode.Q))
        {
            DelayCountdown();
            Instantiate(leftPlayerArrow, releasePosition, Quaternion.identity);
            arrowCount--;
        }
        // Spawns right-going player arrow.
        if (lastDirection == 1 && arrowCount >= 1 && releaseDelay == 1 && Input.GetKeyDown(KeyCode.Q))
        {
            DelayCountdown();
            Instantiate(rightPlayerArrow, releasePosition, Quaternion.identity);
            arrowCount--;
        }
        if(releaseDelay < 1 && releaseDelay > 0)
        {
            DelayCountdown();
        }
        if (releaseDelay <= 0)
        {
            releaseDelay = 1;
        }
        
        
    }
    */

    public void StartShieldTimer()
    {
        shieldTimer = shieldTimer + Time.deltaTime;
    }

    public void ShieldCooldown()
    {
        shieldDelay = shieldDelay - Time.deltaTime;
    }

    public void Shielding()
    {
        if(hasShield == true && qPress < 2 && shieldDelay == 1 && Input.GetKeyDown(KeyCode.Q) && isAirborne == false)
        {
            qPress++;
        }
        if (hasShield == true && isAirborne == false && qPress == 1 && isShielding == false && shieldDelay == 1)
        {
            StartShieldTimer();
            Audio.clip = ShieldUp;
            Audio.Play();
            walkSpeed = walkSpeed / 2;
            shieldIcon.GetComponent<Image>().color = new Color32(200, 0, 0, 255);
        }
        if (shieldTimer > 0f && shieldTimer < 3)
        {
            StartShieldTimer();
            isShielding = true;
            canJump = false;
            canGlide = false;
        }
        if (shieldTimer > 3 || qPress == 2)
        {
            qPress = 0;
            isShielding = false;
            Audio.clip = ShieldDown;
            Audio.Play();
            walkSpeed = walkSpeed * 2;
            canJump = true;
            shieldTimer = 0;
            ShieldCooldown();
        }
        if (shieldDelay < 1 && shieldDelay > 0)
        {
            ShieldCooldown();
        }
        if (shieldDelay < 0)
        {
            shieldIcon.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            shieldDelay = 1;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Check to see if the collision enter is the ground
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovingPlatform")
        {
            //RaycastHit2D hitCenter = Physics2D.Raycast(transform.position, -this.transform.up, 1f, LayerMask.GetMask("Ground"));
            //RaycastHit2D hitLeft = Physics2D.Raycast(transform.position +Vector3.left*0.34f, transform.right *0.34f-this.transform.up, 1f, LayerMask.GetMask("Ground"));
            //RaycastHit2D hitRight = Physics2D.Raycast(transform.position+Vector3.right*0.34f, -transform.right*0.34f-this.transform.up, 1f, LayerMask.GetMask("Ground"));

            //Debug.DrawLine(transform.position, Vector2.down * 1f + (Vector2)transform.position, Color.cyan);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundCheckSize, LayerMask.GetMask("Ground"));
            Debug.Log(colliders.Length);
            if (colliders != null)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    Debug.Log(colliders[i].tag);
                    if (colliders[i].tag == "Ground" || colliders[i].tag == "MovingPlatform")
                    {
                        Debug.Log("Staying in collision");

                        isAirborne = false;
                        canJump = true;
                        canWalk = true;
                        canGlide = false;
                        hasPlayedGlide = false;

                        //Reset the drag after any possible glide
                        playerBody.drag = 0;

                    }
                }
            }
            //if(hitCenter.collider != null || hitLeft.collider != null || hitRight.collider != null)
            {

            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovingPlatform")
        {
            bool isLanding = false;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundCheckSize, LayerMask.GetMask("Ground"));
            Debug.Log("No colliders: " + colliders == null);
            if (colliders != null)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (collider.tag == "Ground" || collider.tag == "MovingPlatform")
                    {
                        Debug.Log("Hit Ground");
                        isLanding = true;
                        hasGlided = false;
                        break;
                    }
                }
            }
            Debug.Log("Goblin is landing: " + isLanding);
            if (isAirborne && isLanding)
            {
                if (lastDirection == 1 && hasPlayedGlide == false)
                {
                    canWalk = false;
                    justLanded = true;
                    anim.Play("Monster Land_Right");
                    //Debug.Log("land right");
                    //canWalk = true;
                }
                if (lastDirection == -1 && hasPlayedGlide == false)
                {
                    canWalk = false;
                    justLanded = true;
                    anim.Play("Monster Land_Left");
                    //Debug.Log("land left");
                    //canWalk = true;
                }
                else if (lastDirection == 1 && hasPlayedGlide == true && hasBlanket == true)
                {
                    canWalk = false;
                    justLanded = true;
                    anim.Play("Parachute Close_Right");
                }
                else if (lastDirection == -1 && hasPlayedGlide == true && hasBlanket == true)
                {
                    canWalk = false;
                    justLanded = true;
                    anim.Play("Parachute Close_Left");
                }
                PlayLandingSound();
                Invoke("FinishLanding", 0.25f);
                hasPlayedGlide = false;
                isAirborne = false;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(groundCheck.transform.position, Vector3.forward, groundCheckSize);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Check to see if the collision exit is the ground
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovingPlatform")
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundCheckSize, LayerMask.GetMask("Ground"));
            if (colliders != null)
            {
                int count = 0;
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].tag != "Ground" || colliders[i].isTrigger || colliders[i].tag == "MovingPlatform")
                    {
                        count++;

                    }
                }
                if (count == colliders.Length)
                {
                    canJump = false;
                    isWalking = false;
                    isAirborne = true;
                    canGlide = true;
                    if (lastDirection == 1 && !Input.GetKey(KeyCode.LeftShift) && isShielding == false)
                    {
                        anim.Play("Monster Jump_Right");
                    }

                    if (lastDirection == -1 && !Input.GetKey(KeyCode.LeftShift) && isShielding == false)
                    {
                        anim.Play("Monster Jump_Left");
                    }

                    if (lastDirection == 1 && Input.GetKeyDown(KeyCode.LeftShift) && isShielding == false && hasBlanket == true)
                    {
                        anim.Play("Parachute Open_Right");

                    }

                    if (lastDirection == -1 && Input.GetKeyDown(KeyCode.LeftShift) && isShielding == false && hasBlanket == true)
                    {
                        anim.Play("Parachute Open_Left");
                    }

                }
            }

        }

    }

    // Changes the idle sprite.
    public void IdleDirection()
    {
        if (isWalking == false && moveDirection == 0 && lastDirection == -1 && isAirborne == false && justLanded == false && isShielding == false)
        {
            anim.Play("Monster Idle_Left");
        }
        if (isWalking == false && moveDirection == 0 && lastDirection == 1 && isAirborne == false && justLanded == false && isShielding == false)
        {
            anim.Play("Monster Idle_Right");
            //Debug.Log("Idling");
        }

        if (isWalking == false && moveDirection == 0 && lastDirection == 1 && isAirborne == false && justLanded == false && isShielding == true && shieldTimer < 1)
        {
            anim.Play("Shield_Idle_Right");
        }
        if (isWalking == false && moveDirection == 0 && lastDirection == 1 && isAirborne == false && justLanded == false && isShielding == true && shieldTimer > 1 && shieldTimer < 2)
        {
            anim.Play("Shield_Idle_Yellow_Right");
        }
        if (isWalking == false && moveDirection == 0 && lastDirection == 1 && isAirborne == false && justLanded == false && isShielding == true && shieldTimer > 2)
        {
            anim.Play("Shield_Idle_Red_Right");
        }

        if (isWalking == false && moveDirection == 0 && lastDirection == -1 && isAirborne == false && justLanded == false && isShielding == true && shieldTimer < 1)
        {
            anim.Play("Shield_Idle_Left");
        }
        if (isWalking == false && moveDirection == 0 && lastDirection == -1 && isAirborne == false && justLanded == false && isShielding == true && shieldTimer > 1 && shieldTimer < 2)
        {
            anim.Play("Shield_Idle_Yellow_Left");
        }
        if (isWalking == false && moveDirection == 0 && lastDirection == -1 && isAirborne == false && justLanded == false && isShielding == true && shieldTimer > 2)
        {
            anim.Play("Shield_Idle_Red_Left");
        }

    }

    // Activates/deactivates the player's grab boxes depending on where they are facing.
    /* public void GrabDirection()
    {
        if(lastDirection == -1)
        {
            // Activates the left grab box.
            grabBoxLeft.SetActive(true);
            // Deativates the right grab box.
            grabBoxRight.SetActive(false);
        }
        if (lastDirection == 1)
        {
            // Activates the left grab box.
            grabBoxRight.SetActive(true);
            // Deativates the left grab box.
            grabBoxLeft.SetActive(false);
        }
    } */

    private void Walk()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && isByDoor == false)
        {
            //Move to the right
            RaycastHit2D hit = Physics2D.Raycast(transform.position, this.transform.right, 0.7f, LayerMask.GetMask("Ground"));
            Debug.DrawLine(transform.position, Vector2.right * 0.7f + (Vector2)transform.position, Color.cyan);
            lastDirection = 1;

            if (hit.collider == null)
            {
                transform.position += Vector3.right * Time.deltaTime * walkSpeed;
                moveDirection = 1;
                isWalking = true;
                //Set animation to walk right
                if (moveDirection == 1 && isWalking == true && isAirborne == false && justLanded == false && isShielding == false && shieldTimer == 0)
                {
                    anim.Play("Monster Walk_Right");
                }
                if (moveDirection == 1 && isWalking == true && isAirborne == true && justLanded == false && isShielding == false && shieldTimer == 0 && (!Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.LeftShift) && hasBlanket == false)) && hasGlided == false)
                {
                    anim.Play("Monster Jump_Right");
                }
                if (moveDirection == 1 && isWalking == true && isAirborne == true && justLanded == false && isShielding == false && shieldTimer == 0 && ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift)) && hasBlanket == true))
                {
                    anim.Play("Parachute Open_Right");
                }

                if (moveDirection == 1 && isWalking == true && isAirborne == true && justLanded == false && isShielding == false && shieldTimer == 0 && (Input.GetKeyUp(KeyCode.LeftShift) && hasBlanket == true) && hasGlided == true)
                {
                    anim.Play("Parachute Close And Jump_Right");
                }

                if (moveDirection == 1 && isWalking == true && isAirborne == true && justLanded == false && isShielding == false && shieldTimer == 0 && Input.GetKey(KeyCode.LeftShift))
                {
                    Glide();
                }
                if (moveDirection == 1 && isWalking == true && isAirborne == false && justLanded == false && isShielding == true && shieldTimer > 0 && shieldTimer < 1)
                {
                    anim.Play("Shield_Right");
                }
                if (moveDirection == 1 && isWalking == true && isAirborne == false && justLanded == false && isShielding == true  && shieldTimer > 1 && shieldTimer < 2)
                {
                    anim.Play("Shield_Yellow_Right");
                }
                if (moveDirection == 1 && isWalking == true && isAirborne == false && justLanded == false && isShielding == true && shieldTimer > 2)
                {
                    anim.Play("Shield_Red_Right");
                }



            }
            // Changes the idle spirte.



        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && isByDoor == false)
        {
            //Move to the left
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -this.transform.right, 0.7f, LayerMask.GetMask("Ground"));
            Debug.DrawLine(transform.position, Vector2.left * 0.7f + (Vector2)transform.position, Color.cyan);
            lastDirection = -1;
            if (hit.collider == null)
            {
                transform.position += Vector3.left * Time.deltaTime * walkSpeed;
                moveDirection = -1;
                isWalking = true;
                //Set animation to walk left.
                if (moveDirection == -1 && isWalking == true && isAirborne == false && justLanded == false && isShielding == false && shieldTimer == 0)
                {
                    anim.Play("Monster Walk_Left");
                }
                if (moveDirection == -1 && isWalking == true && isAirborne == true && justLanded == false && isShielding == false && shieldTimer == 0 && (!Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.LeftShift) && hasBlanket == false)) && hasGlided == false)
                {
                    anim.Play("Monster Jump_Left");
                }
                if (moveDirection == -1 && isWalking == true && isAirborne == true && justLanded == false && isShielding == false && shieldTimer == 0 && ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift)) && hasBlanket == true))
                {
                    anim.Play("Parachute Open_Left");
                }
                if (moveDirection == -1 && isWalking == true && isAirborne == true && justLanded == false && isShielding == false && shieldTimer == 0 && (Input.GetKeyUp(KeyCode.LeftShift) && hasBlanket == true) && hasGlided == true)
                {
                    anim.Play("Parachute Close And Jump_Left");
                }

                if (moveDirection == -1 && isWalking == true && isAirborne == false && justLanded == false && isShielding == true && shieldTimer > 0 && shieldTimer < 1 )
                {
                    anim.Play("Shield_Left");
                }
                if (moveDirection == -1 && isWalking == true && isAirborne == false && justLanded == false && isShielding == true && shieldTimer > 1 && shieldTimer < 2)
                {
                    anim.Play("Shield_Yellow_Left");
                }
                if (moveDirection == -1 && isWalking == true && isAirborne == false && justLanded == false && isShielding == true && shieldTimer > 2)
                {
                    anim.Play("Shield_Red_Left");
                }

            }
            //Will set animator with moveDirection

            //Set animation to walk left
        }
        else
        {
            moveDirection = 0;
            isWalking = false;
        }
        previousMoveDirection = moveDirection;
    }

    private void Jump()
    {
        isAirborne = true;
        beforeJumpDirection = moveDirection;
        playerBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        if (lastDirection == 1 && (!Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift) && hasBlanket == false))
        {
            //Debug.Log("Jump Right");
            anim.Play("Monster Jump_Right");
        }
        if (lastDirection == -1 && (!Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift) && hasBlanket == false))
        {
            //Debug.Log("Jump Left");
            anim.Play("Monster Jump_Left");
        }
        if (lastDirection == 1 && ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift)) && hasBlanket == true))
        {
            //Debug.Log("Jump Left");
            anim.Play("Parachute Open_Right");
            hasGlided = true;
        }
        if (lastDirection == -1 && ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift)) && hasBlanket == true))
        {
            //Debug.Log("Jump Left");
            anim.Play("Parachute Open_Left");
            hasGlided = true;
        }
    }

    private void Glide()
    {
        if (!isSwinging && isShielding == false && hasBlanket == true)
        {
            //Hey Patrick is it working?
            float previousY = this.transform.position.y;
            if (Input.GetKeyDown(KeyCode.LeftShift) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space)))
            {
                if (lastDirection == 1)
                    anim.Play("Parachute Open_Right");
                else if (lastDirection == -1)
                    anim.Play("Parachute Open_Left");
                hasPlayedGlide = true;
                hasGlided = true;
                PlayGlideSound();
                isGliding = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (lastDirection == 1)
                    anim.Play("Parachute Close And Jump_Right");
                else if (lastDirection == -1)
                    anim.Play("Parachute Close And Jump_Left");
                hasPlayedGlide = false;
                isGliding = false;

            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!hasPlayedGlide)
                {
                    if (lastDirection == 1)
                        anim.Play("Parachute Open_Right");
                    else if (lastDirection == -1)
                        anim.Play("Parachute Open_Left");
                    hasPlayedGlide = true;
                    PlayGlideSound();
                }
                playerBody.drag += glideFactor;
                if (playerBody.drag > maximumDrag)
                {
                    playerBody.drag = maximumDrag;
                }
            }
            else
            {
                playerBody.drag = 0;
            }
        }
    }

    private void ApplyVerticalForce()
    {
        playerBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);

    }

    void FinishLanding()
    {
        justLanded = false;
    }

    public void PlayLandingSound()
    {
        int index = Random.Range(0, landingSounds.Length);
        Audio.clip = landingSounds[index];
        Audio.Play();
    }

    public void PlayGlideSound()
    {
        int index = Random.Range(0, glideSounds.Length);
        Audio.clip = glideSounds[index];
        Audio.Play();
    }
}
