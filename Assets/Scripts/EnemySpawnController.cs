using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private List<Transform> _path1;
    [SerializeField] private List<Transform> _path2;
    [SerializeField] private List<Transform> _path3;
    [SerializeField] private List<Transform> _path4;

    [SerializeField] private int _spawnSpeed; // spawn per second
    [SerializeField] private GameObject _enemyPrefab;

    public IEnumerator coroutine;
    public IEnumerator coroutine1;
    public GameObject player;
    public CarMove _car;

    private int sayac;

    private List<GameObject> _enemyPool;

    private Vector3[] _wayPoint1;
    private Vector3[] _wayPoint2;
    private Vector3[] _wayPoint3;
    private Vector3[] _wayPoint4;

    private void Awake()
    {
        _enemyPool = new List<GameObject>(); // enemyleri de bir liste halinde tutyoruz. Bu genel yapilan birsey sanirm

        SetWaypoints();
        coroutine = StartCreateEnemy(_spawnSpeed);
        coroutine1 = StartCreateEnemy1(_spawnSpeed);
        _car.enemySpawn.AddListener(EnemySpawner); // Carmove un icindeki ikinci dusman dalgasini hayata gecirecek 
        sayac = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            player = other.gameObject;
            
            StartCoroutine(coroutine);
        }
        
    }
    private void Update()
    {
        if (player != null && Vector3.Distance(_path1[0].position, player.transform.position) < 5)
        {
            StopCoroutine(coroutine);
        }
        if (player != null && Vector3.Distance(_path4[_path4.Count - 1].position, player.transform.position) < 5 && sayac ==0)
        {
            StartCoroutine(coroutine1);
            sayac += 1;
        }
        if (player !=null && Vector3.Distance(_path4[0].position,player.transform.position) < 15)
        {
            StopCoroutine(coroutine1);
        }

    }


    private void SetWaypoints()
    {
        _wayPoint1 = new Vector3[_path1.Count-1];   // arrayleri tanimliyoruz.
        _wayPoint2 = new Vector3[_path2.Count-1];
        _wayPoint3 = new Vector3[_path3.Count-1];
        _wayPoint4 = new Vector3[_path4.Count-1];


        for (int i = 1; i < _path1.Count; i++)
        {
           _wayPoint1[i - 1] = _path1[i].position;
           _wayPoint2[i - 1] = _path2[i].position;
           _wayPoint3[i - 1] = _path3[i].position;
           _wayPoint4[i - 1] = _path4[i].position;
        }
    }

    private IEnumerator StartCreateEnemy(int speed)
    {
        while (true)
        {
            int spawnPath = UnityEngine.Random.Range(1, 4);
            Debug.Log(spawnPath);
            GameObject enemy = new GameObject();
            
           
            switch (spawnPath)
            {
                case 1:
                    enemy = Instantiate(_enemyPrefab, _path1[0].position,Quaternion.Euler(_enemyPrefab.transform.rotation.eulerAngles.x,
                        _enemyPrefab.transform.rotation.eulerAngles.y, _enemyPrefab.transform.rotation.eulerAngles.z));
                    
                    enemy.GetComponent<EnemyObject>().SetPath(_wayPoint1).StartMoveent();
                    _enemyPool.Add(enemy);
                    //enemy.GetComponent<EnemyObject>().StartMoveent(); // return this yazmasak bunu kullanacktik iste
                    break;
                case 2:
                    enemy = Instantiate(_enemyPrefab, _path2[0].position, _path2[0].transform.rotation);
                    enemy.GetComponent<EnemyObject>().SetPath(_wayPoint2);
                    _enemyPool.Add(enemy);
                    enemy.GetComponent<EnemyObject>().StartMoveent();
                    break;
                case 3:
                    enemy = Instantiate(_enemyPrefab, _path3[0].position, _path3[0].transform.rotation);
                    enemy.GetComponent<EnemyObject>().SetPath(_wayPoint3);
                    _enemyPool.Add(enemy);
                    enemy.GetComponent<EnemyObject>().StartMoveent();
                    break;
            }

            yield return new WaitForSeconds( speed);
        }
    }
    public IEnumerator StartCreateEnemy1(int speed)
    {
        while (true)
        {
            GameObject enemy = new GameObject();
            enemy = Instantiate(_enemyPrefab, _path4[0].position, Quaternion.Euler(_enemyPrefab.transform.rotation.eulerAngles.x,
                        _enemyPrefab.transform.rotation.eulerAngles.y, _enemyPrefab.transform.rotation.eulerAngles.z));

            enemy.GetComponent<EnemyObject>().SetPath(_wayPoint4).StartMoveent();
            _enemyPool.Add(enemy);

            yield return new WaitForSeconds(speed);
        }
    }

    public void EnemySpawner()
    {
        StartCoroutine(coroutine1);
    }
}
