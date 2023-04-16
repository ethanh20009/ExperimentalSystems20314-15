using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private Transform cardContainer;

    private void Awake()
    {
        cardContainer = transform.Find("CardContainer");
    }

    public void setHighscore(int score)
    {
        //Get highscore container in children
        cardContainer.Find("HighscoreContainer").GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    public void setScore(int score)
    {
        //Get score container in children
        cardContainer.Find("ScoreContainer").GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    public void hideScore()
    {
        cardContainer.Find("ScoreContainer").gameObject.SetActive(false);
        cardContainer.Find("ScoreLabel").gameObject.SetActive(false);

    }

    public void showScore()
    {
        cardContainer.Find("ScoreContainer").gameObject.SetActive(true);
        cardContainer.Find("ScoreLabel").gameObject.SetActive(true);
    }
}
