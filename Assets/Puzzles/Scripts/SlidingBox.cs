using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingBox : Puzzle {

    [SerializeField]
    private Sprite[] blockSprites;

    [SerializeField]
    private Vector2 centerPoint;

    [SerializeField]
    private Vector2 blockSize;

    // block prefab
    GameObject block;
    PlayerController player;

    static int SIZE = 3;
    static int N_EMPTY_CELLS = 1;
    static int N_SCRAMBLE = 20;

    GameObject[,] blocks = new GameObject[SIZE,SIZE];
    List<Vector3> possiblePositions = new List<Vector3>();

    bool checkWin_flag = true;

    public override void StartPuzzle(Player[] players)
    {
        base.StartPuzzle(players);

        player = players[0].GetPlayerController();
    }

    // Start is called before the first frame update
    void Awake()
    {
        player = PlayerInput.Instance.Player1;

        block = (GameObject) Resources.Load("SlideyBlocks/Prefabs/Block");
        blockSprites = Resources.LoadAll<Sprite>("SlideyBlocks/Sprites");

        int positiveStart = SIZE / 2;
        int negativeStart = -SIZE / 2;

        // create a set of positions so we check that there's no repetition
        for (int pi = positiveStart; pi > positiveStart - SIZE; pi--)
        {
            for (int pj = negativeStart; pj < negativeStart + SIZE; pj++)
            {
                Vector3 p = centerPoint + new Vector2(pj, pi) * blockSize;
                possiblePositions.Add(p);
            }
        }

        // generate blocks in random postiions
        List<int> alreadyExtractedNumbers = new List<int>();
        int n;
        for (int i = 0; i < SIZE * SIZE - N_EMPTY_CELLS; i++)
        {
            do
            {
                n = i;
                //n = Random.Range(0, SIZE*SIZE - N_EMPTY_CELLS);
            } while (alreadyExtractedNumbers.Contains(n));

            alreadyExtractedNumbers.Add(n);
            Vector3 pos = possiblePositions[n];
            GameObject b = Instantiate(block);
            b.GetComponent<SpriteRenderer>().sprite = blockSprites[i];
            int nToShow = i + 1;
            b.name = nToShow.ToString();

            //add block as children of the prefab
            b.transform.SetParent(transform.Find("Content").Find("Blocks"));
            b.transform.localPosition = pos;

            b.layer = 8;
            
            // save the blocks in the position matrix
            int x = n % SIZE;
            int y = n / SIZE;
            blocks[x, y] = b;
        }

        scramblePuzzle();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (checkWin_flag)
        {
            checkWin();
            checkWin_flag = false;
        }

        Tuple<int, int> emptyCell = getEmptyCell();
        int x = emptyCell.Item1;
        int y = emptyCell.Item2;

        // move the only box that can move in that direction. If none can move, do nothing
        if (player.VerticalPress > 0){
            moveUp(x,y);
            checkWin_flag = true;
        }

        if (player.VerticalPress < 0){
            moveDown(x, y);
            checkWin_flag = true;
        }

        if (player.HorizontalPress > 0){
            moveRight(x, y);
            checkWin_flag = true;
        }

        if (player.HorizontalPress < 0){
            moveLeft(x, y);
            checkWin_flag = true;
        }
    }

    private void checkWin()
    {
        int checkNumber = 1;
        bool win = true;
        for (int i1 = 0; i1 < SIZE; i1++)
        {
            for (int i2 = 0; i2 < SIZE; i2++)
            {
                GameObject bl = blocks[i2, i1];
                if (bl != null && checkNumber <= SIZE*SIZE)
                {
                    string blockNumber = bl.name;
                    Debug.Log(blockNumber + " | " + checkNumber.ToString());
                    if (blockNumber != checkNumber.ToString())
                    {
                        win = false;
                    }
                }
                checkNumber++;
            }
        }
        Debug.Log(win);
        if (win)
        {
            OnCompleted();
        }
    }

    private Tuple<int,int> getEmptyCell()
    {
        int x = 0, y = 0;

        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (blocks[i, j] == null)
                {
                    x = i;
                    y = j;
                }
            }
        }
        return new Tuple<int, int>(x, y);
    }

    private void moveUp(int x, int y)
    {
        if (y < SIZE - 1)
        {
            blocks[x, y] = blocks[x, y + 1];
            blocks[x, y + 1] = null;
            blocks[x, y].transform.Translate(0, blockSize.y, 0);
        }
    }

    private void moveDown(int x, int y)
    {
        if (y > 0)
        {
            blocks[x, y] = blocks[x, y - 1];
            blocks[x, y - 1] = null;
            blocks[x, y].transform.Translate(0, -blockSize.y, 0);
        }
    }

    private void moveRight(int x, int y)
    {
        if (x > 0)
        {
            blocks[x, y] = blocks[x - 1, y];
            blocks[x - 1, y] = null;
            blocks[x, y].transform.Translate(blockSize.x, 0, 0);
        }
    }

    private void moveLeft(int x, int y)
    {
        if (x < SIZE - 1)
        {
            blocks[x, y] = blocks[x + 1, y];
            blocks[x + 1, y] = null;
            blocks[x, y].transform.Translate(-blockSize.x, 0, 0);
        }
    }

    private void scramblePuzzle()
    {
        for (int k = 1; k < N_SCRAMBLE; k++)
        {
            Tuple<int, int> empty = getEmptyCell();
            int x = empty.Item1;
            int y = empty.Item2;
            
            int r = UnityEngine.Random.Range(0, 4);
            
            switch (r)
            {
                case 0:
                    {
                        moveUp(x, y);
                        Debug.Log("Up");
                    }
                    break;
                case 1:
                    {
                        moveRight(x, y);
                        Debug.Log("Right");
                    }
                    break;
                case 2:
                    {
                        moveDown(x, y);
                        Debug.Log("Down");
                    }
                    break;
                case 3:
                    {
                        moveLeft(x, y);
                        Debug.Log("Left");
                    }
                    break;
            }
        }
    }
}
