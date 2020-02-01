using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PipesPuzzleScript : MonoBehaviour
{

    public static Node[,] nodes = new Node[7,7];
    // Start is called before the first frame update
    void Start()
    {
        //generate an 2d array of nodes
        

        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                print("i = " + i + ";  j = " + j);
                nodes[i,j] = new Node(new Vector2Int(i, j));
            }
        }

        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                print("i = " + i + ";  j = " + j);
                nodes[i,j].updateNeighbours();
            }
        }


        //randomly generate start and end nodes


        Node start, end = nodes[0,0];
        
        int randomStart = Random.Range(0, 6);
        int randomEnd = Random.Range(0, 6);

        //randomly generate start node
        if (Random.value > .5)
        {
            start = nodes[0,randomStart];
        }
        else
        {
            start = nodes[randomStart,0];
        }

        //randomly generate end node
        if (Random.value > .5)
        {
            end = nodes[6,randomEnd];
        }
        else
        {
            start = nodes[randomEnd,6];
        }



        //find a path through the pipes that uses every pipe once at maximum

        List<Node> visited = new List<Node>();



        //start at the starting node
        Node current = start;

        visited.Add(current);


        while (!current.Equals(end))
        {


            //pick a random connected node
            //check to see if we have moved to it before
            //if not, move to that node
            if (!visited.Contains(current.getConnections()[Random.Range(0, 3)])) // if we have not visited the randomly selected node
            {
                current = current.getConnections()[Random.Range(0, 3)]; //move to the randomly selected node
            }

            visited.Add(current);

            //go back to the start if we run in to a dead end

            int length = current.getConnections().Count();
            int collissions = 0;

            for(int i = 0; i< length; i++)
            {
                if (visited.Contains(current.getConnections()[i])) { collissions++; }
            }
            if (collissions == length)
            {
                print("pathfinding resetting");
                //set the current node to the start
                current = start;
                //clear the visited list
                visited.Clear();
            }

        }

        foreach (Node n in visited) {
            print(n.getPosition().ToString() + "\n");
        }
        
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
    private List<Node> connections = new List<Node>();

    public Node(Vector2Int position)
    {
        this.position = position;

       
    }


    public void updateNeighbours()
    {
        //add connected nodes to list of connections
        
        if(this.position.x > 0)
        {
            this.connections.Add(PipesPuzzleScript.nodes[this.position.x - 1,this.position.y]);
        }

        if (this.position.x < 6)
        {
            Debug.Log(this.position.x);
            this.connections.Add(PipesPuzzleScript.nodes[this.position.x + 1,this.position.y]);
        }
        if (this.position.y > 0)
        {
            this.connections.Add(PipesPuzzleScript.nodes[this.position.x,this.position.y - 1]);
        }

        if (this.position.y < 6)
        {
            this.connections.Add(PipesPuzzleScript.nodes[this.position.x,this.position.y + 1]);
        }

        
        
        
        
    }
    public List<Node> getConnections()
    {
        return this.connections;
    }

    public Vector2Int getPosition()
    {
        return this.position;
    }
}

public class Tile
{
    
    public Tile()
    {

    }
}
