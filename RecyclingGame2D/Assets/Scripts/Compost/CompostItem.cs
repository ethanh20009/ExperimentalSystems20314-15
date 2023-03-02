using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostItem : MonoBehaviour
{

    public bool isCompostable;
    
    // Start is called before the first frame update
    void Start()
    {
        isCompostable = Random.Range(0, 2) == 1; //50% chance each way, can change
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
