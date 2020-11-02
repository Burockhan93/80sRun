using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UIPages")]

    public GameObject optionScreen;
    public GameObject creditsScreen;
    public GameObject mainScreen;

    // Start is called before the first frame update
    void Start()
    {
        optionScreen.SetActive(false);
        creditsScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //Gamestart
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        mainScreen.SetActive(false);
        optionScreen.SetActive(true);
    }

    public void Credits()
    {
        mainScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void toMain()//Ana menüye dönus
    {
        mainScreen.SetActive(true);
        optionScreen.SetActive(false);
        creditsScreen.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }


}
