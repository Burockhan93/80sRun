using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public Camera[] cameras;
    private int cam;
    //public Camera rear;
    //public Camera side;
   


    // Start is called before the first frame update
    void Start()
    {
        cam = 0;
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].enabled =false;  // ilki haric diger cameralar kapali
        }


        //rear.enabled = true; iki kamera varken bunu kullanmistik
        //side.enabled = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))  iki kamera varken bunu kullanmistik
        //{
        //    rear.enabled = !rear.enabled;
        //    side.enabled = !side.enabled;
        //}

        if (Input.GetKeyDown(KeyCode.C))
        {
            cam++;
            

            if (cam < cameras.Length)
            {   // ilk kameralarda isek
                cameras[cam - 1].enabled = false; // güncel kamera iptal
                Debug.LogError(cam);
                cameras[cam].enabled = true; // yeni camera gecerli
            }
            else // zaten son kamerada isek
            {
                cameras[cam - 1].enabled = false; // güncel kamera iptal
                cam = 0;
                cameras[cam].enabled = true; // basa dön
                Debug.LogError(cam);
            }
            
        }

        if (gameObject==null)
        {
            Debug.LogError("Araba yok");
        }
        
    }
}
