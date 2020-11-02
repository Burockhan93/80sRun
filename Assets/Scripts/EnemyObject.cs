using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyObject : MonoBehaviour
{
    private Vector3[] _path;
    private float _speed;
    public Tween anim;

    private void Start()
    {
        Debug.Log(transform.position);
        Destroy(gameObject, 10);
    }
    public void StartMoveent()
    {
        anim = transform.DOPath(_path, 10f);

    }

    public EnemyObject SetPath(Vector3[] path)  // Baska scriptte cagirdigimizda bu nesneyi tekrar döndürsün
    {                                           //enemy.GetComponent<EnemyObject>().SetPath(_wayPoint1).StartMoveent();
                                                // EnemySpawnda böyle kod yazdik return this demezsek devaminda StartMovement yazamayiz. Tekrar cagirmamiz gerekir bu instance i.
        _path = path;
        return this;
    }

    public EnemyObject SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }
}
