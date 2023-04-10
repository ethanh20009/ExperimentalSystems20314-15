using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compostBinScript : MonoBehaviour
{
    [SerializeField]
    public float speed = 1f;
    [SerializeField]
    public float maxSpeed = 2f;
    [SerializeField]
    public ParticleSystem correctEffect;
    [SerializeField]
    public ParticleSystem wrongEffect;
    [SerializeField]
    public CompostGameState manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float pos = transform.position.x;
        float mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float direction = mousePos - pos;
        Vector3 vect = Vector3.right * direction * Time.deltaTime;
        vect *= speed;
        vect = Vector3.ClampMagnitude(vect, maxSpeed);
        transform.position += vect;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CompostItem itemComponent = collision.gameObject.GetComponent<CompostItem>();
        if (itemComponent == null) { return; }
        if (itemComponent.isCompostable)
        {
            //Correct
            correctEffect.Play();
            manager.updateScore(1);
        }
        else { wrongEffect.Play(); }
        
        itemComponent.markForDestruction();
    }


}
