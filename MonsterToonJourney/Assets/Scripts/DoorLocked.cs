using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorLocked : MonoBehaviour
{
    GameManager gm;
    private PlayerMove pm;
    private Image keyIcon;
    private GameObject fm;
    private Animator anim;

    public List<BoxCollider2D> colliders;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("Player").GetComponent<PlayerMove>();
        keyIcon = GameObject.Find("Key Icon").GetComponent<Image>();
        fm = GameObject.Find("Fear Meter");
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && pm.hasKey && !gm.isPaused)
        {
            anim.Play("DoorLocked_Opening");
            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = false;
            }
            pm.hasKey = false;
            pm.Audio.clip = pm.keyUse;
            pm.Audio.Play();
            keyIcon.enabled = false;
            keyIcon.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }
}
