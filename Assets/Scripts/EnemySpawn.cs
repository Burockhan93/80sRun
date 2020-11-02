using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject car;
    public GameObject x_Ref;
    public GameObject Enemy;
   

    private Transform _spawnPoint;
    private int _timer;
    private GameObject _enemy;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 30;
        _spawnPoint = Enemy.transform; // transform almamiz gereken bir yerdi búrdan aldik.

    }

    // Update is called once per frame
    void Update()
    {
        _timer--;
        Vector3 spawnx = new Vector3(x_Ref.transform.position.x+Random.Range(1,8), 0, 0); // Soldaki bariyerre göre
        Vector3 spawny = new Vector3(0, car.transform.position.y, 0); // yerde
        Vector3 spawnz = new Vector3(0f, 0f, car.transform.position.z+80);// arabanin 80 m önune

        Vector3 spawn = new Vector3(spawnx.x, spawny.y, spawnz.z);

        _spawnPoint.position = (spawn);

        if (_timer <= 0)
        {
            spawnEnemy();
            _timer = 150;
        }
    }

    void spawnEnemy()
    {
        _enemy = Instantiate(Enemy, _spawnPoint.position,Enemy.transform.rotation);
        Destroy (_enemy, 8f);
    }
}
