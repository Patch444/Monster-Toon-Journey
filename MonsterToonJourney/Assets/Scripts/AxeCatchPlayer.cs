using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCatchPlayer : MonoBehaviour
{
    GameManager gm;
    public bool hasPlayer;
    GameObject player;
    GameObject playerCamera;
    public GameObject rotationParent;
    public GameObject positionSetter; //This is stupid but I'm desperate;

    public PendulumSwing pendulum;

    Vector3 start;
    public Vector3 target;
    public float timeToMove;
    public float launchForce;

    public float cameraOffsetx;
    public float cameraOffsety;
    public float cameraOffsetz;
    // Start is called before the first frame update
    void Start()
    {
        pendulum = this.gameObject.GetComponent<PendulumSwing>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerCamera = GameObject.Find("Main Camera");
        target = this.transform.position - 3 * Vector3.up;

        //timeToMove = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(positionSetter == null)
        {
            positionSetter = GameObject.Find("PositionSetter");
        }
        if (!gm.isPaused)
        {

            if (hasPlayer)
            {
                playerCamera.transform.position = new Vector3(player.transform.position.x + cameraOffsetx, playerCamera.transform.position.y,
                    player.transform.position.z + cameraOffsetz);

                //player.transform.localPosition = positionSetter.transform.localPosition;
                //player.transform.position = this.transform.position;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    hasPlayer = false;
                    //player.GetComponent<Rigidbody2D>().simulated = true;
                    if (transform.rotation.z >= 0)
                    {
                        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(launchForce, 1), ForceMode2D.Impulse);
                    }
                    else if (transform.rotation.z < 0)
                    {
                        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-launchForce, 1), ForceMode2D.Impulse);
                    }
                    player.transform.SetParent(null);
                    player.transform.rotation = Quaternion.identity;
                    playerCamera.transform.SetParent(player.transform);
                    playerCamera.transform.localPosition = new Vector3(cameraOffsetx, cameraOffsety, cameraOffsetz);
                    //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    player.GetComponent<Rigidbody2D>().gravityScale = 1;
                    //player.GetComponent<Rigidbody2D>().simulated = true;
                    player.gameObject.GetComponent<PlayerMove>().canWalk = true;
                    player.gameObject.GetComponent<PlayerMove>().canGlide = true;

                }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hasPlayer = true;
            player = collision.gameObject;
            playerCamera.transform.SetParent(null);
            playerCamera.transform.position = new Vector3(player.transform.position.x + cameraOffsetx, 
                player.transform.position.y + cameraOffsety, player.transform.position.z + cameraOffsetz);
            //player.transform.SetParent(rotationParent.transform);
            player.transform.SetParent(positionSetter.transform);
            player.transform.localPosition = Vector3.zero;
            //player.transform.position = target;
            //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            //player.GetComponent<Rigidbody2D>().gravityScale = 0;
            //player.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            player.gameObject.GetComponent<PlayerMove>().canWalk = false;
            player.gameObject.GetComponent<PlayerMove>().canGlide = false;
            player.gameObject.GetComponent<PlayerMove>().isSwinging = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            hasPlayer = false;
            //player.GetComponent<Rigidbody2D>().simulated = true;
            //if (transform.rotation.z > 0)
            //{
            //    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(launchForce, 1), ForceMode2D.Impulse);
            //}
            //else if (transform.rotation.z < 0)
            //{
            //    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-launchForce, 1), ForceMode2D.Impulse);
            //}
            player.transform.SetParent(null);
            player.transform.rotation = Quaternion.identity;
            playerCamera.transform.SetParent(player.transform);
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
            player.gameObject.GetComponent<PlayerMove>().canWalk = true;
            player.gameObject.GetComponent<PlayerMove>().canGlide = true;
            player.gameObject.GetComponent<PlayerMove>().isSwinging = false;
            //Hey Patrick is it working?
        }
    }
}
