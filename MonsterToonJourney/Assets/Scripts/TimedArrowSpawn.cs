using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedArrowSpawn : MonoBehaviour
{
    // Sets up bools to determine which direction the launcher will fire arrows.
    public bool isRightArrowLauncher;
    public bool isActive;

    public float spawnTimer;
    public float spawnTimerDuration;

    // Sets up the arrows.
    public GameObject rightArrow;
    public GameObject leftArrow;
    private GameManager gm;


    // Sets up where the arrows will launch.
    private Vector3 launchPoint;

    public AudioClip arrowLaunchSound;

    public AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTimerDuration;
        launchPoint = transform.position;
        SpawnArrow();
        Audio = GetComponent<AudioSource>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            SpawnCountdown();
            if (spawnTimer <= 0 && isActive == true)
            {
                SpawnArrow();
                Audio.clip = arrowLaunchSound;
                Audio.Play();
                spawnTimer = spawnTimerDuration;
            }
        }
    }

    public void SpawnCountdown()
    {
        spawnTimer = spawnTimer - Time.deltaTime;
    }

    // Spawns an arrow.
    public void SpawnArrow()
    {
        // Checks if the launcher will fire right.
        if (isRightArrowLauncher == true)
        {
            // Spawns a right-going arrow.
            Instantiate(rightArrow, launchPoint, Quaternion.identity);
        }
        // Spawns a left-going arrow.
        else
        {
            Instantiate(leftArrow, launchPoint, Quaternion.identity);
        }
    }
}



