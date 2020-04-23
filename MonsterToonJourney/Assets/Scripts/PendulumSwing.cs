using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    GameManager gm;
    GameObject player;
    AxeCatchPlayer catcher;

    Quaternion targetAngle;
    Quaternion startAngle;

    Vector3 startPosition;
    Vector3 targetPositionRight;
    Vector3 targetPositionLeft;

    [SerializeField]
    float maxAngle;
    [SerializeField]
    float timeToMove;
    [SerializeField]
    float multiplier;

    public AudioClip axeSwing;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        catcher = this.GetComponent<AxeCatchPlayer>();
        audio = GetComponent<AudioSource>();
        //audio.volume = PlayerPrefs.GetFloat("FxVolume");

        targetAngle = new Quaternion(0, 0, maxAngle / 180, 1);
        targetPositionRight = catcher.target + 2 * Vector3.right;
        targetPositionLeft = catcher.target + 2 * Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            catcher.rotationParent.transform.rotation = this.transform.rotation;
            if (timeToMove == 0)
            {
                startAngle = transform.rotation;
                startPosition = player.transform.position;
            }
            if (targetAngle.z > 0)
            {
                if (maxAngle > 0)
                {
                    if (Mathf.Abs(maxAngle / 180 - transform.rotation.z) > 0.002)
                    {
                        //Debug.Log(transform.rotation.z + " " + maxAngle / 180);

                        timeToMove += Time.deltaTime;
                        transform.rotation = Quaternion.Lerp(startAngle, targetAngle, timeToMove);
                        if (catcher.hasPlayer)
                        {
                            //catcher.rotationParent.transform.position += Vector3.right * multiplier * Time.deltaTime;
                            //player.transform.position = Vector3.Lerp(startPosition, targetPositionRight, timeToMove);
                        }
                    }
                    else
                    {
                        timeToMove = 0;
                        startAngle = transform.rotation;
                        targetAngle = new Quaternion(0, 0, targetAngle.z * (-1), 1);
                    }
                }
                else if (maxAngle < 0)
                {
                    if (Mathf.Abs(-maxAngle / 180 - transform.rotation.z) > 0.002)
                    {
                        //Debug.Log(transform.rotation.z + " " + maxAngle / 180);
                        timeToMove += Time.deltaTime;
                        transform.rotation = Quaternion.Lerp(startAngle, targetAngle, timeToMove);
                        if (catcher.hasPlayer)
                        {
                            //player.transform.position += Vector3.left * 8 * Time.deltaTime;
                        }
                    }
                    else
                    {
                        timeToMove = 0;
                        startAngle = transform.rotation;
                        startPosition = player.transform.position;
                        targetAngle = new Quaternion(0, 0, targetAngle.z * (-1), 1);
                    }
                }

            }
            else if (targetAngle.z < 0)
            {
                if (maxAngle > 0)
                {
                    if (Mathf.Abs(-maxAngle / 180 - transform.rotation.z) > 0.002)
                    {
                        //Debug.Log(transform.rotation.z + " " + maxAngle / 180);
                        timeToMove += Time.deltaTime;
                        transform.rotation = Quaternion.Lerp(startAngle, targetAngle, timeToMove);
                        //if (catcher.hasPlayer)
                        //player.transform.position += Vector3.right *8* Time.deltaTime;
                        //player.transform.position = Vector3.Lerp(startPosition, targetPositionLeft, timeToMove);

                    }
                    else
                    {
                        timeToMove = 0;
                        startAngle = transform.rotation;
                        targetAngle = new Quaternion(0, 0, targetAngle.z * (-1), 1);
                    }
                }
                else if (maxAngle < 0)
                {
                    if (Mathf.Abs(maxAngle / 180 - transform.rotation.z) > 0.002)
                    {
                        //Debug.Log(transform.rotation.z + " " + maxAngle / 180);

                        timeToMove += Time.deltaTime;
                        transform.rotation = Quaternion.Lerp(startAngle, targetAngle, timeToMove);
                        //if(catcher.hasPlayer)
                        //player.transform.position = Vector3.Lerp(startPosition, targetPositionRight, timeToMove);
                    }
                    else
                    {
                        timeToMove = 0;
                        startAngle = transform.rotation;
                        targetAngle = new Quaternion(0, 0, targetAngle.z * (-1), 1);
                    }
                }

            }
        }



    }
}
