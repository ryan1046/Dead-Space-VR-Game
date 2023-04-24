using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelase;
    GameObject presser;
    public GameObject Door;

    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0.015f, 0);
            onRelase.Invoke();
            isPressed = false;
        }
    }

    public void OpenCloseDoor()
    {
        Animator animate = Door.GetComponent<Animator>();
        bool isOpen = animate.GetBool("button_Pressed");
        Debug.Log(isOpen);
        animate.SetBool("button_Pressed", !isOpen );
    }



    //Used for testing
    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(-10, 3, -4);
        sphere.AddComponent<Rigidbody>();
    }


}
