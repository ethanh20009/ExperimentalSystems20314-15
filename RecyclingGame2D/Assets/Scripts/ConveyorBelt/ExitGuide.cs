using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGuide : MonoBehaviour
{    

    void OnMouseDown()
    {
        GameObject.Find("Recycling_Guide_Buttons(Clone)").SetActive(false);
        disableGuide();
    }

    void disableGuide()
    {
        GameObject[] PopupScreens = GameObject.FindGameObjectsWithTag("PopupScreen");
        foreach (GameObject screen in PopupScreens)
        {
            if(screen != null)
            {
                screen.SetActive(false);
            }
        }
    }
}
