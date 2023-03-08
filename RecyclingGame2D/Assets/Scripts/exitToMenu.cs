using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitToMenu : MonoBehaviour
{
    public void goToMenu()
    {
        //loads the menu screen
        SceneManager.LoadScene(1);
    }
}
