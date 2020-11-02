using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUi;
    

    private void Start()
    {
        pauseMenuUi.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (GameObject.Find("Car").GetComponent<CarMove>()._isEnd) // oyun sonu otomatik aciliyor.
        {
            Invoke ("Pause",5f);
        }
       
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true); //canvastaki menu aktif oluyor
        Time.timeScale = 0; // time sinifindan metod kullanan herkes etkileniyor ve zaman bu emtodlari kullananlar icin ilerlemiyor.
        isPaused = true; // pause durumu aktif
}

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        Debug.Log("loading menu...");
        SceneManager.LoadScene("Menu"); // Menu ye donecez. o scenenin adini yazdik string olarak
    }

    public void Restart()
    {
        Debug.Log("restarting...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

   

    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit(); 
    }
}
