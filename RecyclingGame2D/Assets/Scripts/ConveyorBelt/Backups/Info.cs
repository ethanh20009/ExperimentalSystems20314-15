using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public GameObject Prefab;
    GameObject Screen;
    //bool secondExit = false;
    
    
    public void Start()
    {
        Screen = Instantiate(Prefab, new Vector2(0, 0), Quaternion.identity);
        Screen.SetActive(false);
    }
    void OnMouseOver()
    {
        Screen.SetActive(true);
    }
    
    public void OnMouseExit()
    {
        if (Screen.name != "Recycling_Guide_Buttons(Clone)")
        {
            Screen.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        Screen.SetActive(true);
        StartCoroutine(wait());
        Screen.SetActive(false);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
    }
}
