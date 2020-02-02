using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_BoardPiece : MonoBehaviour
{
    public bool IsFilled { get; private set; }
    public bool IsHighlighted { get; private set; }
    public bool IsBlocked { get; private set; }

    private SpriteRenderer spriteRenderer;

    private byte neighbours;

    [SerializeField]
    private Sprite[] sprites;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetNeighbours(byte newNeighbours)
    {
        neighbours = newNeighbours;
        UpdateSprite();
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

    public void SetBlocked(bool isBlocked)
    {
        IsBlocked = isBlocked;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if(spriteRenderer)
        {
            Sprite newSprite = null;

            switch(neighbours)
            {
                case 0: // None
                    newSprite = sprites[0];
                    break;

                case 1: // N
                    newSprite = sprites[1];
                    transform.localRotation = Quaternion.Euler(0, 0, 270);
                    break;

                case 2: // E
                    newSprite = sprites[1];
                    transform.localRotation = Quaternion.Euler(0, 0, 180);
                    break;

                case 4: // S
                    newSprite = sprites[1];
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;

                case 8: // W
                    newSprite = sprites[1];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 3: // NE
                    newSprite = sprites[2];
                    transform.localRotation = Quaternion.Euler(0, 0, 270);
                    break;

                case 6: // SE
                    newSprite = sprites[2];
                    transform.localRotation = Quaternion.Euler(0, 0, 180);
                    break;

                case 12: // SW
                    newSprite = sprites[2];
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;

                case 9: // NW
                    newSprite = sprites[2];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 5: // NS
                    newSprite = sprites[3];
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;

                case 10: // EW
                    newSprite = sprites[3];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 7: // NES
                    newSprite = sprites[4];
                    transform.localRotation = Quaternion.Euler(0, 0, 180);
                    break;

                case 14: // ESW
                    newSprite = sprites[4];
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;

                case 13: // NSW
                    newSprite = sprites[4];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 11: // NEW
                    newSprite = sprites[4];
                    transform.localRotation = Quaternion.Euler(0, 0, 270);
                    break;

                case 15: // NESW
                    newSprite = sprites[5];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
            }

            spriteRenderer.sprite = newSprite;
            // spriteRenderer.color = IsBlocked ? Color.red : ( IsHighlighted ? Color.yellow : ( IsFilled ? Color.white : Color.gray ) );
        } else
        {
            Debug.LogWarning("Sprite renderer not existing");
        }
    }
}
