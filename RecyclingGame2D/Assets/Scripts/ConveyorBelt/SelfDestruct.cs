using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfDestruct : MonoBehaviour
{
    public Sprite correctSprite;
    public Sprite incorrectSprite;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("it enter = "+gameObject.name);
        Destroy(gameObject,8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        //void OnMouseDown()
        //Debug.Log(gameObject.name);
        if (gameObject.name == "correct")
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            Debug.Log("Made it");
            gameObject.GetComponent<SpriteRenderer>().sprite = correctSprite;
            Destroy(gameObject, 5);
        }
        else if (gameObject.name == "incorrect")
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("Made it");
            gameObject.GetComponent<SpriteRenderer>().sprite = incorrectSprite;
            Destroy(gameObject, 5);
        }
        
        // if destroyed correctly switch sprite to a tick
        // if incorrectly switch to x
    }
}
