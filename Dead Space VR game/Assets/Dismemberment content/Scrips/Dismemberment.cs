using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dismemberment : MonoBehaviour
{
     Animator myanim;

    List<Rigidbody> ragdollRigids;
 
   

    // Start is called before the first frame update
    void Start()
    {
        myanim = GetComponent<Animator>();

        ragdollRigids = new List<Rigidbody>(transform.GetComponentsInChildren<Rigidbody>());
        ragdollRigids.Remove(GetComponent<Rigidbody>());
        deactivateragdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void Activateragdoll()
    {
        myanim.enabled = false;
        for(int i = 0; i < ragdollRigids.Count; i++)
        {
            ragdollRigids[i].useGravity = true;
            ragdollRigids[i].isKinematic = false;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    void deactivateragdoll()
    {
        myanim.enabled = true;
        for (int i = 0; i < ragdollRigids.Count; i++)
        {
            ragdollRigids[i].useGravity = false;
            ragdollRigids[i].isKinematic = true;
        }
    }


    public void getkilled()
    {
        Activateragdoll();
    }
}
