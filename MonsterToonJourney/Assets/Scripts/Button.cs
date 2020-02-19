using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    GameManager gm;

    private PlayerMove pm;

    public List<GameObject> counterparts;
    public GameObject box;
    public List<SpikeTrap> sts;
    public List<ArrowSpawn> aSpawns;


    public int counterpartCount;

    public bool hasBox;
    public bool canPress;
    public bool isPressed;
    private bool boxConstrained;
    public bool canInteract;
    public bool isDeactivator;
    public bool isActivator;
    public bool isSpikes;
    public bool isArrowSpawn;
    public bool isPlatform;
    public bool isPortcullis;



    public AudioClip BoxPlace;

    private AudioSource Audio;

    private Image boxIcon;

    // Start is called before the first frame update
    void Start()
    {
        
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        boxIcon = GameObject.Find("Box Icon").GetComponent<Image>();
        Audio = GetComponent<AudioSource>();
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        if (isSpikes)
        {
            foreach (GameObject spike in counterparts)
                sts.Add(spike.GetComponent<SpikeTrap>());
        }
        if (isArrowSpawn)
        {
            foreach (GameObject spawn in counterparts)
                aSpawns.Add(spawn.GetComponent<ArrowSpawn>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            //Add the box's rotation and position constraints back
            EnableBox();


            //Player places the box
            if (Input.GetKeyDown(KeyCode.E) && canInteract && pm.hasBox && !hasBox)
            {
                pm.hasBox = false;
                hasBox = true;
                boxIcon.enabled = false;
                box.SetActive(true);
                Audio.clip = BoxPlace;
                Audio.Play();
            }
<<<<<<< Updated upstream
=======
            // Player picks up box
            
            else if (Input.GetKeyDown(KeyCode.E) && canInteract && !pm.hasBox && hasBox)
            {
                pm.hasBox = true;
                hasBox = false;
                boxIcon.enabled = true;
                box.SetActive(false);
                pm.Audio.clip = pm.boxGrab;
                pm.Audio.Play();
            }
            
>>>>>>> Stashed changes

            // Activates/deavtivates the button's counterpart.
            if (isPressed == true || hasBox == true)
            {
                if (isActivator)
                {
                    if (isSpikes)
                    {
                        foreach (SpikeTrap spike in sts)
                            spike.isActive = true;
                    }
                    if (isArrowSpawn)
                    {
                        foreach (ArrowSpawn spawn in aSpawns)
                            spawn.isActive = true;
                    }
                    if (isPortcullis)
                    {
                        foreach (GameObject portcullis in counterparts)
                            // Will eventually play rising animation
                            portcullis.SetActive(true);
                    }
                    if (isPlatform)
                    {
                        foreach (GameObject platform in counterparts)
                            platform.SetActive(true);
                    }
                }
                if (isDeactivator)
                {
                    if (isSpikes)
                    {
                        foreach (SpikeTrap spike in sts)
                            spike.isActive = false;
                    }
                    if (isArrowSpawn)
                    {
                        foreach (ArrowSpawn spawn in aSpawns)
                            spawn.isActive = false;
                    }
                    if (isPortcullis)
                    {
                        foreach (GameObject portcullis in counterparts)
                            // Will eventually play closing animation
                            portcullis.SetActive(false);
                    }
                    if (isPlatform)
                    {
                        foreach (GameObject platform in counterparts)
                            platform.SetActive(false);
                    }
                }
            }
            else
            {
                if (isActivator)
                {
                    if (isSpikes)
                    {
                        foreach (SpikeTrap spike in sts)
                            spike.isActive = false;
                    }
                    if (isArrowSpawn)
                    {
                        foreach (ArrowSpawn spawn in aSpawns)
                            spawn.isActive = false;
                    }
                    if (isPortcullis)
                    {
                        foreach (GameObject portcullis in counterparts)
                            // Will eventually play closing animation
                            portcullis.SetActive(false);
                    }
                    if (isPlatform)
                    {
                        foreach (GameObject platform in counterparts)
                            platform.SetActive(false);
                    }
                }
                if (isDeactivator)
                {
                    if (isSpikes)
                    {
                        foreach (SpikeTrap spike in sts)
                            spike.isActive = true;
                    }
                    if (isArrowSpawn)
                    {
                        foreach (ArrowSpawn spawn in aSpawns)
                            spawn.isActive = true;
                    }
                    if (isPortcullis)
                    {
                        foreach (GameObject portcullis in counterparts)
                            // Will eventually play rising animation
                            portcullis.SetActive(true);
                    }
                    if (isPlatform)
                    {
                        foreach (GameObject platform in counterparts)
                            platform.SetActive(true);
                    }
                }
            }
        }
    }
    public void EnableBox()
    {
        if (!boxConstrained)
        {
            box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            boxConstrained = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            canInteract = true;
            isPressed = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            canInteract = false;
            isPressed = false;
        }
    }
}

