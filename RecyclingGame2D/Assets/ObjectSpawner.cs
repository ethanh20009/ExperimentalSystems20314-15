using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ObjecttoSpawn;
    public int objects_created = 0;
    int object_limit = 1;

    public void CreateObject()
    {
        if (objects_created < object_limit)
        {
            GameObject newObject = Instantiate(ObjecttoSpawn, new Vector3(0, -2, 0), Quaternion.identity);
            objects_created++;
        }
    }
}
