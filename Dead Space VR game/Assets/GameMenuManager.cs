using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;
    public Transform head;
    public float spawnDistance = 1;
    public Slider volumeSlider;
    public Slider SFXSlider;
    public GameManager gameManager;
    public Toggle controlOption;



    public Button exitButton;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        if (PlayerPrefs.GetInt("grabController") == 0)
        {
            controlOption.SetIsOnWithoutNotify(true);
        }
        else
        {
            controlOption.SetIsOnWithoutNotify(false);
        }
        exitButton.onClick.AddListener(exitClick);
        controlOption.onValueChanged.AddListener(UpdateControls);

    }


    void exitClick()
    {
        // Application.Quit();
        SceneManager.LoadScene("Start Scene");
    }


    // Update is called once per frame
    void Update()
    {
        UpdateVolumeMusic();
        UpdateVolumeSFX();
        //UpdateControls();
     if (showButton.action.WasPerformedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

           // menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

        }
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }


    void UpdateControls(bool change)
    {
        gameManager.ChangeControls(controlOption.isOn);
    }


    void UpdateVolumeMusic()
    {
        gameManager.UpdateVolumeMusic(volumeSlider.value);
    }

    void UpdateVolumeSFX()
    {
        gameManager.UpdateSFXSound(SFXSlider.value);
    }




}
