using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public int optionNumber;
    public bool isCorrect = false;
    public QuizManager quizManager;

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            quizManager.correct(optionNumber);
        }
        else
        {

            Debug.Log("Wrong Answer");
            quizManager.incorrect(optionNumber);
        }
    }
}
