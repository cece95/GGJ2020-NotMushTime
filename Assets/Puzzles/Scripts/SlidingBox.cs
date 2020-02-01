using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingBox : MonoBehaviour{

    // block prefab
    GameObject block;

    static int SIZE = 2;

    GameObject[,] blocks = new GameObject[SIZE,SIZE];
    List<Vector3> possiblePositions = new List<Vector3>();

    bool checkWin_flag = true;

    // Start is called before the first frame update
    void Start()
    {

        block = (GameObject) Resources.Load("Prefabs/Block");

        int positiveStart = SIZE / 2;
        int negativeStart = -SIZE / 2;

        // create a set of positions so we check that there's no repetition
        for (int pi = positiveStart; pi > positiveStart - SIZE; pi--)
        {
            for (int pj = negativeStart; pj < negativeStart + SIZE; pj++)
            {
                Vector3 p = new Vector3(3 * pj, 3 * pi, 0);
                possiblePositions.Add(p);
            }
        }

        // generate blocks in random postiions
        List<int> alreadyExtractedNumbers = new List<int>();
        int n;
        for (int i = 0; i < SIZE * SIZE - 1; i++)
        {
            do
            {
                n = Random.Range(0, SIZE*SIZE - 1);
            } while (alreadyExtractedNumbers.Contains(n));

            alreadyExtractedNumbers.Add(n);
            Vector3 pos = possiblePositions[n];
            GameObject b = Instantiate(block, pos, Quaternion.identity);
            int nToShow = i + 1;
            b.GetComponentInChildren<TextMeshProUGUI>().text = nToShow.ToString();
            
            // save the blocks in the position matrix
            int x = n % SIZE;
            int y = n / SIZE;
            blocks[x, y] = b;
        }
    }

    // Update is called once per frame
    void Update() {

        if (checkWin_flag == true)
        {
            checkWin();
            checkWin_flag = false;
        }

        // get the coordinates of the empty cell
        int x = 0, y = 0;

        for(int i=0; i<SIZE; i++) { 
            for(int j=0; j<SIZE; j++){
                if (blocks[i,j] == null){
                    x = i;
                    y = j;
                }
            }
        }

        // move the only box that can move in that direction. If none can move, do nothing
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (y < SIZE-1) {
                blocks[x, y] = blocks[x, y + 1];
                blocks[x, y + 1] = null;
                blocks[x, y].transform.Translate(0, 3, 0);

                // check for victory conditions
                checkWin_flag = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)){
            if (y > 0){
                blocks[x, y] = blocks[x, y - 1];
                blocks[x, y - 1] = null;
                blocks[x, y].transform.Translate(0, -3, 0);

                // check for victory conditions
                checkWin_flag = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)){
            if (x > 0){
                blocks[x, y] = blocks[x - 1, y];
                blocks[x - 1, y] = null;
                blocks[x, y].transform.Translate(3, 0, 0);

                // check for victory conditions
                checkWin_flag = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            if (x < SIZE-1){
                blocks[x, y] = blocks[x + 1, y];
                blocks[x + 1, y] = null;
                blocks[x, y].transform.Translate(-3, 0, 0);
            }

            // check for victory conditions
            checkWin_flag = true;
        }
    }

    private bool checkWin()
    {
        int checkNumber = 1;
        bool win = true;
        for (int i1 = 0; i1 < SIZE; i1++)
        {
            for (int i2 = 0; i2 < SIZE; i2++)
            {
                GameObject bl = blocks[i2, i1];
                if (bl != null && checkNumber < SIZE*SIZE)
                {
                    string blockNumber = bl.GetComponentInChildren<TextMeshProUGUI>().text;
                    if (blockNumber != checkNumber.ToString())
                    {
                        win = false;
                    }
                }
                checkNumber++;
            }
        }
        return win;
    }
}
