using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int Score;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        StartCoroutine(SpawnTarget());
        Score = 0;
        scoreText.text = "Score: " + Score;
    }
}
