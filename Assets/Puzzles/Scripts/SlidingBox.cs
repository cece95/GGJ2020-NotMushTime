using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingBox : MonoBehaviour{

    // block prefab
    GameObject block;

    // 
    List<GameObject> blocks = new List<GameObject>();
    List<Vector3> possiblePositions = new List<Vector3>();

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
            b.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString(); 
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
