using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class HealthKit : MonoBehaviour
{

    public float healthAmount = 50;
    public Collider healthCollider;
    public Health playerHealth;
    public TextMeshProUGUI healthCount1;
    public TextMeshProUGUI healthCount2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       /* Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().gameObject.GetComponentInChildren<Health>().TakeDamage(-50); 
        }
        */
    }


   public void HealPlayer()
    {
        if (healthAmount > 0 && playerHealth.health != 100)
        {
            if (healthAmount < 10)
            {
                playerHealth.TakeDamage(-healthAmount);
                healthAmount = 0;
            }
            else
            {
                playerHealth.TakeDamage(-10);
                healthAmount -= 10;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthAmount();
    }

    void UpdateHealthAmount()
    {
        
         
        healthCount1.text = healthAmount.ToString();
        healthCount2.text = healthAmount.ToString();


    }
}
