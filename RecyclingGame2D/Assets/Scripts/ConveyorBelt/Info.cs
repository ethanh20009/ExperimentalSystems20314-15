using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public GameObject gameRulesPrefab;
    GameObject gameRulesScreen;
    
    public void Start()
    {
        gameRulesScreen = Instantiate(gameRulesPrefab, new Vector2(0, 0), Quaternion.identity);
        gameRulesScreen.SetActive(false);
    }
    void OnMouseOver()
    {
        gameRulesScreen.SetActive(true);
    }
    
    public void OnMouseExit()
    {
        gameRulesScreen.SetActive(false);
    }

    public void OnMouseDown()
    {
        gameRulesScreen.SetActive(true);
        StartCoroutine(wait());
        gameRulesScreen.SetActive(false);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
    }
}
