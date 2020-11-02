using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class CarMove : MonoBehaviour
{
    [SerializeField] float _gas;
    [SerializeField] float _turn;
    public TextMeshProUGUI text;
    public TextMeshProUGUI text1;
    public GameObject thunder;
    public GameObject end;
    public GameObject end1;
    public GameObject secondEnemyWave;
    public bool _isEnd;

    public UnityEvent onEndScene = new UnityEvent();
    public UnityEvent onPass = new UnityEvent();
    public UnityEvent onSlope = new UnityEvent();
    public UnityEvent enemySpawn = new UnityEvent();

    RectTransform text1pos;
    RectTransform textpos;

    Camera main;
    
    private Rigidbody _rb;
    private int _points;
    private float _thunderLeft =30;
    private Boolean _section; // Reset position yazisi icin gerekli olan düzeltme

    private TrailRenderer _neon;
    private TrailRenderer _neon1;

    private GameObject _thunder;
    






    // Start is called before the first frame update
    void Start()
    {
        //_neon = GetComponentInChildren<TrailRenderer>();  once bunla yapmistik ama iki neon ekleyince isler karisti.

        //foreach (TrailRenderer neon1 in _neon)  bu iki satir ne ben de bilmiom sormam gerek

        //    neon1.useSpring = false;

        //This gets the Main Camera from the Scene
        //ölünce bu kamera aktif olacak main cam bu
        main = Camera.main;
        _section = true;

        text1pos = text1.GetComponent<RectTransform>(); // text1 in positionu böyle aliniyor.
        textpos = text.GetComponent<RectTransform>();

        _neon = this.transform.Find("CarNeon-left").GetComponent<TrailRenderer>();
        _neon1 = this.transform.Find("CarNeon-right").GetComponent<TrailRenderer>();

        _neon.enabled = false;
        _neon1.enabled = false;

        // child object icinden trail rendereri aldik.
        // oyunun basinda iptal ediyoruz.



        text.gameObject.SetActive(true);
        // Texti aciyoruz.
        text1.gameObject.SetActive(true);


        _isEnd = false;  // End screende herseyi kitleyecez
        
        


        _rb = GetComponent<Rigidbody>();
       _turn = 50f;
       _gas = 2000f;
        text.text = "G O ! ! ! ";
        if (text.enabled==true)
        {
            StartCoroutine(textChanger());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        _thunderLeft += Time.deltaTime*1000;
       // print(_thunderLeft);

        // goes forward always but not in the air
        if (transform.position.y < 0 && _isEnd == false)
        {
            transform.Translate(0, 0, 0.5f*Time.deltaTime);
        }
        

        //forward - backward
        if (Input.GetKey(KeyCode.UpArrow) && _isEnd == false) // araba havadayken ileri giderse ucuo onu engelledik.
        {
            transform.Translate(0, 0, 0.01f*_gas*Time.deltaTime);
            _rb.AddForce(Vector3.forward*3);
            print("ileri");
        }
        else if (Input.GetKey(KeyCode.DownArrow) && _isEnd == false)
        {
            transform.Translate(0, 0, -2f * Time.deltaTime);
        }

        // right - left
        if (Input.GetKey(KeyCode.RightArrow) && _isEnd == false)
        {
            transform.Translate(0.1f*_turn * Time.deltaTime, 0, 0);
            

        }else if (Input.GetKey(KeyCode.LeftArrow) && _isEnd == false)
        {
            transform.Translate(-0.1f*_turn * Time.deltaTime, 0, 0);
        }

        if (gameObject==null) // kontrol amacli
        {
            print("hey");
        }

        //rotate

        if (Input.GetKey(KeyCode.A) && _isEnd == false)
        {
            transform.Rotate(0,- _turn * Time.deltaTime, 0);

        }
        else if (Input.GetKey(KeyCode.D) && _isEnd == false)
        {
            transform.Rotate(0, _turn * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.R) && _isEnd == false) // reset position
        {
            print("R");

            transform.rotation = Quaternion.Euler(Vector3.zero);
            text.text = "";
        }

            //if (gameObject.activeSelf==false)
            //{
            //    print("Hey");
            //    text.enabled = true;
            //    text.text = "Game over";
            //}

            print(gameObject.activeSelf); //Kontrol maksatli

        if ((transform.rotation.eulerAngles.y >= 50) && (transform.rotation.eulerAngles.y <= 310) && _isEnd == false && _section==true ) // arabanin yönü kayiksa
        {
            text.text = "Reset your position with R";
        }
        if (Input.GetKeyDown(KeyCode.Space) && _thunderLeft >= 30 && _isEnd == false)
        {
            Vector3 thunderSpawn = transform.position;
            thunderSpawn.y = thunderSpawn.y+3;

            _thunder = Instantiate(thunder, thunderSpawn,transform.rotation);
            _thunderLeft-=30;
        }

        if (Input.GetKey(KeyCode.T)) 
        {
            transform.position = end1.transform.position; // burasi oyunun sonhalinde cikacak
        }

    }

    //IEnumerator Blink()
    //{
    //    text.text = "Reset your position with R";
    //    yield return new WaitForSeconds(1);
    //    text.text = "";
        
    //}



    IEnumerator textChanger()
    {
        yield return new WaitForSeconds(1);
        text.text = "";

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            text.text = "Game Over";
           // text1pos.anchoredPosition = new Vector3(81.69f, 50f, 0f); // oyun sonu text1i bu konuma aliyoruz.
            text1pos.anchoredPosition = Vector3.Lerp(text1pos.anchoredPosition, new Vector3(45f, 80f, 0f), 1); // yumusak hareket olmadi lan enteresan. bunu bir arastircaz
            text1.fontSize = 50f;
            gameObject.SetActive(false);
            
            //Ölünce cara bagli kameralar gittigi icin ana kameraya ulasip aktive ettik.
            main.enabled = true;

        }

        if (other.gameObject.CompareTag("EnemyPoint")) // add points bu araba prefabinin icine koyydugumu bos obje collideri
        {
            onPass.Invoke();
            Destroy(other.gameObject, 2f);
        }

        if (other.gameObject.CompareTag("Nitro"))
        {
            _neon.enabled = true;
            _neon1.enabled = true;

            _gas = 5000;
            print("5000");
            StartCoroutine(Wait(2));
        }
        if (other.gameObject.CompareTag("Jump"))
        {
            print("Jump");
            _rb.AddForce(transform.up * 800);
            _rb.AddForce(transform.forward * 100f);
        }
        if (Vector3.Distance(secondEnemyWave.transform.position, transform.position) < 5)
        {
            enemySpawn.Invoke();
        }
        if (other.gameObject.CompareTag("section"))
        {
            _section = false;
        }
        if (other.gameObject.CompareTag("end"))
        {
            _isEnd = true;
            onEndScene.Invoke();
            transform.position = end.transform.position; // DoTween ile yapalm
            transform.rotation = end.transform.rotation;
            textpos.anchoredPosition = new Vector3(45f, 80f, 0f);
            text.text = "Congrulations!!!";
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("slope"))
        {
            _rb.AddForce(Vector3.forward * 40); // slopelarda araci hizlandirioz

            
            onSlope.Invoke();
            Debug.Log("Slope");
        }
        if (other.gameObject.CompareTag("slower"))
        {
            print("OO");
            _rb.AddForce(0, -2, -180); // yavaslatmaya  caliscaz
        }
    }

    IEnumerator Wait(int i) //genel bir bekleme coroutine i
    {
        yield return new WaitForSeconds(i);
        _gas = 2000;
        print("2000");
        _neon.enabled = false;
        _neon1.enabled = false;


    }
}
