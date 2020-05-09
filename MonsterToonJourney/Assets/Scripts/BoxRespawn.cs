using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxRespawn : MonoBehaviour
{
    GameManager gm;
    private PlayerMove pm;
    

    private GameObject fm;
    public GameObject ogBox;

    public List<GameObject> counterparts;
    public List<BoxRespawnPoint> brps;
    public List<GameObject> ogBoxes;
    public List<SpikeTrap> sts;
    public List<Button> btns;

    public int counterpartCount;

    public Animator anim;

    public bool canInteract;
    public bool hasSpikes;
    public bool hasButtons;

    private Image boxIcon;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        boxIcon = GameObject.Find("Box Icon").GetComponent<Image>();
        fm = GameObject.Find("Fear Meter");
   
        anim = this.GetComponent<Animator>();

        foreach (GameObject respawner in counterparts)
        {
            brps.Add(respawner.GetComponent<BoxRespawnPoint>());
        }

        foreach (GameObject spike in counterparts)
        {
            sts.Add(spike.GetComponent<SpikeTrap>());
        }

        foreach (GameObject button in counterparts)
        {
            btns.Add(button.GetComponent<Button>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            anim.enabled = true;
            if (Input.GetKeyDown(KeyCode.E) && canInteract)
            {
                foreach (GameObject ogBox in ogBoxes)
                {
                    if (ogBox == null)
                    {
                        if (hasSpikes == true)
                        {
                            foreach (SpikeTrap spike in sts)
                            {
                                if (spike != null && spike.hasBox == true)
                                {
                                    spike.hasBox = false;
                                    spike.box.SetActive(false);
                                    spike.spikes.GetComponent<BoxCollider2D>().enabled = true;
                                }
                            }
                        }
                        if (hasButtons == true)
                        {
                            foreach (Button button in btns)
                            {
                                if (button != null && button.hasBox == true)
                                {
                                    button.hasBox = false;
                                    button.isPressed = false;
                                    button.box.SetActive(false);
                                    button.anim.Play("Button_Release");
                                }
                            }
                        }

                        if (pm.hasBox == true)
                        {
                            pm.hasBox = false;
                            boxIcon.enabled = false;
                            boxIcon.transform.SetParent(GameObject.Find("Canvas").transform);
                        }
                        foreach (BoxRespawnPoint brp in brps)
                        {
                            if (brp != null && ogBox == null)
                            {
                                brp.SpawnBox();
                            }
                        }
                        anim.Play("Lever_Pull");
                    }
                }
            }
            
    }
    else
        {
            anim.enabled = false;
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
        //when player moves out of range reset canInteract
        if (other.tag == "Player")
        {
            canInteract = false;
        }
    }
}
