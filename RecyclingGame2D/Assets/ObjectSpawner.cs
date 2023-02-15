using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ObjecttoSpawn;

    public void CreateObject()
    {
        GameObject newObject = Instantiate(ObjecttoSpawn);
    }
}
