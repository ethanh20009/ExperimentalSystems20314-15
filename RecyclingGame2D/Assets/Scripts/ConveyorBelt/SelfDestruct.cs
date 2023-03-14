using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfDestruct : MonoBehaviour
{
    public GameObject correctPrefab;
    public GameObject incorrectPrefab;
    SpawnRandomPoint spawnRandomPoint;

    // Start is called before the first frame update
    void Start()
    {
        //to automatically destroy itself after a while of being in the game
        //Destroy(gameObject,8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // if destroyed correctly switch sprite to a x
        // if incorrectly switch to tick
        // if heart bonus then more life

        // destroying the object
        Destroy(gameObject);
        spawnRandomPoint = GameObject.Find("Spawn_Point").GetComponent<SpawnRandomPoint>();

        if (gameObject.name == "correct")
        {
            // remember this item was on its way to the correct bin, eliminating it is the wrong thing to do
            // which takes away a heart
            GameObject result = Instantiate(incorrectPrefab, transform.position, Quaternion.identity);//remember you switched            
            Destroy(result, 1);
            
            spawnRandomPoint.minusHearts();

        }
        else if (gameObject.name == "incorrect")
        {
            // remember this item was on its way to the incorrect bin, eliminating it is the right thing to do
            // which their score increases for
            GameObject result = Instantiate(correctPrefab, transform.position, Quaternion.identity); // remember you switched         
            Destroy(result, 1);

            
            spawnRandomPoint.plusScore();

        }
        else if (gameObject.name == "heart")
        {
            
            spawnRandomPoint.plusHearts();                    
        }

        




    }

    
}
