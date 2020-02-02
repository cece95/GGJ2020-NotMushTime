using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTile : MonoBehaviour
{
    public SpriteRenderer N, S, W, E, C;
    public int desiredRotation;
    public int staticRotation;

    //this variable is the current orientation of the object
    public int rotation;
    
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Vector3 standardScale = new Vector3(0.5f, 0.5f, 1.0f);

    private bool isSelected;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = standardScale;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

    public void SetRotation(float rotation)
    {
        desiredRotation = rotation;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentRotation = transform.localRotation.eulerAngles.z;

        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.LerpAngle(currentRotation, desiredRotation, 0.2f));
        transform.localScale = Vector3.Lerp(transform.localScale, (isSelected ? 1.2f : 1.0f) * standardScale, 0.2f);
    }
}
