using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class StartTimer : MonoBehaviour
{
    public Text countdownTextField;

    void Start()
    {
        StartCoroutine(StartTimerCoroutine());
    }

    IEnumerator StartTimerCoroutine()
    {
        countdownTextField.text = "3";
        yield return new WaitForSeconds(1.0f);
        countdownTextField.text = "2";
        yield return new WaitForSeconds(1.0f);
        countdownTextField.text = "1";
        yield return new WaitForSeconds(1.0f);
        countdownTextField.text = "Go!";
        // start the game here
        yield return new WaitForSeconds(1.0f);
        countdownTextField.text = "";
        countdownTextField.enabled = false;
        yield return null;
    }

}
