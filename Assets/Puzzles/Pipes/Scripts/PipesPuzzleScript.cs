using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesPuzzleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //generate an 2d array of nodes
        Node[][] nodes = new Node[6][]; 

        for(int i = 0; i<6; i++)
        {
            for(int j = 0; j<6; j++)
            {
                nodes[i][j] = new Node(new Vector2Int(i, j));
            }
        }

        //randomly generate start and end nodes


        Node start;
        Node end;
        int randomStart = Random.Range(0, 6);
        int randomEnd = Random.Range(0, 6);

        //randomly generate start node
        if (Random.value > .5)
        {
            start = nodes[0][randomStart];
        }
        else
        {
            start = nodes[randomStart][0];
        }

        //randomly generate end node
        if (Random.value > .5)
        {
            end = nodes[0][randomEnd];
        }
        else
        {
            start = nodes[randomEnd][0];
        }



        //find a path through the pipes that uses every pipe once at maximum

        List<Vector2Int> visited = new List<Vector2Int>();



        //start at the starting node
        Node current = start;

        //pick a random connected node
        
        //check to see if we have moved to it before

        //if not, move to that node

        //check to see if we are at the end node -- loop if not

        //if all of the connected nodes have been visited, reset the list of visited nodes and start again

        //if we are at the end node, record the path that we took to get there



        //based on the path, place tiles that form the path

        //if there are any empty tiles, randomly generate the piece to go in to them

        //once the grid is full, rotate all the tiles a random number of times (between 0 and 3)




        //fill all the other nodes with random pipes
        //rotate all the nodes a random number of times
    }

    // Update is called once per frame
    void Update()
    {
        //if a block is selected, rotate it and update the blocks it is connected to

    }
}

public class Node
{
    private Vector2Int position;
    private List<Vector2Int> connections;

    public Node(Vector2Int position)
    {
        this.position = position;

        //add connected nodes to list of connections
        this.connections.Add(this.position + new Vector2Int(1,0));
        this.connections.Add(this.position + new Vector2Int(-1, 0));
        this.connections.Add(this.position + new Vector2Int(0, -1));
        this.connections.Add(this.position + new Vector2Int(0, 1));
    }

    public List<Vector2Int> getConnections()
    {
        return this.connections;
    }

}

public class Tile
{
    
    public Tile()
    {

    }
}
