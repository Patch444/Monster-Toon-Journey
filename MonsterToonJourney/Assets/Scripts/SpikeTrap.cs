using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpikeTrap : MonoBehaviour
{
    GameManager gm;
    public bool canInteract;
    public float startTimer;
    public float startLength = 3.0f;
    public float upTimer;
    public float downTimer;
    public float retractTimer;
    public float retractLength = 1f;
    private float upSpeed = .12f;
    private float downSpeed = .12f;

    public bool hasBox;
    public bool movingUp;
    public bool movingDown;
    public bool hasGoneUp;
    public bool hasGoneDown = true;
    public bool timerRunning;
    private bool boxConstrained;
    private bool SoundPlayed;
    public bool isActive;

    public AudioClip SpikeUp;
    public AudioClip SpikeDown;
    public AudioClip BoxPlace;

    public AudioSource Audio;

    public GameObject box;
    public GameObject spikes;
    private GameObject fm;
    private PlayerMove pm;
    private BoxLaunch boxLaunch;
    public Vector3 spikeStart;
    public Vector3 spikeEnd;
    private Image boxIcon;

    public Scene currentScene;
    public string sceneName;




    // Start is called before the first frame update
    void Start()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName != "Intro")
        {
            pm = GameObject.Find("Player").GetComponent<PlayerMove>();
            boxIcon = GameObject.Find("Box Icon").GetComponent<Image>();
            fm = GameObject.Find("Fear Meter");
        }
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartTimer();
        boxLaunch = box.GetComponent<BoxLaunch>();
        spikeStart = spikes.transform.position;
        spikeEnd = spikes.transform.position;
        spikeEnd.y += 1.5f;
        Audio = GetComponent<AudioSource>();
        //Audio.volume = PlayerPrefs.GetFloat("FxVolume");
        //boxLaunch.Launch();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.hasUnpaused && !timerRunning)
        {

        }
        if (!gm.isPaused && isActive == true)
        {
            //Add the box's rotation and position constraints back
            Time.timeScale = 1;
            EnableBox();
            /*
            //Player places the box
            if (Input.GetKeyDown(KeyCode.E) && canInteract && pm.hasBox && !hasBox)
            {
                pm.hasBox = false;
                hasBox = true;
                boxIcon.enabled = false;
                boxIcon.transform.SetParent(GameObject.Find("Canvas").transform);
                box.SetActive(true);
                Audio.clip = BoxPlace;
                Audio.Play();
                //spikes.GetComponent<BoxCollider2D>().enabled = false;
            }
            */
            //starts activation timer
            if (timerRunning)
            {
                startTimer = startTimer + Time.deltaTime;
            }
            //Start spikes moving upward
            if (startTimer >= startLength && hasGoneDown)
            {
                ActivateSpikes();
            }
            //start timer to stop spikes
            if (movingUp)
            {
                //upTimer = upTimer + Time.deltaTime;
                spikes.transform.Translate(Vector2.up * upSpeed);
            }
            //stop spikes
            /*if (upTimer >= 0.2f && movingUp)
            {
                movingUp = false;
                upTimer = 0f;
                hasGoneUp = true;
            }*/
            if (spikes.transform.position.y >= spikeEnd.y && movingUp)
            {
                movingUp = false;
                //upTimer = 0f;
                hasGoneUp = true;
            }
            //start timer to move down
            if (hasGoneUp)
            {
                spikes.transform.position = spikeEnd;
                retractTimer = retractTimer + Time.deltaTime;
                SoundPlayed = false;
            }
            //start moving down
            if (retractTimer >= retractLength && hasGoneUp)
            {
                retractTimer = 0f;
                hasGoneUp = false;
                movingDown = true;
            }
            //time moving down
            if (movingDown)
            {
                PlayDownSound();
                downTimer = downTimer + Time.deltaTime;
                spikes.transform.Translate(Vector2.down * downSpeed);
            }
            //finish moving down, deactivate spikes
            if (downTimer >= 0.2f && movingDown)
            {
                DeactivateSpikes();


            }
        }
        else
        {
            Time.timeScale = 0;
            //box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            boxConstrained = false;
            StopAllCoroutines();

        }


    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            canInteract = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            canInteract = false;
        }
    }
    public void StartTimer()
    {
        timerRunning = true;
    }
    public void ActivateSpikes()
    {
        timerRunning = false;
        startTimer = 0f;
        if (!hasBox)
        {
            //move the spikes up
            movingUp = true;
            PlayUpSound();
            //move them down
            //StartCoroutine(DownDelay());
            //restart timer
        }
        else if (hasBox)
        {
            //move the spikes up
            movingUp = true;
            PlayUpSound();
            //StartCoroutine(DownDelay());
            //launch box
            boxLaunch.Launch();
            //start delay on move down

        }
    }
    public void DeactivateSpikes()
    {
        downTimer = 0f;
        movingDown = false;
        hasGoneDown = true;
        spikes.transform.position = spikeStart;
        timerRunning = true;
        SoundPlayed = false;
    }

    public void AddBox()
    {
        hasBox = true;
        //spikes.GetComponent<BoxCollider2D>.enabled = 
    }
    public void ReactivateTrap()
    {
        //Debug.Log("Spike Trap Reactivating");
        StartCoroutine(DownDelay());
        gm.hasUnpaused = false;
    }
    public IEnumerator DownDelay()
    {
        Debug.Log("Moving spikes");
        if (hasGoneDown)
        {
            hasGoneDown = false;
            movingUp = true;
            yield return new WaitForSecondsRealtime(.2f);
            movingUp = false;
            hasGoneUp = true;
            yield return new WaitForSecondsRealtime(1f);
            movingDown = true;
            yield return new WaitForSecondsRealtime(.2f);
            movingDown = false;
            hasGoneUp = false;
            hasGoneDown = true;
            spikes.transform.position = spikeStart;
        }
        else if (hasGoneUp)
        {
            yield return new WaitForSecondsRealtime(1f);
            movingDown = true;
            yield return new WaitForSecondsRealtime(.2f);
            movingDown = false;
            hasGoneUp = false;
            hasGoneDown = true;
            spikes.transform.position = spikeStart;
        }
        StartTimer();
    }
    public void EnableBox()
    {
        if (!boxConstrained)
        {
            //box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            //box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            boxConstrained = true;
        }

    }
    public void PlayUpSound()
    {
        if (!SoundPlayed)
        {
            Audio.clip = SpikeUp;
            Audio.Play();
            SoundPlayed = true;
        }
    }
    public void PlayDownSound()
    {
        if (!SoundPlayed)
        {
            Audio.clip = SpikeDown;
            Audio.Play();
            SoundPlayed = true;
        }
    }
    public void RemoveBox()
    {
        StartCoroutine(DelayTrapBox());
    }
    public IEnumerator DelayTrapBox()
    {
        yield return new WaitForSecondsRealtime(.2f);
        hasBox = false;
    }
}
