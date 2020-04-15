using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeftBoxReGrab : MonoBehaviour
{
    GameManager gm;
    private PlayerMove pm;
    public GameObject spikeTrap;
    public GameObject box;
    private GameObject fm;
    public bool canInteract;
    private Image boxIcon;
    public Scene currentScene;
    public string sceneName;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.E) && canInteract && !pm.hasBox && pm.lastDirection == 1 && !gm.isPaused)
            {
                pm.hasBox = true;
                spikeTrap.GetComponent<SpikeTrap>().RemoveBox();
                box.GetComponent<BoxLaunch>().BoxReset();
                boxIcon.enabled = true;
                boxIcon.transform.SetParent(fm.transform);
                pm.Audio.clip = pm.boxGrab;
                pm.Audio.Play();
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
