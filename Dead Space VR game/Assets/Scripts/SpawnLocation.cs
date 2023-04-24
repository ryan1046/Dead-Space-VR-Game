using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
   public bool hasSpawnedEnemy;
    public float maxSpawnRange = 30;
   public float minSpawnRange = 10;

    public bool inSpawnRange;
    public bool spawnInPlayerSight;

    public GameObject enemySpawn;
    public GameObject player;

    public LayerMask isPlayer;
    public Transform playerHitBox;


    public LayerMask ignoreLayer;

    public bool hiddenSpawn;

    public AudioSource spawnSound;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        CheckIsInLineOfSight();
        CheckInSpawnRange();
    }

    public void spawnEnemy(GameObject enemyToSpawn)
    {
        if (spawnInPlayerSight == false || hiddenSpawn == true)
        {
            if (inSpawnRange == true)
            {
                if (hasSpawnedEnemy == false)
                {
                    if (spawnSound != null)
                    {
                        spawnSound.Play();
                    }
                    Instantiate(enemyToSpawn, this.transform.position, this.transform.rotation);
                    hasSpawnedEnemy = true;
                }

            }

        }

      
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minSpawnRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxSpawnRange);
    }


    void CheckInSpawnRange()
    {

        /*Collider[] maxRangeColliders = Physics.OverlapSphere(transform.position, maxSpawnRange);
        Collider[] minRangeColliders = Physics.OverlapSphere(transform.position, minSpawnRange);
        */
        
        /*foreach (var hitCollider in minRangeColliders)
        {
            if (hitCollider.gameObject == enemySpawn)
            {
                inSpawnRange = false;
                return;
            }
        }
        foreach (var hitCollider in maxRangeColliders)
        {
            if (hitCollider.gameObject == enemySpawn)
            {
                inSpawnRange = true;
                return;

            }
        }
        */
        inSpawnRange = false;
        Debug.Log(Physics.CheckSphere(enemySpawn.transform.position, maxSpawnRange, isPlayer));
        if (Physics.CheckSphere(enemySpawn.transform.position, maxSpawnRange, isPlayer))
       {
            inSpawnRange = true;
        }
        if (Physics.CheckSphere(enemySpawn.transform.position, minSpawnRange, isPlayer))
       {
           inSpawnRange = false;
        }
      
        //  playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
    }

    void CheckIsInLineOfSight()
    {
        var rayDirection = enemySpawn.transform.position - playerHitBox.position;
        Ray ray = new Ray(enemySpawn.transform.position, playerHitBox.position - enemySpawn.transform.position);
        RaycastHit hit;

        // Debug.Log(hit.transform.name);
        //Debug.Log(hit.transform.tag);
        Debug.DrawRay(playerHitBox.position, rayDirection, Color.yellow);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayer))
        {
            Debug.Log(hit.transform.name);
            Debug.Log(hit.transform.tag);
            // Debug.DrawRay(,ray, Color.yellow);
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("IF IS WORKING");
                spawnInPlayerSight = true;
                return;
            }


        }

        spawnInPlayerSight = false;


    }





}
