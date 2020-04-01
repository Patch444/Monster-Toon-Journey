using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeftButtonRegrab : MonoBehaviour
{
    public GameManager gm;
    public PlayerMove pm;
    public GameObject button;
    public GameObject box;
    private GameObject fm;
    public bool canInteract;
    private Image boxIcon;
    public Scene currentScene;
    public string sceneName;

    public Button buttonScript;
    public Animator buttonAnim;

    public Vector3 boxStart;

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
            buttonScript = button.GetComponent<Button>();
            buttonAnim = button.GetComponent<Animator>();
            boxStart = box.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.E) && canInteract && pm.hasBox && !buttonScript.hasBox && pm.lastDirection == 1)
            {
                pm.hasBox = false;
                buttonScript.hasBox = true;
                boxIcon.enabled = false;
                boxIcon.transform.SetParent(GameObject.Find("Canvas").transform);
                box.SetActive(true);
                buttonScript.isPressed = true;
                buttonAnim.Play("Button_Press");
                //regrabCollider.SetActive(true);
                //Audio.clip = BoxPlace;
                //Audio.Play();
            }

            // Player picks up box

            else if (Input.GetKeyDown(KeyCode.E) && canInteract && !pm.hasBox && buttonScript.hasBox && pm.lastDirection == 1)
            {
                pm.hasBox = true;
                buttonScript.hasBox = false;
                boxIcon.enabled = true;
                boxIcon.transform.SetParent(fm.transform);
                box.SetActive(false);
                pm.Audio.clip = pm.boxGrab;
                //regrabCollider.SetActive(true);
                pm.Audio.Play();
                buttonScript.isPressed = false;
                buttonAnim.Play("Button_Release");
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
