using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recyclableObject : MonoBehaviour
{
    [SerializeField]
    public RecyclingTypes material;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        if (this.spriteRenderer != null) { setupSpriteImage(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public RecyclingTypes GetRecyclingType()
    {
        return this.material;
    }

    private void setupSpriteImage()
    {
        string spritePath = $"RecyclingImages/{material.ToString()}";
        Sprite[] sprites = Resources.LoadAll<Sprite>(spritePath);
        Debug.Log(sprites.Length);
        if (sprites.Length == 0) { return; } //No custom sprites exist for object
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
