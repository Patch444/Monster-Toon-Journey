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
    public GameObject regrabCollider;
    private GameObject fm;

    //public BoxCollider2D regrabCollider;

    public List<SpikeTrap> sts;
    public List<ArrowSpawn> aSpawns;
    public List<Portcullis> ports;

    public Animator anim;

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
    public AudioClip ButtonDown;
    public AudioClip ButtonUp;

    public AudioSource Audio;

    private Image boxIcon;

    // Start is called before the first frame update
    void Start()
    {
        
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        boxIcon = GameObject.Find("Box Icon").GetComponent<Image>();
        fm = GameObject.Find("Fear Meter");
        anim = this.GetComponent<Animator>();
        //regrabCollider.enabled = false;

        Audio = GetComponent<AudioSource>();

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
        if (isPortcullis)
        {
            foreach (GameObject port in counterparts)
                ports.Add(port.GetComponent<Portcullis>());
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            //Add the box's rotation and position constraints back
            EnableBox();
            anim.enabled = true;
            

            //Player places the box

            
// >>>>>>> Stashed changes

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
                        foreach (Portcullis port in ports)

                            port.Fall();
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
                        foreach (Portcullis port in ports)
                            // Will eventually play closing animation
                            port.Rise();
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
                        foreach (Portcullis port in ports)
                            port.Rise();
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
                        foreach (Portcullis port in ports)
                            port.Fall();
                    }
                    if (isPlatform)
                    {
                        foreach (GameObject platform in counterparts)
                            platform.SetActive(true);
                    }
                }
            }
        }
        else
        {
            anim.enabled = false;
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
        if (other.tag == "Player")
            if (!hasBox)
            {
                Audio.clip = ButtonDown;
                Audio.Play();
            }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            canInteract = true;
            isPressed = true;
            anim.Play("Button_Press");
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            canInteract = false;
            isPressed = false;
            if (!hasBox)
            {
                Audio.clip = ButtonUp;
                anim.Play("Button_Release");
                Audio.Play();
            }
        }
    }
}

