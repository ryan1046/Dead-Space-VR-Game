using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public Rigidbody rb;
    public int damage = 10;
    public float radius = 1;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //dealDamage(collision.gameObject);
        if (rb == collision.collider.attachedRigidbody)
        {
           // return;
        }
        Collider[] cols = Physics.OverlapSphere(this.transform.position, radius);
        foreach(Collider col in cols)
        {
            if (col.tag != "HitBox")
            {
                continue;
            }
            Debug.Log(col.gameObject.name);
            dealDamage(col.gameObject);
            return;
        } 
    }



    // Start is called before the first frame update
    public void dealDamage(GameObject target)
    {
     
        target.GetComponentInParent<LimbDamage>().TakeDamage(damage);

    }
}
