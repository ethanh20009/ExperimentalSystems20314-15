using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public GameObject gameRulesPrefab;
    private bool show = false;
    GameObject gameRulesScreen;
    
    
    void OnMouseOver()
    {
        if (show == false)
        {
            gameRulesScreen = Instantiate(gameRulesPrefab, new Vector2(0, 0), Quaternion.identity);            
            show = true;
        }
        
    }

    
    void OnMouseExit()
    {
        if (show == false)
        {
            Destroy(gameRulesScreen);
            show = false;
        }
    }

    void OnMouseDown()
    {
        gameRulesScreen = Instantiate(gameRulesPrefab, new Vector2(0, 0), Quaternion.identity);
        Destroy(gameRulesScreen, 3);
    }
}
