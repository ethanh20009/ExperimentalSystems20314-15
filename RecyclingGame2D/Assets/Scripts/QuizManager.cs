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

    public Text QuestionTxt;

    private void Start()
    {
        string qaPath = Directory.GetCurrentDirectory() + @"\Assets\Scripts\QAData.csv";
        ReadCSV readCSV = new ReadCSV();
        CSVObject Data = readCSV.Read(qaPath);
        for (int i = 0; i < Data.data.Count; i++)
        {
            QuestionAndAnswers qa = new QuestionAndAnswers();
            qa.Question = Data.data[i][0];
            Debug.Log(qa.Question);
            qa.Answers[0] = Data.data[i][1];
            qa.Answers[1] = Data.data[i][2];
            qa.Answers[2] = Data.data[i][3];
            qa.Answers[3] = Data.data[i][4];
            qa.CorrectAnswer = Convert.ToInt32(Data.data[i][5]);
            Debug.Log(qa.Answers[3]);
            QA.Add(qa);      
        }
        Debug.Log(QA[0].Question);
        generateQuestion();
    }

    public void correct(int optionNum)
    {
        options[optionNum].GetComponent<Image>().color = Color.green;
        StartCoroutine(BackToWhite(optionNum));
    }

    public void incorrect(int optionNum)
    {
        options[optionNum].GetComponent<Image>().color = Color.red;
        StartCoroutine(BackToWhite(optionNum));
    }

    IEnumerator BackToWhite(int optionNum)
    {
        yield return new WaitForSeconds(1);
        options[optionNum].GetComponent<Image>().color = Color.white;
        QA.RemoveAt(currentQuestion);
        generateQuestion();
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
