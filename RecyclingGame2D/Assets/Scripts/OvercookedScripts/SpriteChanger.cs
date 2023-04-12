using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite Sprite1,Sprite2,Sprite3,Sprite4;
    [SerializeField] TextMeshProUGUI _objectiveText;
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
            _objectiveText.text = Sprite1.name;
        }
        if (index == 1)
        {
            spriteRenderer.sprite = Sprite2;
            _objectiveText.text = Sprite2.name;
        }
        if (index == 2)
        {
            spriteRenderer.sprite = Sprite3;
            _objectiveText.text = Sprite3.name;
        }
        if (index == 3)
        {
            spriteRenderer.sprite = Sprite4;
            _objectiveText.text = Sprite4.name;
        }
    }
}
