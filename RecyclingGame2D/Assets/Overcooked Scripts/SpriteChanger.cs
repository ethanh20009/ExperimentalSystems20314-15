using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite Sprite1,Sprite2,Sprite3,Sprite4;
    public int index;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (index == 0)
        {
            spriteRenderer.sprite = Sprite1;
        }
        if (index == 1)
        {
            spriteRenderer.sprite = Sprite2;
        }
        if (index == 2)
        {
            spriteRenderer.sprite = Sprite3;
        }
        if (index == 3)
        {
            spriteRenderer.sprite = Sprite4;
        }
    }
}
