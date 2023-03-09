using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    ObjectSpawner os;

    void Update()
    {
        os = GameObject.Find("GameObject").GetComponent<ObjectSpawner>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision");
        os.objects_created = 0;
        Destroy(other.gameObject);
    }
}
