using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCollision : MonoBehaviour
{
    public GameObject correctShader;
    public GameObject incorrectShader;
    SpawnRandomPoint spawnRandomPoint;
   

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        //Destroy(collision.gameObject);

        if (collision.gameObject.name == "correct")
        {
            GameObject shaderResult = Instantiate(correctShader, transform.position, Quaternion.identity);
            Destroy(shaderResult, 1);
        }
        else if (collision.gameObject.name == "incorrect")
        {
            GameObject shaderResult = Instantiate(incorrectShader, transform.position, Quaternion.identity);
            Destroy(shaderResult, 1);

            spawnRandomPoint = GameObject.Find("Spawn_Point").GetComponent<SpawnRandomPoint>();
            spawnRandomPoint.minusHearts();
            spawnRandomPoint.showCorrection(collision.gameObject.GetComponent<SelfDestruct>().spriteLocationEnding);
        }

        Destroy(collision.gameObject);
        // heart bonus just gets destroyed


    }
}
