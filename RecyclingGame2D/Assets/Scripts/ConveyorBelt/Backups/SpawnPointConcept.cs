using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointConcept : MonoBehaviour
{
    public int Drag;

    public SpriteRenderer goodSpriteRenderer;
    public SpriteRenderer badSpriteRenderer;
    public Sprite goodRecycle;
    public Sprite badRecycle;

    //public float rayLength;
    //public LayerMask layermask;

    // Start is called before the first frame update
    void Start()
    {
        GameObject good = new GameObject();
        GameObject bad = new GameObject();

        good.name = "good";
        bad.name = "bad";

        good.AddComponent<SpriteRenderer>();
        bad.AddComponent<SpriteRenderer>();
        
        // making object visible
        // - putting the object in the right location then layer
        // - adding the sprite image
        good.transform.position = new Vector2(0,5);
        bad.transform.position = new Vector2(0,9);

        good.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Items");
        bad.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Items");

        good.GetComponent<SpriteRenderer>().sprite = goodRecycle;
        bad.GetComponent<SpriteRenderer>().sprite = badRecycle;

        //adding gravity and changing drag
        good.AddComponent<Rigidbody2D>();
        bad.AddComponent<Rigidbody2D>();

        good.AddComponent<BoxCollider2D>();
        bad.AddComponent<BoxCollider2D>();

        good.GetComponent<Rigidbody2D>().drag = Drag;
        bad.GetComponent<Rigidbody2D>().drag = Drag;

        //clicked destroy and self- destroy
        good.AddComponent<SelfDestructConcept>(); // you renamed SelfDestruct.cs to SelfDestructConcept.cs so this might give errors
        bad.AddComponent<SelfDestructConcept>();

        Destroy(good, 15);
        Destroy(bad, 15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
