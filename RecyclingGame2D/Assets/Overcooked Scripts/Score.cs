using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    private int score = 0;
    [SerializeField] TextMeshProUGUI _scoreText;
    Objective ob;


    void Start()
    {
        _scoreText.text = "Score: " + score;
    }

    void Update()
    {
        ob = GameObject.Find("Submit").GetComponent<Objective>();
        if (score != ob.Score)
        {
            score = ob.Score;
            _scoreText.text = "Score: " + score;
        }

    }
}
