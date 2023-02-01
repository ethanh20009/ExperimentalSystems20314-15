using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recyclableObject : MonoBehaviour
{
    [SerializeField]
    public RecyclingTypes material;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RecyclingTypes GetRecyclingType()
    {
        return this.material;
    }
}
