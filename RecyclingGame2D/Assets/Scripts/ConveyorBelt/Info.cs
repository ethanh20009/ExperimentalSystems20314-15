using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public GameObject gameRulesPrefab;
    GameObject gameRulesScreen = new GameObject();
    
    void OnMouseOver()
    {
        GameObject gameRulesScreen = Instantiate(gameRulesPrefab, new Vector2(0, 0), Quaternion.identity);
    }

    void OnMouseExit()
    {
        Destroy(gameRulesScreen);
    }
}
