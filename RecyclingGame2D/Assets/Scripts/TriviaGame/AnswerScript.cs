using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public int optionNumber;
    public int correctNumber;
    public bool isCorrect = false;
    public bool buttonEnabled = true;
    public QuizManager quizManager;

    public void Answer()
    {
        Debug.Log(buttonEnabled);
        if (buttonEnabled)
        {
            quizManager.toggleButtons();
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
}
