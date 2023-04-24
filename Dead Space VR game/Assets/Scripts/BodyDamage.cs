using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BodyDamage : MonoBehaviour
{
    /*
    public GameObject head;
    public GameObject deadHead;
    public int headHealth = 100;

    public GameObject animationRig;

    public GameObject body;
    public GameObject deadBody;
    public int bodyHealth = 100;


    public GameObject rightLeg;
    public GameObject leftLeg;
    public GameObject deadrightLeg;
    public GameObject deadleftLeg;

    public int leftLegHealth = 100;
    public int rightLegHealth = 100;


    public GameObject rightArm;
    public GameObject leftArm;

    public GameObject deadrightArm;
    public GameObject deadleftArm;
    public int leftArmHealth = 100;
    public int rightArmHealth = 100;
    */

    public LimbDamage[] Limbs;


    public GameObject animationRig;
    public GameObject zombie;

    public bool hasDied = false;

    int numLimbsDestroyed = 6;
    //public bool hasTorso = true;
    public int armCount = 2;
    public int legCount = 2;


    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

        Limbs = zombie.GetComponentsInChildren<LimbDamage>();
        if(Limbs.Length < 4 || armCount == 0)
        {
            hasDied = true;
        }
        if(legCount != 2)
        {
            zombie.GetComponent<EnemyAI>().updateIsMissingLegs(true);
         
        }
        //Changed to Cant Damage Torso
        /* foreach(LimbDamage limb in Limbs) 
         {
             if (limb.LimbType == "Body")
             {
                 hasTorso = true;
                 break;
             }
             else
             {
                 hasTorso = false;
             }
         }
         */
        int currentArmCount = 0;
        int currentLegCount = 0;
        foreach (LimbDamage limb in Limbs)
        {
            if (limb.LimbType == "Arm")
            {
                currentArmCount += 1;
            
            }
            if(limb.LimbType == "Leg")
            {
                currentLegCount += 1;
            }
          
        }
        armCount = currentArmCount;
        legCount = currentLegCount;




        if (hasDied == true)
        {
            EnemyDie();

        }


        /*
        CheckHealth(headHealth, head, deadHead);
        CheckHealth(bodyHealth, body, deadBody);
        CheckHealth(leftArmHealth, leftArm, deadleftArm);
        CheckHealth(rightArmHealth, leftArm, deadrightArm);
        CheckHealth(leftLegHealth, leftLeg, deadleftLeg);
        CheckHealth(rightLegHealth, rightLeg, deadrightLeg);
        */

    }


    void EnemyDie()
    {
        /*
        //NEED TO Improve
        if(head != null)
         SpawnBodyPart(head, deadHead);
        if (body!= null)
            SpawnBodyPart(body, deadBody);
        if (leftArm != null)
            SpawnBodyPart(leftArm, deadleftArm);
        if (leftLeg != null)
            SpawnBodyPart(leftLeg, deadleftLeg);
        if (rightArm != null)
            SpawnBodyPart(rightArm, deadrightArm);
        if (rightLeg != null)
            SpawnBodyPart(rightLeg, deadrightLeg);
        
            DestoryBodyPart(head, deadHead);
        DestoryBodyPart(body, deadBody);
        DestoryBodyPart(leftArm, deadleftArm);
        DestoryBodyPart(leftLeg, deadleftLeg);
        DestoryBodyPart(rightArm, deadrightArm);
        DestoryBodyPart(rightLeg, deadrightLeg);
        */
        for (int i = 0; i < Limbs.Length; i++)
        {
            Limbs[i].health = 0;
        }



        zombie.GetComponent<EnemyAI>().enabled = false;
        zombie.GetComponent<Animator>().enabled = false;
        zombie.GetComponent<NavMeshAgent>().enabled = false;
        zombie.GetComponent<CapsuleCollider>().enabled = false;

        /* DisableRenderer(head);
         DisableRenderer(body);
         DisableRenderer(rightArm);
         DisableRenderer(rightLeg);
         DisableRenderer(leftArm);
         DisableRenderer(leftLeg);
         */
        Destroy(animationRig);
     



    }







    void DisableRenderer(GameObject toDisable)
    {
        toDisable.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }





    /*
    void SpawnBodyPart(GameObject killedBodyPart, GameObject spawnBodyPart)
    {
        Instantiate(spawnBodyPart, killedBodyPart.GetComponentInChildren<BoxCollider>().transform.position, Quaternion.identity);

        //Destroy(killedBodyPart);




    }



    void CheckHealth(int checkHealth, GameObject bodyPart, GameObject deadBodyPart)
    {
        if (bodyHealth <= 0 && bodyPart != null)
        {
            SpawnBodyPart(bodyPart, deadBodyPart);
            DestoryBodyPart(bodyPart, deadBodyPart);
        }
    }



    void DestoryBodyPart(GameObject killedBodyPart, GameObject spawnBodyPart)
    {
       // Instantiate(spawnBodyPart, killedBodyPart.GetComponentInChildren<BoxCollider>().transform.position,  Quaternion.identity);
       
        Destroy(killedBodyPart);

    }
    */


}
