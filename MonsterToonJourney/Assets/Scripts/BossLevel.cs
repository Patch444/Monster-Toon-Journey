using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BossLevel : MonoBehaviour
{
    private GameManager gm;

    public float startTime;
    public float bossTimer;

    public Text countdownTxt;

    public Sprite[] mamaSprites = new Sprite[5];

    [SerializeField]
    Sprite mamaMeter;

    // Start is called before the first frame update
    void Start()
    {
        mamaMeter = GameObject.Find("Mama Meter").GetComponent<Image>().sprite;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gm.isBossLevel == true)
        {
            startTime = bossTimer;
            countdownTxt = GameObject.Find("Countdown Text").GetComponent<Text>();
            GameObject.Find("Mama Meter").GetComponent<Image>().sprite = mamaSprites[4];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            BossCountdown();
            if (bossTimer <= (startTime * 0.75) && bossTimer >= (startTime * 0.5))
            {
                GameObject.Find("Mama Meter").GetComponent<Image>().sprite = mamaSprites[3];
            }
            if (bossTimer <= (startTime * 0.5) && bossTimer >= (startTime * 0.25))
            {
                GameObject.Find("Mama Meter").GetComponent<Image>().sprite = mamaSprites[2];
            }
            if (bossTimer <= (startTime * 0.25))
            {
                GameObject.Find("Mama Meter").GetComponent<Image>().sprite = mamaSprites[1];
            }
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
