using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Transform _car; // arabaya göre spawn olcaz

    private int _enemyVelocity = 10;
    //public TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 8);
        //text.gameObject.SetActive(true);
        //text.text = "";


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( 1 * Time.deltaTime * _enemyVelocity,0,0);

    }

    //void OnTriggerEnter(Collider other)
    //{
    //    other.gameObject.SetActive(false);
        
    //    text.text = "Game Over";    burasi eskiden elemani yok ediyordu
    //}
}
