using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;

    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);

        leftTeleportation.SetActive(!isLeftRayHovering && leftActivate.action.ReadValue<float>() > 0.1f && leftCancel.action.ReadValue<float>() == 0);
        rightTeleportation.SetActive(!isRightRayHovering && rightActivate.action.ReadValue<float>() > 0.1f && rightCancel.action.ReadValue<float>() == 0);
        //leftTeleportation.SetActive(leftCancel.action.ReadValue<float>() == 0);
       // rightTeleportation.SetActive(rightCancel.action.ReadValue<float>() == 0);
    }
}
