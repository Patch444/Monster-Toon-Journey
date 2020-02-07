using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public bool canAppear = true;
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
        //when player comes near allow pickup
        if (other.tag == "Player" && canAppear)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        if (other.tag == "Mimic" && canAppear)
        {
            canAppear = false;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //when player comes near allow pickup
        if (other.tag == "Player")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
