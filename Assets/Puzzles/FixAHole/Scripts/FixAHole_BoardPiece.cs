using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_BoardPiece : MonoBehaviour
{
    private bool IsFilled = false;

    [SerializeField]
    private Sprite emptySprite;

    [SerializeField]
    private Sprite filledSprite;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer);
    }
    
    public void SetFilled(bool isFilled)
    {
        IsFilled = isFilled;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if(spriteRenderer)
        {
            spriteRenderer.sprite = IsFilled ? filledSprite : emptySprite;
            spriteRenderer.color = IsFilled ? Color.white : Color.gray;
        } else
        {
            Debug.LogWarning("Sprite renderer not existing");
        }
    }
}
