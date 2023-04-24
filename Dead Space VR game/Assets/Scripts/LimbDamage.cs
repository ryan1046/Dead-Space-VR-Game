using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbDamage : MonoBehaviour
{

   

    public GameObject spawnBodyPart;
    public int health = 100;
    public Collider[] colliders;
    public string LimbType;
    public GameObject bloodPrefap;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        colliders = this.gameObject.GetComponentsInChildren<Collider>();
        CheckHealth();
    }

    void CheckHealth()
    {
        if (health <= 0)
        {
            SpawnBodyPart();
            DestoryBodyPart();
        }
    }





    public void SpawnBodyPart()
    {
        Instantiate(spawnBodyPart, this.gameObject.GetComponentInChildren<BoxCollider>().transform.position, Quaternion.identity);

        //Destroy(killedBodyPart);




    }

   public void TakeDamage(int damage)
    {
        if(LimbType == "Body")
        {
            return;
        }
        health -= damage;
    }



    public void DestoryBodyPart()
    {
        // Instantiate(spawnBodyPart, killedBodyPart.GetComponentInChildren<BoxCollider>().transform.position,  Quaternion.identity);
        if (bloodPrefap != null)
        {
            Instantiate(bloodPrefap, this.gameObject.GetComponentInChildren<BoxCollider>().transform.position, this.gameObject.GetComponentInChildren<BoxCollider>().transform.rotation);
        }
        Destroy(this.gameObject);




    }

}
