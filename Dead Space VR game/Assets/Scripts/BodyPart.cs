using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{



    public GameObject myBodyPart;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        SkinnedMeshRenderer bodyMesh = myBodyPart.GetComponent<SkinnedMeshRenderer>();
        this.transform.position = bodyMesh.rootBone.position;
        this.transform.rotation = bodyMesh.rootBone.rotation;
        // float xRotation = this.transform.rotation.x - 90f;
        // Vector3 rotationToAdd = new Vector3(-45, 0, 0);
        //bodyMesh.localBounds;
        //this.transform.position = bodyMesh.localBounds.center;
        this.transform.Translate(bodyMesh.localBounds.center);
        //this.transform.Rotate(rotationToAdd);
    }
}
