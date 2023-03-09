using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    public float targetTime = 60.0f;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] TextMeshProUGUI _GameOverText;
    public GameObject Submit;

    void Start()
    {
        _timerText.text = "Time: " + targetTime;
    }

    void Update()
    {

        if (targetTime <= 0.0f)
        {
            _timerText.text = "Time: " + 0.0;
            timerEnded();
        }
        else
        {
            targetTime -= Time.deltaTime;
            _timerText.text = "Time: " + targetTime;
        }

    }

    void timerEnded()
    {
        _GameOverText.gameObject.SetActive(true);
        Submit.gameObject.SetActive(false);
    }


}
