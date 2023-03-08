using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private Text _scoreText;
    Objective ob;


    void Start()
    {
        _scoreText = "Score: " + score;
    }

    void Update()
    {
        ob = GameObject.Find("Submit").GetComponent<Objective>();
        if (score != ob.Score)
        {
            score = ob.Score;
            _scoreText = "Score: " + score;
        }

    }
}
