using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChanger : MonoBehaviour
{
    public string collisionObject;
    public GameObject Spawn;
    ObjectSpawner os;

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision");
        if (other.gameObject.name == collisionObject)
        {
            Destroy(other.gameObject);
            GameObject newObject = Instantiate(Spawn, transform.position, Quaternion.identity);
            os.objects_created = 0;
        }
    }
}
