using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjects : MonoBehaviour
{
    public GameObject selectedObject;
    Vector3 offset;

    void Update()
    {
        if (transform.position.z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y);
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject.tag == "item" && selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }
}
