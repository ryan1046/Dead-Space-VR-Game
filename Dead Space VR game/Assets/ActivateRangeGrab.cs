using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class ActivateRangeGrab : MonoBehaviour
{
    public GameObject leftGrabRay;
    public GameObject RightGrabRay;

    public XRDirectInteractor leftHand;
    public XRDirectInteractor rightHand;

    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

   // bool isLeftGrabbing;
   // bool isRightGrabbing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        bool isLeftGrabbing = leftHand.hasSelection;
       
       bool isRightGrabbing = rightHand.hasSelection; 
        



        leftGrabRay.SetActive(isLeftGrabbing);
        RightGrabRay.SetActive(isRightGrabbing);
    }
}
