using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float targetTime = 60.0f;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] TextMeshProUGUI _GameOverText;
    public GameObject Submit;
    Objective ob;

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

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);
    }

    void timerEnded()
    {
        ob = GameObject.Find("Submit").GetComponent<Objective>();
        _GameOverText.gameObject.SetActive(true);
        Submit.gameObject.SetActive(false);
        string oldHS = BFSaveSystem.LoadClass<string>("HS2");
        if(int.TryParse(oldHS, out int result))
        {
            if (result < ob.Score)
            {
                BFSaveSystem.SaveClass<string>(ob.Score.ToString(), "HS2");
            }
        }
        else
        {
            //In this case the highscore is invalid anyway and so should be replaced
            BFSaveSystem.SaveClass<string>(ob.Score.ToString(), "HS2");
        }
        StartCoroutine(wait());
    }


}
