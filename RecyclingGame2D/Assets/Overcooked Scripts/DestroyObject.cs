using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    ObjectSpawner os;

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision");
        Destroy(other.gameObject);
        os.objects_created = 0;
    }
}
