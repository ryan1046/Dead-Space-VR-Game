using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public EnemyAI[] enemies;
    public SpawnLocation[] enemySpawnLocations;


    public GameObject leftHand;
    public GameObject rightHand;
    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;


    public GameObject spawnEnemy;

    bool hasGameStarted = false;


    public AudioSource audioPlayer;

    public AudioClip battleMusic;
    public AudioClip normalMusic;

    public bool isPlayerInCombat;

    public GameObject exit;

    public float musicVolume;


    public Transform playerHitBox;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 1);
        if (PlayerPrefs.GetInt("grabController") == 0)
        {
            ChangeControls(true);
        }
        else
        {
            ChangeControls(false);
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            UpdateVolumeMusic(PlayerPrefs.GetFloat("musicVolume"));
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            UpdateSFXSound(PlayerPrefs.GetFloat("sfxVolume"));
        }


        }

    void StartGame()
    {
        hasGameStarted = true;
    }



    // Update is called once per frame
    void Update()
    {
        enemies = FindObjectsOfType<EnemyAI>();
        enemySpawnLocations = FindObjectsOfType<SpawnLocation>();
        CheckIfInCombatMusic();
        if (hasGameStarted == true)
        {
            SpawnEnemies();
        }
    }

   
  


    void PlayerDied()
    {

        SceneManager.LoadScene("Dead Scene");


    }

   public void PlayerCompletesLevel()
    {
        SceneManager.LoadScene("Win Scene");
    }

    void CheckIfInCombatMusic()
    {
        foreach(EnemyAI enemy in enemies)
        {
            if(enemy.playerInSightRange == true)
            {
                //player in Combat
                CancelInvoke("ChangeToNormalMusic");
                if (audioPlayer.clip != battleMusic)
                {
                    audioPlayer.clip = battleMusic;
                    audioPlayer.Play();
                }
             
              
            }
            else
            {
                //change back
                Invoke("ChangeToNormalMusic", 6);

            }
        }
        if(enemies.Length == 0)
        {
            Invoke("ChangeToNormalMusic", 6);
        }
    }


    public void UpdateVolumeMusic(float wantVolume)
    {
        PlayerPrefs.SetFloat("musicVolume", wantVolume);

        musicVolume = PlayerPrefs.GetFloat("musicVolume");

        audioPlayer.volume = musicVolume;
    }


    public void UpdateSFXSound(float wantVolume)
    {
       PlayerPrefs.SetFloat("sfxVolume", wantVolume);
        AudioSource[] SFXsound = FindObjectsOfType<AudioSource>();
        foreach(AudioSource soundVolume in SFXsound)
        {
            if(soundVolume.gameObject.tag != "Music")
            {
                soundVolume.volume = PlayerPrefs.GetFloat("sfxVolume");
            }
        }

    }

    void ChangeToNormalMusic()
    {
        if (audioPlayer.clip != normalMusic)
        {
            audioPlayer.clip = normalMusic;
            audioPlayer.Play();
        }
        
    }

    public void ChangeControls(bool isHold)
    {
        if(isHold)
        {
            PlayerPrefs.SetInt("grabController", 0);
        }
        else
        {
            PlayerPrefs.SetInt("grabController", 1);
        }

   
        if (PlayerPrefs.GetInt("grabController") == 0)
        {
            leftHand.GetComponent<XRDirectInteractor>().selectActionTrigger = XRBaseControllerInteractor.InputTriggerType.Sticky ;
            rightHand.GetComponent<XRDirectInteractor>().selectActionTrigger = XRBaseControllerInteractor.InputTriggerType.Sticky;
             leftRay.selectActionTrigger   = XRBaseControllerInteractor.InputTriggerType.Sticky;
            rightRay.selectActionTrigger   = XRBaseControllerInteractor.InputTriggerType.Sticky;
        }
        if(PlayerPrefs.GetInt("grabController") == 1)
        {
            leftHand.GetComponent<XRDirectInteractor>().selectActionTrigger = XRBaseControllerInteractor.InputTriggerType.State;
            rightHand.GetComponent<XRDirectInteractor>().selectActionTrigger = XRBaseControllerInteractor.InputTriggerType.State;
            leftRay.selectActionTrigger = XRBaseControllerInteractor.InputTriggerType.State;
            rightRay.selectActionTrigger = XRBaseControllerInteractor.InputTriggerType.State;
        }



    }


    void SpawnEnemies()
    {
        foreach(SpawnLocation availableSpawn in enemySpawnLocations)
        {
            availableSpawn.spawnEnemy(spawnEnemy);
        }


    }







}
