using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChanger : MonoBehaviour
{
    public string collisionObject;
    public GameObject Spawn;

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision");
        if (other.gameObject.name == collisionObject)
        {
            Destroy(other.gameObject);
            ObjectSpawner os = gameObject.GetComponent<ObjectSpawner>();
            os.ObjecttoSpawn = Spawn;
            os.CreateObject();
        }
    }
}
