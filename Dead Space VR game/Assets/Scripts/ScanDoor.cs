using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanDoor : door
{
    public int doorID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DoorCard")
        {
            if (other.gameObject.GetComponent<CardScanner>().getCardNum() == doorID)
            {
                OpenCloseDoor();
            }
        }
    }

    public void OpenCloseDoor()
    {
        Animator animate = Door.GetComponent<Animator>();
        bool isOpen = animate.GetBool("button_Pressed");
        Debug.Log(isOpen);
        animate.SetBool("button_Pressed", !isOpen);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
