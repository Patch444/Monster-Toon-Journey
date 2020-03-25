using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed;
    public int moveDirection;
    public int lastMoveDirection;
    public bool isSidewaysPlatform;
    public Grid grid;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            if (isSidewaysPlatform == true)
            {
                MoveSideWays();
            }
            else
            {
                MoveVertical();
            }
        }
        else
        {
            
        }

    }

    public void MoveSideWays()
    {
        if (moveDirection == 1)
        {
            lastMoveDirection = 1;
            transform.position += Vector3.right * Time.deltaTime * platformSpeed;
        }
        if (moveDirection == -1)
        {
            lastMoveDirection = -1;
            transform.position += Vector3.left * Time.deltaTime * platformSpeed;
        }
    }

    public void MoveVertical()
    {
        if (moveDirection == 1)
        {
            lastMoveDirection = 1;
            transform.position += Vector3.up * Time.deltaTime * platformSpeed;
        }
        if (moveDirection == -1)
        {
            lastMoveDirection = -1;
            transform.position += Vector3.down * Time.deltaTime * platformSpeed;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = grid.transform;
        }
    }


}
