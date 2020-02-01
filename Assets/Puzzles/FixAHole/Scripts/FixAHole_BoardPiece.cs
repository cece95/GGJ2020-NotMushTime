using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_BoardPiece : MonoBehaviour
{
    public bool IsFilled { get; private set; }
    public bool IsHighlighted { get; private set; }

    [SerializeField]
    private Sprite emptySprite;

    [SerializeField]
    private Sprite filledSprite;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void SetFilled(bool isFilled)
    {
        IsFilled = isFilled;
        UpdateSprite();
    }

    public void SetHighlight(bool isHighlighted)
    {
        IsHighlighted = isHighlighted;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if(spriteRenderer)
        {
            spriteRenderer.sprite = IsFilled ? filledSprite : emptySprite;
            spriteRenderer.color = IsHighlighted ? Color.yellow : ( IsFilled ? Color.white : Color.gray );
        } else
        {
            Debug.LogWarning("Sprite renderer not existing");
        }
    }
}
