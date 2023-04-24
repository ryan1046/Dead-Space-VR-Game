using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health = 100;

    public GameObject bar1;
    public GameObject bar2;
    public GameObject bar3;
    public GameObject bar4;
    public GameObject bar5;

    public Material fullHealthGlow;
    public Material midHealthGlow;
    public Material lowHealthGlow;
    public Material offGlow;


    public Color fullLight;
    public Color midLight;
    public Color lowLight;


    private void Update()
    {
        if(health > 100)
        {
            health = 100;
        }
        TurnBarGlowOff(bar1);
        TurnBarGlowOff(bar2);
        TurnBarGlowOff(bar3);
        TurnBarGlowOff(bar4);
        TurnBarGlowOff(bar5);
        if (health <= 20)
        {
            TurnBarGlowLow(bar5);
        }
        else if (health <= 40)
        {
            TurnBarGlowLow(bar4);
            TurnBarGlowLow(bar5);
        }
        else if (health <= 60)
        {
            TurnBarGlowMid(bar3);
            TurnBarGlowMid(bar4);
            TurnBarGlowMid(bar5);

        }
        else if (health <= 80)
        {
           
            TurnBarGlowFull(bar2);
            TurnBarGlowFull(bar3);
            TurnBarGlowFull(bar4);
            TurnBarGlowFull(bar5);

        }
        else if(health > 80)
        {
            TurnBarGlowFull(bar1);
            TurnBarGlowFull(bar2);
            TurnBarGlowFull(bar3);
            TurnBarGlowFull(bar4);
            TurnBarGlowFull(bar5);
        }



    }

    void TurnBarGlowOff(GameObject bar)
    {
        bar.GetComponent<Renderer>().material = offGlow;
        bar.GetComponentInChildren<Light>().enabled = false;
    }

    void TurnBarGlowFull(GameObject bar)
    {
        bar.GetComponent<Renderer>().material = fullHealthGlow;
        bar.GetComponentInChildren<Light>().enabled = true;
        bar.GetComponentInChildren<Light>().color = fullLight;
    }
    void TurnBarGlowMid(GameObject bar)
    {
        bar.GetComponent<Renderer>().material = midHealthGlow;
        bar.GetComponentInChildren<Light>().enabled = true;
        bar.GetComponentInChildren<Light>().color = midLight;
    }
    void TurnBarGlowLow(GameObject bar)
    {
        bar.GetComponent<Renderer>().material = lowHealthGlow;
        bar.GetComponentInChildren<Light>().enabled = true;
        bar.GetComponentInChildren<Light>().color = lowLight;
    }






    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene("Dead Scene");
        }

    }

    public float GetHealth()
    {
        return health;
    }



 

}
