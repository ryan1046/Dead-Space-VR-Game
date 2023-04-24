using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour
{
    public bool isshot;
    public GameObject bleed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isshot)
        {
            Instantiate(bleed, transform.position,
                 transform.rotation);
        }

        isshot = false;
    }
}
