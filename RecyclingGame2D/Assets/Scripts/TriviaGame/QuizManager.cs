using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QA;
    public GameObject[] options;
    public int currentQuestion;
    public int score = 0;
    public CorrectPlayer correctPlayer;
    public IncorrectPlayer incorrectPlayer;
    public bool buttonsEnabled;
   
    public Text QuestionTxt;
    public Text Score;

    private void Start()
    {
        string qaPath = "Trivia/QAData";
        TextAsset qaFile = Resources.Load<TextAsset>(qaPath);

        ParseCSV readCSV = new ParseCSV();
        CSVObject Data = readCSV.Read(qaFile.ToString());
        for (int i = 0; i < Data.data.Count; i++)
        {
            QuestionAndAnswers qa = new QuestionAndAnswers();
            qa.Question = Data.data[i][0];
            qa.Answers[0] = Data.data[i][1];
            qa.Answers[1] = Data.data[i][2];
            qa.Answers[2] = Data.data[i][3];
            qa.Answers[3] = Data.data[i][4];
            qa.CorrectAnswer = Convert.ToInt32(Data.data[i][5]);
            QA.Add(qa);      
        }
        Score.text = score.ToString();
        buttonsEnabled = true;
        generateQuestion();
    }

    public void correct(int optionNum)
    {
        options[optionNum].GetComponent<Image>().color = Color.green;
        score++;
        Score.text = score.ToString();
        correctPlayer.PlayRockCorrect();
        StartCoroutine(BackToWhite(optionNum));
    }

    public void incorrect(int optionNum)
    {
        options[optionNum].GetComponent<Image>().color = Color.red;
        incorrectPlayer.PlayRockIncorrect();
        StartCoroutine(BackToWhite(optionNum));
    }

    IEnumerator BackToWhite(int optionNum)
    {
        yield return new WaitForSeconds(2);
        options[optionNum].GetComponent<Image>().color = Color.white;
        QA.RemoveAt(currentQuestion);
        toggleButtons();
        generateQuestion();
    }

    public void toggleButtons()
    {
        buttonsEnabled = !buttonsEnabled;
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().buttonEnabled = buttonsEnabled;
        }
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].GetComponent<AnswerScript>().optionNumber = i;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QA[currentQuestion].Answers[i];

            if (QA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        currentQuestion = Random.Range(0, QA.Count);

        QuestionTxt.text = QA[currentQuestion].Question;
        SetAnswers();
    }
}