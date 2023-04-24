using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSound : MonoBehaviour
{
    public AudioSource audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");

             audioPlayer.volume = musicVolume;
        }
    }
}
