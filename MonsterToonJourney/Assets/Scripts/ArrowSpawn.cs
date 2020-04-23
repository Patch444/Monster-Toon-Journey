using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrowSpawn : MonoBehaviour
{
    // Sets up bools to determine which direction the launcher will fire arrows.
    public bool isRightArrowLauncher;
    public bool isActive;

    // Sets up the arrows.
    public GameObject rightArrow;
    public GameObject leftArrow;
    private GameObject currentArrow;
    private GameManager gm;

    // Sets up where the arrows will launch.
    private Vector3 launchPoint;

    public AudioClip arrowLaunchSound;

    public AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        launchPoint = transform.position;
        SpawnArrow();
        Audio = GetComponent<AudioSource>();
        //Audio.volume = PlayerPrefs.GetFloat("FxVolume");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            if (currentArrow == null && isActive == true)
            {
                SpawnArrow();
                Audio.clip = arrowLaunchSound;
                Audio.Play();
            }
        }

    }

    // Spawns an arrow.
    public void SpawnArrow()
    {
        // Checks if the launcher will fire right.
        if (isRightArrowLauncher == true)
        {
            // Spawns a right-going arrow.
            currentArrow = Instantiate(rightArrow, launchPoint, Quaternion.identity);
        }
        // Spawns a left-going arrow.
        else
        {
            currentArrow = Instantiate(leftArrow, launchPoint, Quaternion.identity);
        }
    }
}
