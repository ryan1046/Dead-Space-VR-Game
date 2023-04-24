using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    
    public Button startButton;
    public Button quitButton;
    public Button optionButton;
    public Button backButton;
    public GameObject menu;
    public GameObject optionMenu;


   
    public Slider volumeSlider;
    public Slider SFXSlider;
   
    public Toggle controlOption;


    // Start is called before the first frame update
    void Start()
    {

        startButton.onClick.AddListener(startClick);
        quitButton.onClick.AddListener(quitClick);
        optionButton.onClick.AddListener(optionClick);
        backButton.onClick.AddListener(backClick);
        controlOption.onValueChanged.AddListener(UpdateControls);
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
    }

    void startClick()
    {
        Debug.Log("pressed");
        SceneManager.LoadScene("User Testing Scene");
    }


    void quitClick()
    {
        Application.Quit();
    }

    void optionClick()
    {
        menu.SetActive(false);
        optionMenu.SetActive(true);
    }

    void backClick()
    {
        menu.SetActive(true);
        optionMenu.SetActive(false);

    }




    void UpdateControls(bool change)
    {
       
        if (controlOption.isOn)
        {
            PlayerPrefs.SetInt("grabController", 0);
        }
        else
        {
            PlayerPrefs.SetInt("grabController", 1);
        }

    }


    void UpdateVolumeMusic()
    {
  
        PlayerPrefs.SetFloat("sfxVolume", volumeSlider.value);
    }

    void UpdateVolumeSFX()
    {
        
        PlayerPrefs.SetFloat("sfxVolume", SFXSlider.value);
    }


}
