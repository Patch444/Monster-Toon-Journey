using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAnimateControl : MonoBehaviour
{
    [SerializeField]
    GameManager gm;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isPaused == true)
        {
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
        }
    }
}
