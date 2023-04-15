using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePickup : MonoBehaviour
{
    //removed as causing error error
    //private float distance = 10f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log(hit.transform.gameObject.name);
                pickupable pickupableObject = hit.transform.gameObject.GetComponent<pickupable>();
                if (pickupableObject != null)
                {
                    pickupableObject.pickup(hit.point);
                }
            }
            else
            {
                Debug.Log("No hit");
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
        
}
