using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstCutscene : MonoBehaviour
{
    public SceneManager sm;

    public float endTime;
    public float timer;

    public Image blackScreen;
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;

    // Start is called before the first frame update
    void Start()
    {
        image2.enabled = false;
        image3.enabled = false;
        image4.enabled = false;
        image5.enabled = false;

        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        timer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
        if (timer < 3.5f)
        {
            blackScreen.CrossFadeAlpha(0, 0.5f, false);
        }
        if (timer >= 4f)
        {
            image2.enabled = true;
            image1.CrossFadeAlpha(0, 0.5f, false);
        }
        if (timer >= 8f)
        {
            image3.enabled = true;
            image2.CrossFadeAlpha(0, 0.5f, false);
        }
        if (timer >= 12f)
        {
            image4.enabled = true;
            image3.CrossFadeAlpha(0, 0.5f, false);
        }
        if (timer >= 16f)
        {
            image5.enabled = true;
            image4.CrossFadeAlpha(0, 0.5f, false);
        }
        if (timer >= endTime)
        {
            StartCoroutine(Delay());
        }
        if (Input.anyKey)
        {
            image5.CrossFadeAlpha(0, 0.5f, false);
            PlayerPrefs.SetInt("HowFar", 1);
            sm.ToLevelSelect();
        }
    }

    public void StartTimer()
    {
        timer = timer + Time.deltaTime;
    }

    public IEnumerator Delay()
    {
        image5.CrossFadeAlpha(0, 0.5f, false);
        yield return new WaitForSecondsRealtime(3);
        PlayerPrefs.SetInt("HowFar", 1);
        sm.ToLevelSelect();
    }
}
