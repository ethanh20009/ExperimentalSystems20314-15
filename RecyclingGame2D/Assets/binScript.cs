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
    [SerializeField]
    ParticleSystem binExplosion;
    [SerializeField]
    ParticleSystem binCorrectExplosion;
    [SerializeField]
    RecyclingTypes recycleType;



    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentItems = litterTracker.getPickupables().ToArray();
        for (int i = 0; i < currentItems.Length; i++) //Check every pickupable's range
        {
            Vector3 dist = currentItems[i].transform.position - recycledPoint.position;
            if (dist.magnitude >= range) { continue; } //Check if in range of centre of bin (dropped in)
            
            //Destroy item (add points etc...)
            GameObject toDelete = currentItems[i].gameObject;
            recyclableObject recyclingComponent = toDelete.GetComponent<recyclableObject>();
            if (recyclingComponent != null) //Is a recyclable object
            {
                if (recyclingComponent.GetRecyclingType() == recycleType) //Recycled correctly
                {
                    binCorrectExplosion.Play();
                }
                else
                {
                    binExplosion.Play();

                }
            }
            litterTracker.removePickupable(currentItems[i]);
            Destroy(toDelete);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color =new Color32(255, 0, 0, 200);
        Gizmos.DrawSphere(recycledPoint.position, this.range);
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




