using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Thunderbolt : MonoBehaviour

{
    Rigidbody enemy;
    // Start is called before the first frame update
    EnemyObject enemy1;
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * 60);
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward);
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))   // normalde sadece yok edecektik lakin bir videoda addexplosionforce görünce
                                                      // dedim bunu da deneyelim.
        {
            other.GetComponent<EnemyObject>().anim.Kill(false); // DoTween in fonksiyonu yokettik
            enemy = other.GetComponent<Rigidbody>();
            enemy.AddExplosionForce(1000f,other.transform.position,20f,20f, (float)ForceMode.Force);
            enemy.AddForce(Random.Range(-5, 5)*5, Random.Range(-1, 1)*0, Random.Range(-1, 1)*500); // random saga sola ucussunlar diye
            print("bomba");

            Destroy(other.gameObject,2f);
        }

        //Destroy(other.gameObject);
    }
}
