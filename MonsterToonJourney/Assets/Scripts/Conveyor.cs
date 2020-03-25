using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Animator anim;
    public GameManager gm;
    public bool isTopWheel;
    public bool isMiddle;
    public bool isMiddleWheel;
    public bool isBottomWheel;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            anim.enabled = true;
            if (isTopWheel == true)
            {
                anim.Play("Conveyor_TopWheel");
            }
            if (isMiddle == true)
            {
                anim.Play("Conveyor_Middle");
            }
            if (isMiddleWheel == true)
            {
                anim.Play("Conveyor_MiddleWheel");
            }
            if (isBottomWheel == true)
            {
                anim.Play("Conveyor_BottomWheel");
            }
        }
        else
        {
            anim.enabled = false;
        }
    }
}
