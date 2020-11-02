using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // oyunu restart etmek icin

public class EndGame : MonoBehaviour
{
    public GameObject car;
    public GameObject gameEndUI;

    private int _update;
    
    // Start is called before the first frame update
    void Start()
    {
        gameEndUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!car.activeSelf)  // araba yok olunca enemy ye carpinca yani
        {
            print("gel1");
            _update++;
            if (_update == 1)
            {
                print("gel2");
                gameEndUI.SetActive(true);
                show();   // oyun sonu ekrani gelecek
            }
            
        }
    }

    void show()
    {
        gameEndUI.SetActive(true);

    }

    public void retry()
    {
        Debug.Log("restarting level...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restart kodu

    }

    public void quitt()
    {
        Debug.Log("quitting game...");
        Application.Quit();
    }
}
