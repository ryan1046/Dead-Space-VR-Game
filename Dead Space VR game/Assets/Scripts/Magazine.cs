using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{

    public int bulletsLeft = 200;

    private void Update()
    {
        if(bulletsLeft <= 0)
        {
            this.gameObject.tag = "Untagged";
        }
    }

}
