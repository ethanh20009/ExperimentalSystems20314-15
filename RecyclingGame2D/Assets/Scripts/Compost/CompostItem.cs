using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostItem : MonoBehaviour
{

    public bool isCompostable;
    private CompostGameState gm;
    private string itemName;
    
    // Start is called before the first frame update
    void Awake()
    {
        isCompostable = Random.Range(0, 2) == 1; //50% chance each way, can change
        gm = FindObjectOfType<CompostGameState>(); //Get game manager;
        if (isCompostable)
        {
            GetComponent<SpriteRenderer>().sprite = gm.CompostableItems[Random.Range(0, gm.CompostableItems.Count)];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = gm.NonCompostableItems[Random.Range(0, gm.NonCompostableItems.Count)];
        }
        itemName = GetComponent<SpriteRenderer>().sprite.name;
    }

    private void Start()
    {
        gm.trackCompostItem(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getItemName()
    {
        return itemName;
    }

    public void markForDestruction()
    {
        gm.untrackCompostableItem(gameObject);
        destroyGameObject();
    }

    private void destroyGameObject()
    {
        if (gm != null)
        {
            gm.spawnNewItem();
        }
        Destroy(gameObject);
    }




}
