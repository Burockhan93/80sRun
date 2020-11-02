using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;

public class PointCalculator : MonoBehaviour
{
    [SerializeField] private CarMove _car;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _points;
    // Start is called before the first frame update

    void Awake()
    {
        _car.onPass.AddListener(SetPoint);
        _car.onSlope.AddListener(EarnSlopePoint);
        _scoreText.text = "Score: " + _points;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPoint()
    {
        _points += 5;
        _scoreText.text = "Score: " + _points.ToString();

    }
    private void EarnSlopePoint()
    {
        _points++;                                    // add points
        _scoreText.text = "Score: " + _points.ToString();
    }
}
