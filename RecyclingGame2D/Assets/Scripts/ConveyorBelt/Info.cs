using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    public GameObject gameRulesPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseHover()
    {
        GameObject gameRulesScreen = Instantiate(gameRulesPrefab, new Vector2(0, 0), Quaternion.identity);
    }
}
