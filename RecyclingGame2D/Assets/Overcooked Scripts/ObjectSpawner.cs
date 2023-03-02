using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    //public GameObject ObjecttoSpawn;
    public int objects_created = 0;
    int object_limit = 1;

    void Update()
    {
    }

    public void CreateObject(GameObject ObjecttoSpawn)
    {
        if (objects_created < object_limit)
        {
            GameObject newObject = Instantiate(ObjecttoSpawn, transform.position, Quaternion.identity);
            objects_created++;
        }
    }
}
