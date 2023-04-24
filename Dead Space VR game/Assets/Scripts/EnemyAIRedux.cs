using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAIRedux : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject Model;
    public Animator animate;
    public Transform player;
    public Transform playerHitBox;


    public Transform headPos;





    public LayerMask isGround, isPlayer;

    public Vector3 walkPoint;

    bool walkPointSet;

    public bool isAttacking, isMoving;

    //public float health = 100;

    public float walkPointRange;

    public float chaseTime = 0;

    Vector3 lastPos;

    public float attackTime;
    bool hasAttacked;
    RaycastHit hit;
    float timer = 0;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public AudioSource currentAudio;
    public AudioClip attackSound;
    public AudioClip walkSound;

    public AudioSource walkAudio;

    float delaySound = 2;


    private void Start()
    {
        lastPos = transform.position;
        player = GameObject.Find("XROrigin").transform;
        agent = GetComponent<NavMeshAgent>();
        animate = Model.GetComponent<Animator>();
        isAttacking = animate.GetBool("attack");
        //isMoving = animate.GetBool("IsMoving");
    }

    /* private void Awake()
     {
         player = GameObject.Find("XROrigin").transform;
         agent = GetComponent<NavMeshAgent>();
         animate = Model.GetComponent<Animator>();
         isAttacking = animate.GetBool("IsAttacking");
         isMoving = animate.GetBool("IsMoving");

     }
     */


    private void Update()
    {
        if (!currentAudio.isPlaying && delaySound < 0 && !hasAttacked)
        {
            currentAudio.clip = walkSound;
            //currentAudio.PlayDelayed(currentAudio.clip.length);
            currentAudio.Play();

        }
        delaySound -= Time.deltaTime;
        if (delaySound < (0 - currentAudio.clip.length - 2))
        {
            delaySound = currentAudio.clip.length;
        }


        checkIsMoving();
        if (isMoving)
        {
            if (!walkAudio.isPlaying)
            {
                walkAudio.PlayDelayed(0.5f);
            }

        }



        var rayDirection = player.position - transform.position;
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        FindPlayer();
        //playerInSightRange = Physics.Raycast(player.position, transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);
        //Debug.DrawRay(transform.position, rayDirection , Color.yellow);
        // (Physics.Raycast(walkPoint, -transform.up, 2f, isGround)
        Debug.DrawRay(rayDirection, -transform.up, Color.blue);


        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if ((playerInSightRange && !playerInAttackRange) || (chaseTime > 0 && !playerInAttackRange))
        {


            ChasePlayer();




        }

        if (playerInAttackRange && playerInSightRange)
        {
            //if (Physics.Raycast(transform.position, rayDirection, out hit))
            // {
            //  if (hit.transform == player)
            //  {
            AttackPlayer();
            // }
            // }
        }
        // if(chaseTime < 0)
        //{
        //Patroling();
        // }
        chaseTime -= Time.deltaTime;
    }


    void FindPlayer()
    {

        // Ray ray = playerCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        var rayDirection = headPos.position - playerHitBox.position;
        Ray ray = new Ray(headPos.position, playerHitBox.position - headPos.position);
        RaycastHit hit;

        // Debug.Log(hit.transform.name);
        //Debug.Log(hit.transform.tag);
        Debug.DrawRay(playerHitBox.position, rayDirection, Color.yellow);
        if (Physics.Raycast(ray, out hit, sightRange))
        {
            Debug.Log(hit.transform.name);
            Debug.Log(hit.transform.tag);
            //Debug.DrawRay(ray., Color.yellow);
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("IF IS WORKING");
                playerInSightRange = true;
                return;
            }


        }

        playerInSightRange = false;

    }


    void checkIsMoving()
    {
        var displacement = transform.position - lastPos;
        lastPos = transform.position;
        if (displacement.magnitude > 0.01)
        {
            animate.SetFloat("speed", displacement.magnitude);
            animate.SetFloat("Angularspeed", displacement.magnitude);
            //isMoving = true;
        }
        else
        {
            //isMoving = false;
        }

    }



    private void Patroling()
    {
        timer += Time.deltaTime;
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            //animate.SetBool("IsMoving", true);
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (timer > 5)
        {
            walkPointSet = false;
            timer = 0;
        }
        if (distanceToWalkPoint.magnitude < 1f)
        {
            //animate.SetBool("IsMoving", false);
            walkPointSet = false;
        }

    }

    void SearchWalkPoint()
    {
        float randX = Random.Range(-walkPointRange, walkPointRange);
        float randZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            Debug.DrawRay(walkPoint, -transform.up, Color.yellow);
            walkPointSet = true;
        }


    }

    private void ChasePlayer()
    {

        //animate.SetBool("IsMoving", true);
        agent.SetDestination(player.position);
        if (playerInSightRange)
        {
            chaseTime = 2;
        }
    }


    private void AttackPlayer()
    {
        animate.SetBool("IsMoving", false);
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!hasAttacked)
        {


            currentAudio.clip = attackSound;
            currentAudio.Play();

            animate.SetBool("IsAttacking", true);

            hasAttacked = true;
            Invoke(nameof(ResetAttack), attackTime);
        }
        if (playerInSightRange)
        {
            chaseTime = 2;
        }


    }

    void ResetAttack()
    {
        animate.SetBool("IsAttacking", false);
        hasAttacked = false;
    }

    /*  public void TakeDamage(int damage)
      {
          health -= damage;
          if (health <= 0)
          {
              Invoke(nameof(DestroyEnemy), 0.5f);
          }   

      }

      private void DestroyEnemy()
      {
          Destroy(gameObject);
      }
      */

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


}
