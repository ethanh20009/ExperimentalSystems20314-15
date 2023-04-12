using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectChanger : MonoBehaviour
{
    public string collisionObject;
    public GameObject Spawn;
    ObjectSpawner os;
    [SerializeField] TextMeshProUGUI _selectedText;

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
            _selectedText.text = "Selected object: " + Spawn.name;
            os.objects_created = 0;
        }
    }
}
