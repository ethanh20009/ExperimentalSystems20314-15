using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class binScript : MonoBehaviour
{
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        recyclableObject recyclingComponent = collision.gameObject.GetComponent<recyclableObject>();
        if (recyclingComponent == null) { return; }
        if (recyclingComponent.GetRecyclingType() == recycleType) //Recycled correctly
        {
            binCorrectExplosion.Play();
        }
        else
        {
            binExplosion.Play();
        }
        Destroy(recyclingComponent.gameObject);

    }
}




