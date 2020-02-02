using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PipesPuzzleScript : MonoBehaviour
{
    public PipeTile prefab;

    public static Node[,] nodes = new Node[5,5];
    // Start is called before the first frame update
    void Start()
    {
        //generate an 2d array of nodes
        

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                print("Creating Node : i = " + i + ";  j = " + j);
                nodes[i,j] = new Node(new Vector2Int(i, j));
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                print("Updating Neighbours : i = " + i + ";  j = " + j);
                nodes[i,j].updateNeighbours();
            }
        }


        //randomly generate start and end nodes


        Node start, end = nodes[0,0];
        
        int randomStart = Random.Range(0, 4);
        int randomEnd = Random.Range(0, 4);

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
            end = nodes[4,randomEnd];
        }
        else
        {
            start = nodes[randomEnd,4];
        }



        //find a path through the pipes that uses every pipe once at maximum

        List<Node> visited = new List<Node>();



        //start at the starting node
        Node current = start;
        Node previous;
        Node next;
        int length = 0;
        int collissions = 0;


        while (!current.Equals(end))
        {
            
            collissions = 0;
            //pick a random connected node
            //check to see if we have moved to it before
            //if not, move to that node

            length = current.getConnections().Count();

            Node randomNode = current.getConnections()[Random.Range(0, length)];

            if (visited.Contains(randomNode) == false) // if we have not visited the randomly selected node
            {
                visited.Add(current);
                current = randomNode; //move to the randomly selected node
            }

            //go back to the start if we run in to a dead end
            length = current.getConnections().Count();


            for (int i = 0; i < length; i++)
            {
                
                if (visited.Contains(current.getConnections()[i]))
                {
                    collissions++;
                }
            }
            if (collissions == length)
            {

                //set the current node to the start
                current = start;
                //clear the visited list
                visited.Clear();
               
            }
            
        }
        visited.Add(end);
        visited.Reverse();
        for(int i = 1; i < visited.Count -1; i++)
        {
            //get the node at index of i in the list of visited nodes
            current = visited[i];

            //get the nodes in front and behind it
            previous = visited[i - 1];
            next = visited[i + 1];
            
            //check to see if this piece has to be straight of bent

            if((current.getPosition() - previous.getPosition()) + (current.getPosition() - next.getPosition()) == new Vector2Int(0,0)) //the connection is straight
            {
                double r = Random.value;

                if (r < 0.1)
                {
                    current.setTile('x');
                }
                else if (r > 0.55)
                {
                    current.setTile('i');
                }
                else
                {
                    current.setTile('t');
                }
            }

            else // the connection is bent
            {
                double r = Random.value;

                if(r < 0.1)
                {
                    current.setTile('x');
                }
                else if( r > 0.55)
                {
                    current.setTile('l');
                }
                else
                {
                    current.setTile('t');
                }

            }

           
        }

        start.setTile('o');
        end.setTile('o');
     

        //if there are any empty tiles, randomly generate the piece to go in to them

        foreach (Node n in nodes)
        {
            if(n.getTile().Equals(null))
            {
                double r = Random.value;

                if (r < 0.1)
                {
                    n.setTile('x');
                }
                else if ( 0.4> r && r > 0.1)
                {
                    n.setTile('l');
                }
                else if(0.6 > r && r > 0.4)
                {
                    n.setTile('t');
                }
                else
                {
                    n.setTile('i');
                }
            }

            for (int i = 0; i< Random.Range(1, 4); i++)
            {
                n.Spin();
            }

            PipeTile newtile = Instantiate(prefab, new Vector3(n.getPosition().x, n.getPosition().y), Quaternion.identity);

            

            if(n.getConnectionDirections().Contains(Vector2Int.up))
            {
                newtile.N.color = Color.red;
            }
            if (n.getConnectionDirections().Contains(Vector2Int.down))
            {
                newtile.S.color = Color.red;
            }
            if (n.getConnectionDirections().Contains(Vector2Int.left))
            {
                newtile.W.color = Color.red;
            }
            if (n.getConnectionDirections().Contains(Vector2Int.right))
            {
                newtile.E.color = Color.red;
            }

        }

 
    }

    // Update is called once per frame
    void Update()
    {
        //if a block is selected, rotate it and update the blocks it is connected to

    }
}

public class Node
{
    private char tile;
    private Vector2Int position;
    private List<Node> connections = new List<Node>();
    private int rotation = 0;
    private List<Vector2Int> connectionDirections = new List<Vector2Int>();

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

        if (this.position.x < 4)
        {
            
            this.connections.Add(PipesPuzzleScript.nodes[this.position.x + 1,this.position.y]);
        }
        if (this.position.y > 0)
        {
           
            this.connections.Add(PipesPuzzleScript.nodes[this.position.x,this.position.y - 1]);
        }

        if (this.position.y < 4)
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

    public void setTile(char type)
    {
        this.tile = type;
    }

    public char getTile()
    {
        return this.tile;
    }

    public List<Vector2Int> getConnectionDirections()
    {
        return connectionDirections;
    }

    public void setConnections(List<Vector2Int> Connections)
    {
        this.connections.Clear();
        foreach (Vector2Int c in Connections)
        {
            int x = this.getPosition().x + c.x;
            int y = this.getPosition().y + c.y;

            
            if (x>0 && x<4 && y>0 && y<4)
            {
                this.connections.Add(PipesPuzzleScript.nodes[this.getPosition().x + c.x, this.getPosition().y + c.y]);
            }
        }
        connectionDirections = Connections;

    }

    public void Spin()
    {
        
        List<Vector2Int> connections = new List<Vector2Int>() {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
        List<Vector2Int> connected = new List<Vector2Int>();

        if (this.tile.Equals('l'))
        {
            if (rotation == 0)
            {
                connected.Add(Vector2Int.up);
                connected.Add(Vector2Int.right);

            }

            if (rotation == 1)
            {
                connected.Add(Vector2Int.down);
                connected.Add(Vector2Int.right);
            }
            if (rotation == 2)
            {
                connected.Add(Vector2Int.down);
                connected.Add(Vector2Int.left);
            }
            if (rotation == 3)
            {
                connected.Add(Vector2Int.up);
                connected.Add(Vector2Int.left);
            }

            setConnections(connected);
            rotation++;
            if (rotation == 4) { rotation = 0; }

        }

        if (this.tile.Equals('i'))
        {
            if (rotation == 0)
            {
                connected.Add(Vector2Int.up);
                connected.Add(Vector2Int.down);

            }

            if (rotation == 1)
            {
                connected.Add(Vector2Int.left);
                connected.Add(Vector2Int.right);
            }

            setConnections(connected);
            rotation++;
            if (rotation == 2) { rotation = 0; }


        }

        if (this.tile.Equals('t'))
        {
            if (rotation == 0)
            {
                connected.Add(Vector2Int.up);
                connected.Add(Vector2Int.right);
                connected.Add(Vector2Int.down);

            }

            if (rotation == 1)
            {
                connected.Add(Vector2Int.down);
                connected.Add(Vector2Int.left);
                connected.Add(Vector2Int.right);
            }
            if (rotation == 2)
            {
                connected.Add(Vector2Int.up);
                connected.Add(Vector2Int.left);
                connected.Add(Vector2Int.down);

            }

            if (rotation == 3)
            {
                connected.Add(Vector2Int.up);
                connected.Add(Vector2Int.left);
                connected.Add(Vector2Int.right);
            }
            setConnections(connected);
            rotation++;
            if (rotation == 4) { rotation = 0; }


        }

        if (this.tile.Equals('x')) { setConnections(connections); }

       

    }

}

public class Tile
{
    private char type;
    
    private List<Node> connectedNodes;

    public Tile(char type)
    {
        this.type = type;

        if (type.Equals("x")) //this is  a cross piece, it is connected to all surrounding tiles
        {
           



        }

        if(type.Equals("i")) // this is a straight line block
        {

        }
         
        if(type.Equals("t")) //this is a T block
        {

        }

        if(type.Equals("l")) // this is an L block
        {

        }
    }
}
