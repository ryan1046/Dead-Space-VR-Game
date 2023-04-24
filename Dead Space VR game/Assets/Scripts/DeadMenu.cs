using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    public Button restartButton;
    public Button mainMenuButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {

        restartButton.onClick.AddListener(restartClick);
        mainMenuButton.onClick.AddListener(mainMenuClick);
        quitButton.onClick.AddListener(quitClick);
    }

    void restartClick()
    {
        Debug.Log("pressed");
        SceneManager.LoadScene("User Testing Scene");
    }
    void mainMenuClick()
    {
        SceneManager.LoadScene("Start Scene");
    }
    void quitClick()
    {
        Application.Quit();
    }




}
