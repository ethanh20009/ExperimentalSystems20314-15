using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ObjecttoSpawn;
    public GameObject Target;
    public int objects_created = 0;
    int object_limit = 1;

    void Update()
    {
    }

    public void CreateObject()
    {
        if (objects_created < object_limit)
        {
            GameObject newObject = Instantiate(ObjecttoSpawn, transform.position, Quaternion.identity);
            newObject.transform.position = Target.transform.position;
            objects_created++;
        }
    }
}
