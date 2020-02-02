using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTile : MonoBehaviour
{
    public SpriteRenderer N, S, W, E, C;
    public int desiredRotation;
    public int staticRotation;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
