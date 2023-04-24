using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public LineRenderer laser;
    public LayerMask ignoreLayer;
    public bool isHolding;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
            laser.SetPosition(0, transform.position);
            RaycastHit hit;
            if (isHolding)
            {
            laser.enabled = true;
            if (Physics.Raycast(transform.position, transform.forward, out hit, ignoreLayer))
                {
                    if (hit.collider)
                    {
                        laser.SetPosition(1, hit.point);
                    }

                }
                else
                {
                    laser.SetPosition(1, transform.forward * 10000);
                }
            }
         else
        {
            laser.enabled = false;
         }


    }




}
