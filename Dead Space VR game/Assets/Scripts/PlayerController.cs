using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public GameObject userWeapon;

    public GameObject gun;
    public XRBaseInteractor socketInteractor;

    public AudioSource walkSource;

    bool isMoving;
    Vector3 lastPos;

    public bool isHoldingGun;
    public bool isGunHolstered;
    public Transform holsterAttachPoint;
    public Laser gunLaser;

    public XRDirectInteractor rightInteractor;
    public XRDirectInteractor leftInteractor;
    public XRDirectInteractor rightRangeInteractor;
    public XRDirectInteractor leftRangeInteractor;


    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;

    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;
    public XRRayInteractor leftThrowRay;
    public XRRayInteractor rightThrowRay;


    public GameObject HitBox;
    public GameObject HeadBox;
    private bool grabbingGun = false;




    private void Update()
    {


        UpdateRangeGrabAttachPoint();
        isHoldingGun =  userWeapon.GetComponentInChildren<SimpleShoot>().holdingGun;
        gunLaser.isHolding = isHoldingGun;
        checkIsMoving();
        if(!walkSource.isPlaying && isMoving)
        {
            walkSource.Play();
        }
        if (isHoldingGun == false && isGunHolstered == false && !grabbingGun)
        {
            Invoke("MoveGunToHolster", 1);
        }
        if(isHoldingGun)
        {
            CancelInvoke("MoveGunToHolster");
        }

        /* if (rightInteractor.IsSelecting(userWeapon.GetComponent<XRBaseInteractable>() ) || leftInteractor.IsSelecting(userWeapon.GetComponent<XRBaseInteractable>()) || rightRay.IsSelecting(userWeapon.GetComponent<XRBaseInteractable>()) || leftRay.IsSelecting(userWeapon.GetComponent<XRBaseInteractable>()))
       {
           CancelInvoke("MoveGunToHolster");

           //isGunHolstered = true;
           socketInteractor.enabled = false;
           isHoldingGun = true;



       }
       if (!rightInteractor.IsSelecting(userWeapon.GetComponent<XRBaseInteractable>()) && !leftInteractor.IsSelecting(userWeapon.GetComponent<XRBaseInteractable>()) || !rightRay.IsSelecting(userWeapon.GetComponent<XRBaseInteractable>()) && !leftRay.IsSelecting(userWeapon.GetComponent<XRBaseInteractable>()))
       {
           socketInteractor.enabled = true;
           //  isGunHolstered = false;
           Invoke("NotHolding",0);
       }
       */
        //bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);
        //bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);
        if (isHoldingGun)
        {
            leftThrowRay.gameObject.SetActive(leftActivate.action.ReadValue<float>() > 0.1f && leftCancel.action.ReadValue<float>() == 0);
            leftRay.gameObject.SetActive(!(leftActivate.action.ReadValue<float>() > 0.1f && leftCancel.action.ReadValue<float>() == 0));
            rightThrowRay.gameObject.SetActive(rightActivate.action.ReadValue<float>() > 0.1f && rightCancel.action.ReadValue<float>() == 0);
            rightRay.gameObject.SetActive(!(rightActivate.action.ReadValue<float>() > 0.1f && rightCancel.action.ReadValue<float>() == 0));
        }
        //rightInteractor.enabled = !(rightActivate.action.ReadValue<float>() > 0.1f && rightCancel.action.ReadValue<float>() == 0);
        //leftInteractor.enabled = !(leftActivate.action.ReadValue<float>() > 0.1f && leftCancel.action.ReadValue<float>() == 0);
    }

    void MoveGunToHolster()
    {
        userWeapon.transform.position = holsterAttachPoint.position;
    }



    void NotHolding()
    {
        isHoldingGun = false;
    }


    void IsHolstered()
    {

        isGunHolstered = true;
    }

    void UpdateRangeGrabAttachPoint()
    {
        rightRay.attachTransform = rightInteractor.transform;
        leftRay.attachTransform = leftInteractor.transform;
    }



    public void TryToGrab(SelectEnterEventArgs arg0)
    {
        if(arg0.interactorObject.transform.gameObject.tag  == "LeftHand" || arg0.interactorObject.transform.gameObject.tag == "RightHand")
            grabbingGun = true;

        throw new NotImplementedException();
    }


    public void NotToGrab(SelectExitEventArgs arg0)
    {
        if (arg0.interactorObject.transform.gameObject.tag == "LeftHand" || arg0.interactorObject.transform.gameObject.tag == "RightHand")
            grabbingGun = false;

        throw new NotImplementedException();
    }





    void Start()
    {

        lastPos = transform.position;
        socketInteractor.selectEntered.AddListener(AddGun);
        socketInteractor.selectExited.AddListener(RemoveGun);
        socketInteractor.selectEntered.AddListener(TryToGrab);
        socketInteractor.selectExited.AddListener(NotToGrab);

    }

   /* private void DropGun(SelectEnterEventArgs arg0)
    {
       
        Debug.Log(arg0.interactableObject.transform.gameObject.tag);
        if (arg0.interactableObject.transform.gameObject.tag =="Gun")
        {
            isHoldingGun = false; ;
        }
        throw new NotImplementedException();
    }

    private void GrabGun(SelectEnterEventArgs arg0)
    {
        Debug.Log(arg0.interactableObject.transform.gameObject.tag);
        if (arg0.interactableObject.transform.gameObject.tag == "Gun")
        {
            isHoldingGun = true;
        }
        throw new NotImplementedException();
    }

    */
    public void RemoveGun(SelectExitEventArgs arg0)
    {
        CancelInvoke("IsHolstered");
        gun = null;
        isGunHolstered = false;
      

        throw new NotImplementedException();
    }

    public void AddGun(SelectEnterEventArgs arg0)
    {
        gun = arg0.interactableObject.transform.gameObject;

        Invoke("IsHolstered", 0);
   


        throw new NotImplementedException();
    }

    void checkIsMoving()
    {
        var displacement = transform.position - lastPos;
        lastPos = transform.position;
        if (displacement.magnitude > 0.01)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

    }

}
