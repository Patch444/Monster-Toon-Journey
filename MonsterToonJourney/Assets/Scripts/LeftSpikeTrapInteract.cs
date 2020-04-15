using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeftSpikeTrapInteract : MonoBehaviour
{
    public GameManager gm;
    public PlayerMove pm;
    public GameObject spikeTrap;
    public GameObject box;
    private GameObject fm;
    public bool canInteract;
    private Image boxIcon;
    public Scene currentScene;
    public string sceneName;

    public Vector3 boxStart;

    public SpikeTrap trapScript;


    // Start is called before the first frame update
    void Start()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (sceneName != "Intro")
        {
            pm = GameObject.Find("Player").GetComponent<PlayerMove>();
            boxIcon = GameObject.Find("Box Icon").GetComponent<Image>();
            fm = GameObject.Find("Fear Meter");
            trapScript = spikeTrap.GetComponent<SpikeTrap>();
            boxStart = box.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.E) && canInteract && pm.hasBox && !trapScript.hasBox && pm.lastDirection == 1)
            {
                pm.hasBox = false;
                trapScript.hasBox = true;
                boxIcon.enabled = false;
                boxIcon.transform.SetParent(GameObject.Find("Canvas").transform);
                box.SetActive(true);
                trapScript.Audio.clip = trapScript.BoxPlace;
                trapScript.Audio.Play();
                box.transform.position = boxStart;
            }
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

}
