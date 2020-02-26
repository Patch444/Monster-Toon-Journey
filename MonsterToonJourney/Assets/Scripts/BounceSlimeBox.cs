using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSlimeBox : MonoBehaviour
{
    public bool isRightBox;
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
        if(other.tag == "Slime")
        {
            if(isRightBox == true)
            {
                other.GetComponent<BounceSlime>().moveDirection = -1;
            }
            else
            {
                other.GetComponent<BounceSlime>().moveDirection = 1;
            }
        }
    }

}
