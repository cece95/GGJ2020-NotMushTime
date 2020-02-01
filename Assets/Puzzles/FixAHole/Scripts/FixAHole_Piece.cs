using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAHole_Piece : MonoBehaviour
{
    public int PieceHeight { get; private set; }
    public int PieceWidth { get; private set; }
    public bool IsHeld;

    [SerializeField]
    public FixAHole_PieceAsset PieceAsset;

    private bool[,] PieceStructure;

    private SpriteRenderer spriteRenderer;
    private Animation animation;

    [SerializeField]
    private Sprite[] sprites;

    private float desiredOrientation = 0;
    private bool isInitialized = false;

    public Vector2Int spriteOffset;

    private Transform rootTransform;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animation = GetComponent<Animation>();

        Debug.Log(PieceAsset);
    }

    public void Initialize(FixAHole_PieceAsset pieceAsset)
    {
        PieceAsset = pieceAsset;
        GetPieceStructure();
        UpdateSprites();
    }

    public void DestroyPiece()
    {
        Destroy(gameObject);
    }

    void UpdateSprites()
    {
        GenerateBlockTiles();

        rootTransform = transform.Find("AnimRoot");
        isInitialized = true;
    }

    void GetPieceStructure()
    {
        int w = PieceAsset.PieceStructure[0].Length;
        int h = PieceAsset.PieceStructure.Count;

        PieceWidth = w;
        PieceHeight = h;

        bool[,] structure = new bool[h, w];

        for (int y = 0; y < h; ++y)
        {
            for (int x = 0; x < w; ++x)
            {
                structure[y, x] = PieceAsset.PieceStructure[y][x] == '1';
            }
        }

        PieceStructure = structure;
    }

    public void RotatePiece(bool rotateRight)
    {
        desiredOrientation += (rotateRight ? -1 : 1) * 90.0f;
        while(desiredOrientation > 360.0f)
        {
            desiredOrientation -= 360.0f;
        }
        while(desiredOrientation < 0.0f)
        {
            desiredOrientation += 360.0f;
        }

        int rot = (int)(desiredOrientation / 90.0f) % 4;
        Debug.Log(rot);
        switch (rot)
        {
            case 0:
                spriteOffset = Vector2Int.zero;
                break;

            case 3:
                spriteOffset = new Vector2Int(0, PieceHeight - 1);
                break;

            case 2:
                spriteOffset = new Vector2Int(-PieceHeight + 1, PieceWidth - 1);
                break;

            case 1:
                spriteOffset = new Vector2Int(-PieceWidth + 1, 0);
                break;
        }

        int w = PieceHeight;
        int h = PieceWidth;

        bool[,] structure = new bool[h, w];

        for (int y = 0; y < h; ++y)
        {
            for (int x = 0; x < w; ++x)
            {
                if (rotateRight)
                {
                    structure[y, x] = PieceStructure[w - x - 1, y];

                }
                else
                {
                    structure[y, x] = PieceStructure[x, h - y - 1];
                }
            }
        }

        PieceWidth = w;
        PieceHeight = h;

        PieceStructure = structure;
    }

    public bool[,] GetStructure()
    {
        return PieceStructure;
    }

    public void SetHighlight(bool isHighlighted)
    {
        if (spriteRenderer)
        {
            spriteRenderer.color = isHighlighted ? Color.white : Color.gray;
        }
    }

    public void Chuck()
    {
        animation.Play();
    }

    void GenerateBlockTiles()
    {
        for(int y = 0; y < PieceHeight; ++y)
        {
            for(int x = 0; x < PieceWidth; ++x)
            {
                GameObject spriteObject = new GameObject();
                SpriteRenderer sr = spriteObject.AddComponent<SpriteRenderer>();

                if(PieceStructure[y, x])
                {
                    int neighbours = CalculateNeighbours(x, y);
                    UpdateSprite(sr, neighbours);
                }

                spriteObject.transform.SetParent(transform.Find("AnimRoot"));
                spriteObject.transform.localPosition = new Vector3(x, -y);
            }
        }
        
    }

    int CalculateNeighbours(int x, int y)
    {
        if (!PieceStructure[y, x])
        {
            return 0;
        }

        int counter = 0;

        if (x - 1 >= 0 && PieceStructure[y, x - 1])
        {
            counter |= 8;
        }
        if (x + 1 < PieceWidth && PieceStructure[y, x + 1])
        {
            counter |= 2;
        }
        if (y - 1 >= 0 && PieceStructure[y - 1, x])
        {
            counter |= 1;
        }
        if (y + 1 < PieceHeight && PieceStructure[y + 1, x])
        {
            counter |= 4;
        }

        return counter;
    }

    void UpdateSprite(SpriteRenderer spriteRenderer, int neighbours)
    {
        if (spriteRenderer)
        {
            Sprite newSprite = null;

            switch (neighbours)
            {
                case 0: // None
                    newSprite = sprites[0];
                    break;

                case 1: // N
                    newSprite = sprites[1];
                    transform.localRotation = Quaternion.Euler(0, 0, 180);
                    break;

                case 2: // E
                    newSprite = sprites[1];
                    transform.localRotation = Quaternion.Euler(0, 0, 270);
                    break;

                case 4: // S
                    newSprite = sprites[1];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 8: // W
                    newSprite = sprites[1];
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;

                case 3: // NE
                    newSprite = sprites[2];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 6: // SE
                    newSprite = sprites[2];
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;

                case 12: // SW
                    newSprite = sprites[2];
                    transform.localRotation = Quaternion.Euler(0, 0, 180);
                    break;

                case 9: // NW
                    newSprite = sprites[2];
                    transform.localRotation = Quaternion.Euler(0, 0, 270);
                    break;

                case 5: // NS
                    newSprite = sprites[3];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 10: // EW
                    newSprite = sprites[3];
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;

                case 7: // NES
                    newSprite = sprites[4];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;

                case 14: // ESW
                    newSprite = sprites[4];
                    transform.localRotation = Quaternion.Euler(0, 0, 270);
                    break;

                case 13: // NSW
                    newSprite = sprites[4];
                    transform.localRotation = Quaternion.Euler(0, 0, 180);
                    break;

                case 11: // NEW
                    newSprite = sprites[4];
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;

                case 15: // NESW
                    newSprite = sprites[5];
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
            }

            spriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("Sprite renderer not existing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInitialized)
        {
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Mathf.LerpAngle(transform.localRotation.eulerAngles.z, desiredOrientation, 0.2f));
            if(IsHeld)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.2f);

                if (rootTransform != null)
                {
                    Vector3 rootPosition = rootTransform.localPosition;
                    rootTransform.localPosition = Vector3.Lerp(rootPosition, new Vector3(spriteOffset.x, spriteOffset.y), 0.2f);
                }
            }
        }
    }
}
