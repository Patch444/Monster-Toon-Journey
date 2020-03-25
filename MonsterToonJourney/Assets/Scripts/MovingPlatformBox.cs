using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBox : MonoBehaviour
{
    public bool isRightTopBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Will change to "MovingPlatform" later.
        if (other.tag == "Ground")
        {
            if (isRightTopBox == true)
            {
                other.GetComponent<MovingPlatform>().moveDirection = -1;
            }
            else
            {
                other.GetComponent<MovingPlatform>().moveDirection = 1;
            }
        }
    }
}
