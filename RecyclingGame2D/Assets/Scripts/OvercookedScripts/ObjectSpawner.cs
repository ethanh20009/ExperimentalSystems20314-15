using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ObjectSpawner : MonoBehaviour
{
    //public GameObject ObjecttoSpawn;
    public int objects_created = 0;
    int object_limit = 1;
    [SerializeField] TextMeshProUGUI _selectedText;

    void Update()
    {
    }

    public void CreateObject(GameObject ObjecttoSpawn)
    {
        if (objects_created < object_limit)
        {
            GameObject newObject = Instantiate(ObjecttoSpawn, transform.position, Quaternion.identity);
            _selectedText.text = "Selected object: " + ObjecttoSpawn.name;
            objects_created++;
        }
    }
}
