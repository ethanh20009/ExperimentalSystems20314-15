using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private CompostGameState gm;
    [SerializeField]
    private GameObject successSprite;
    [SerializeField]
    private GameObject wrongSprite;
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
            GameObject go = Instantiate(successSprite, collision.transform.position, Quaternion.identity);
            Destroy(go, 2f);
        }
        else
        {
            GameObject go = Instantiate(wrongSprite, collision.transform.position, Quaternion.identity);
            Destroy(go, 2f);
        }
        item.markForDestruction();
    }
}
