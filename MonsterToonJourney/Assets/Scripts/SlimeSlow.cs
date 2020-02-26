using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSlow : MonoBehaviour
{
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
        if (other.tag == "Slime")
        {
            other.GetComponent<BounceSlime>().slimeSpeed = 1;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Slime")
        {
            other.GetComponent<BounceSlime>().slimeSpeed = 2;
        }
    }
}
