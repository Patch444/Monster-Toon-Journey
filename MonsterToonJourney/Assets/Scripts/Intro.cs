using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public Camera introCam;

    public SceneManager sm;

    public Vector3 currentPoint;

    public float endDistance;

    public float timer;

    public int scrollSpeed;

    public float distanceTraveled;

    
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        endDistance = 110.9f;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        StartTimer();
        currentPoint = introCam.transform.localPosition;
        distanceTraveled = currentPoint.magnitude;
        if (distanceTraveled < endDistance && timer > 3.0f)
        {
            introCam.transform.localPosition += Vector3.down * Time.deltaTime * scrollSpeed;
        }
        if (distanceTraveled >= endDistance)
        {
            StartCoroutine("Delay");
        }
        if (Input.anyKey)
        {
            sm.ToMainMenu();
        }
    }

    public void StartTimer()
    {
        timer = timer + Time.deltaTime;
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(3);
        sm.ToMainMenu();
    }
}
