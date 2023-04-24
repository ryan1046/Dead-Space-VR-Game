using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limb : MonoBehaviour
{
    Dismemberment dismembermentscript;


    [SerializeField] limb[] childlimbs;

    [SerializeField] GameObject limbprefab;
    [SerializeField] GameObject woundhole;

    [SerializeField] GameObject bloodprefap;

    // Start is called before the first frame update
    void Start()
    {
        dismembermentscript = transform.root.GetComponent<Dismemberment>();

        if (woundhole != null)
        {
            woundhole.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit()
    {
        if (childlimbs.Length > 0)
        {
           foreach(limb limb in childlimbs)
            {
                if(limb != null)
                {
                    limb.GetHit();
                }
            }
        }



        if(woundhole != null)
        {
            woundhole.SetActive(true);


            if(bloodprefap != null)
            {
                Instantiate(bloodprefap, transform.position, transform.rotation);
            }
        }

        if(limbprefab != null )
        {
            Instantiate(limbprefab, woundhole.transform.position, woundhole.transform.rotation); 
        }


        transform.localScale = Vector3.zero;

        dismembermentscript.getkilled();

        Destroy(this); 
    }
}
