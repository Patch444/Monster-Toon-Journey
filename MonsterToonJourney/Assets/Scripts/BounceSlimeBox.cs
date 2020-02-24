using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceSlimeBox : MonoBehaviour
{
    public BounceSlime slime;
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
                slime.moveDirection = -1;
            }
            else
            {
                slime.moveDirection = 1;
            }
        }
    }

}
