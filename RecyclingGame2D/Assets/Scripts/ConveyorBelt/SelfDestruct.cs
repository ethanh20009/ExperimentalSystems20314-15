using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfDestruct : MonoBehaviour
{
    public GameObject correctPrefab;
    public GameObject incorrectPrefab;
    public string[] spriteLocationEnding;
    SpawnRandomPoint spawnRandomPoint;

    void OnMouseDown()
    {
        // if destroyed correctly switch sprite to a x
        // if incorrectly switch to tick
        // if heart bonus then more life
       
        spawnRandomPoint = GameObject.Find("Spawn_Point").GetComponent<SpawnRandomPoint>();

        if (gameObject.name == "correct")
        {
            // remember this item was on its way to the correct bin, eliminating it is the wrong thing to do
            // which takes away a heart
            GameObject result = Instantiate(incorrectPrefab, transform.position, Quaternion.identity);//remember you switched            
            Destroy(result, 1);            
            spawnRandomPoint.minusHearts();
            spawnRandomPoint.showCorrection(spriteLocationEnding);

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

        // destroying the object
        Destroy(gameObject);

    }

    
}
