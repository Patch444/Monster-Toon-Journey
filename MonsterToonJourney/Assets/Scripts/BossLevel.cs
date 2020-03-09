using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BossLevel : MonoBehaviour
{
    private GameManager gm;

    public float bossTimer;

    public Text countdownTxt;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gm.isBossLevel == true)
        {
            countdownTxt = GameObject.Find("Countdown Text").GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            BossCountdown();
            if (bossTimer <= 0f)
            {
                gm.GameOver();
            }
        }
    }

    public void BossCountdown()
    {
        bossTimer = bossTimer - Time.deltaTime;
        if (bossTimer >= 0)
        {
            countdownTxt.text = bossTimer.ToString("F0");
        }
    }


}
