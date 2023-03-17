using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCollision : MonoBehaviour
{
    public GameObject correctShader;
    public GameObject incorrectShader;
    SpawnRandomPoint spawnRandomPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        Destroy(collision.gameObject);

        if (collision.gameObject.name == "correct")
        {
            GameObject shaderResult = Instantiate(correctShader, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(shaderResult, 1);
            

        }
        else if (collision.gameObject.name == "incorrect")
        {
            GameObject shaderResult = Instantiate(incorrectShader, transform.position, Quaternion.identity);            
            Destroy(collision.gameObject);
            Destroy(shaderResult, 1);

            spawnRandomPoint = GameObject.Find("Spawn_Point").GetComponent<SpawnRandomPoint>();
            spawnRandomPoint.minusHearts();

        }
        // nothing happens if heart hits bin

        
    }
}
