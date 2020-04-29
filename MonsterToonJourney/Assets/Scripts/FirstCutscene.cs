using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCutscene : MonoBehaviour
{
    public SceneManager sm;

    public float endTime;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
        if (timer >= endTime)
        {
            StartCoroutine(Delay());
        }
        if (Input.anyKey)
        {
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
        yield return new WaitForSecondsRealtime(3);
        PlayerPrefs.SetInt("HowFar", 1);
        sm.ToLevelSelect();
    }
}
