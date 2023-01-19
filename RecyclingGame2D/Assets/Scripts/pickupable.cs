using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupable : MonoBehaviour
{
    private TargetJoint2D joint;
    private bool isHeld;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<TargetJoint2D>();
        joint.enabled = false;
        isHeld = false;
        litterTracker.addPickupable(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isHeld = false;
                joint.enabled = false;
                return;
            }
            joint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void pickup(Vector2 localCoord)
    {
        Debug.Log(gameObject.name + " Has been picked up at");
        Debug.Log(transform.InverseTransformPoint(localCoord));
        joint.anchor = transform.InverseTransformPoint(localCoord);
        isHeld = true;
        joint.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        joint.enabled = true;
    }
}
