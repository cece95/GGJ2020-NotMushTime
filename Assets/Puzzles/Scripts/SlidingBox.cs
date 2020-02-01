using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingBox : MonoBehaviour{

    // block prefab
    GameObject block;

    GameObject[,] blocks = new GameObject[3,3];
    List<Vector3> possiblePositions = new List<Vector3>();

    //controls
    [HideInInspector] public float P1Vertical;
    [HideInInspector] public float P1Horizontal;

    // Start is called before the first frame update
    void Start()
    {
        block = (GameObject) Resources.Load("prefabs/Block");

        // create a set of positions so we check that there's no repetition
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector2 p = new Vector3(3 * i, 3 * j, 0);
                possiblePositions.Add(p);
            }
        }

        // generate blocks in random postiions
        List<int> alreadyExtractedNumbers = new List<int>();
        int n;
        for (int i = 0; i < 8; i++)
        {
            do
            {
                n = Random.Range(0, 8);
            } while (alreadyExtractedNumbers.Contains(n));

            alreadyExtractedNumbers.Add(n);
            Vector3 pos = possiblePositions[n];
            GameObject b = Instantiate(block, pos, Quaternion.identity);
            int nToShow = i + 1;
            b.GetComponentInChildren<TextMeshProUGUI>().text = nToShow.ToString();
            
            // save the blocks in the position matrix
            int x = n % 3;
            int y = n / 3;
            blocks[x, y] = b;
        }
    }

    // Update is called once per frame
    void Update() {
        // get the coordinates of the empty cell
        int x = 0, y = 0;

        for(int i=0; i<3; i++) { 
            for(int j=0; j<3; j++){
                if (blocks[i,j] == null){
                    x = i;
                    y = j;
                }
            }
        }

        // move the only box that can move in that direction. If none can move, do nothing
        if (P1Vertical > 0){
            if (y < 2) {
                blocks[x, y] = blocks[x, y + 1];
                blocks[x, y + 1] = null;
                blocks[x, y].transform.Translate(0, 3, 0);
            }
        }

        if (P1Vertical < 0){
            if (y > 0){
                blocks[x, y] = blocks[x, y - 1];
                blocks[x, y - 1] = null;
                blocks[x, y].transform.Translate(0, -3, 0);
            }
        }

        if (P1Horizontal > 0){
            if (x > 0){
                blocks[x, y] = blocks[x - 1, y];
                blocks[x - 1, y] = null;
                blocks[x, y].transform.Translate(3, 0, 0);
            }
        }

        if (P1Horizontal < 0){
            if (x < 2){
                blocks[x, y] = blocks[x + 1, y];
                blocks[x + 1, y] = null;
                blocks[x, y].transform.Translate(-3, 0, 0);
            }
        }

        // check for victory conditions
        int checkNumber = 1;
        bool win = true;
        for (int i1 = 0; i1<3; i1++)
        {
            for (int i2 = 0; i2<3; i2++)
            {
                GameObject bl = blocks[i1, i2];
                if (bl.GetComponentInChildren<TextMeshProUGUI>().text != checkNumber.ToString())
                {
                    win = false;
                }
            }
        }
    }
}
