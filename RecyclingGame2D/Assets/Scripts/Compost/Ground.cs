using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private CompostGameState gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<CompostGameState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CompostItem item = collision.gameObject.GetComponent<CompostItem>();
        Debug.Log("Ground Collision");
        if (!item) { return; }
        //Check if item was non compostable
        if (!item.isCompostable)
        {
            gm.updateScore(1);
        }
        Destroy(item.gameObject);
    }
}
