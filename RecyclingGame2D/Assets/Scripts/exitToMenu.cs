using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitToMenu : MonoBehaviour
{
    public void goToMenu()
    {
        SceneManager.LoadScene(1);
    }
}
