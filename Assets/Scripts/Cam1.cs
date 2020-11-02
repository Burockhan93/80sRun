using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cam1 : MonoBehaviour
{
    public GameObject _car;
    public GameObject refCam;
    public GameObject endCam;
    private float _transitionSpeed;

    private Vector3 _cam1; //StartCam
    private Vector3 _cam2; // 3rd personCamstart

    private bool isend;


    //Downward tracking..Failed attempt to fix camera at the car
    //float rotationSpeed = 15f;
    //Vector3 currentEulerAngles;
    //Quaternion currentRotation;
    //float x;
    //float y;
    //float z;


    private void Awake()
    {
        _car.GetComponent<CarMove>().onEndScene.AddListener(SetCameraFinalPosition); //_car.onEndScene.AddListener(SetCam..); da olabilir ama scriptten alalim dedik.
    }
    void Start()
    {
        _transitionSpeed = 2;
        // Starting cam
        _cam1.x = _car.transform.position.x;
        _cam1.y = _car.transform.position.y + 1f;
        _cam1.z = _car.transform.position.z - 1f;


        transform.position = new Vector3(_cam1.x, _cam1.y, _cam1.z);

        //Calculating the 3rdPersonCam
        _cam2 = refCam.transform.position - _car.transform.position;

        // Endscreen

    }

    // Update is called once per frame

    void LateUpdate()
    {
        // Aligning the 3rdPersonCam
        if (_car.transform.position.z < 20.5)
        {
            transform.position = Vector3.Lerp(transform.position, refCam.transform.position, Time.deltaTime);
        }
        else if (_car.transform.position.z >= 20.5 && GameObject.Find("Car").GetComponent<CarMove>()._isEnd == false)
        {
            transform.position = Vector3.Lerp(transform.position, _car.transform.position + _cam2, Time.deltaTime * _transitionSpeed*2 );
           
        }

        //if (GameObject.Find("Car").GetComponent<CarMove>()._isEnd == true) // oyun sonu kamerasi
        //{
        //    print("TrackedReference");
        //    transform.position = Vector3.Lerp(transform.position, endCam.transform.position,0.68f );
        //}

        //-------------OLd VErsion-----------------//

        //        // Cam adapting ...Y
        //        if (transform.position.y <= refCam.transform.position.y)
        //        {
        //            transform.Translate(0, 0.003f, 0);
        //        }
        //        // Cam adapting... Z
        //        if (transform.position.z >= refCam.transform.position.z)
        //        {
        //            transform.Translate(0, 0, -0.002f);
        //        }
        //        //Cam adapting ... Rotation

        //        // Adapting camera after a while
        //    }
        //    else if (_car.transform.position.z >= 20.5)
        //        {

        //       transform.position = _car.transform.position - _cam2;

        //      }


        //----------Failed attempt to fix camera at the car-----------

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    x = 1 - x;


        //    //modifying the Vector3, based on input multiplied by speed and time
        //    currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * rotationSpeed;

        //    //moving the value of the Vector3 into Quanternion.eulerAngle format
        //    currentRotation.eulerAngles = currentEulerAngles;

        //    //apply the Quaternion.eulerAngles change to the gameObject
        //    transform.rotation = currentRotation;
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    x = -1 - x;


        //    //modifying the Vector3, based on input multiplied by speed and time
        //    currentEulerAngles += new Vector3(x, y, z) * Time.deltaTime * rotationSpeed;

        //    //moving the value of the Vector3 into Quanternion.eulerAngle format
        //    currentRotation.eulerAngles = currentEulerAngles;

        //    //apply the Quaternion.eulerAngles change to the gameObject
        //    transform.rotation = currentRotation;
        //}

        // Fixing the camera at car

        transform.LookAt(_car.transform);





    }

    private void SetCameraFinalPosition()
    {
        transform.DOMove(endCam.transform.position,3f);
    }
}
