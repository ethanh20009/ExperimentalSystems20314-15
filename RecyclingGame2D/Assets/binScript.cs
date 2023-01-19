using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class binScript : MonoBehaviour
{
    [SerializeField]
    public Transform recycledPoint;
    [SerializeField]
    public float range = 0.2f;
    private pickupable[] currentItems;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentItems = litterTracker.getPickupables().ToArray();
        for (int i = 0; i < currentItems.Length; i++)
        {
            Vector3 dist = currentItems[i].transform.position - recycledPoint.position;
            if (dist.magnitude < range)
            {
                //Destroy item (add points etc...)
                GameObject toDelete = currentItems[i].gameObject;
                litterTracker.removePickupable(currentItems[i]);
                Destroy(toDelete);
            }
        }
    }
}

public static class litterTracker
{
    private static List<pickupable> items = new List<pickupable>();

    public static void addPickupable(pickupable item)
    {
        items.Add(item);
    }

    public static void removePickupable(pickupable item)
    {
        items.Remove(item);
    }

    public static List<pickupable> getPickupables()
    {
        return items;
    }

}
