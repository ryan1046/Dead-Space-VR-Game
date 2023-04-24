using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public AudioSource openSound;
    public AudioSource closeSound;
    public GameObject Door;
    public Animator animate;

    public int soundToPlay;


    // Start is called before the first frame update
    void Start()
    {
        animate = Door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animate.GetCurrentAnimatorStateInfo(0).IsName("Doors Opening"))
        {
            if (!openSound.isPlaying && soundToPlay == 0)
            {
                openSound.Play();
                soundToPlay = 1;
               // SoundDelay(openSound);
                
            }

        }
        if (animate.GetCurrentAnimatorStateInfo(0).IsName("Doors Closing"))
        {
            if (!closeSound.isPlaying && soundToPlay == 1)
            {

                closeSound.Play();
                soundToPlay = 0;
                //SoundDelay(closeSound);
            }

        }



    }

    


   

    IEnumerator SoundDelay(AudioSource time)
    {
        time.Play();
        yield return new WaitForSeconds(time.clip.length);

    }
}
