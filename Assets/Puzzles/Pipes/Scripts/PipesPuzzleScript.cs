using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PipesPuzzleScript : Puzzle
{
    [SerializeField]
    private bool debugMode = false;

    public PipeTile prefab;

    public Sprite[] sprites;

    public static Node[,] nodes = new Node[5,5];
    Node startNode, endNode = nodes[0, 0];
    private int boardWidth = 5;
    private int boardHeight = 5;

    private PlayerController selector, rotator;

    private int selectedX = 2;
    private int selectedY = 2;

    public override void StartPuzzle(Player[] players)
    {
        base.StartPuzzle(players);

        selector = players[0].GetPlayerController();
        rotator = players[1].GetPlayerController();
    }

    private void Awake()
    {
        if(debugMode)
        {
            selector = PlayerInput.Instance.Player1;
            rotator = PlayerInput.Instance.Player2;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform contentTransform = transform.Find("Content");

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                nodes[i,j] = new Node(new Vector2Int(i, j));
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
               
                nodes[i,j].updateNeighbours();
            }
        }


        //randomly generate start and end nodes
        
        int randomStart = Random.Range(0, 4);
        int randomEnd = Random.Range(0, 4);

        if (Random.value > .5)
        {
            startNode = nodes[0,randomStart];
        }
        else
        {
            startNode = nodes[randomStart,0];
        }

        //randomly generate end node
        if (Random.value > .5)
        {
            endNode = nodes[4,randomEnd];
        }
        else
        {
            startNode = nodes[randomEnd,4];
        }

        //find a path through the pipes that uses every pipe once at maximum

        List<Node> visited = new List<Node>();

        //start at the starting node
        Node current = startNode;
        Node previous;
        Node next;
        int length = 0;
        int collissions = 0;

        while (!current.Equals(endNode))
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
                current = startNode;
                //clear the visited list
                visited.Clear();
               
            }
            
        }

        visited.Add(endNode);

        visited.Reverse();

        for (int i = 1; i < visited.Count -1; i++)
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
                else if (r > 0.45)
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
                else if( r > 0.45)
                {
                    current.setTile('l');
                }
                else
                {
                    current.setTile('t');
                }

            }

           
        }

        startNode.setTile('s');
        endNode.setTile('e');
     

        //if there are any empty tiles, randomly generate the piece to go in to them

        foreach (Node n in nodes)
        {
            if(n.getTile().Equals('\0')) // if the character variable is empty
            {
                double r = Random.value;

                if (r < 0.07)
                {
                    n.setTile('x');
                }
                else if ( 0.5> r && r > 0.07)
                {
                    n.setTile('l');
                }
                else if(0.65 > r && r > 0.5)
                {
                    n.setTile('t');
                }
                else
                {
                    n.setTile('i');
                }
            }

            //rotate every node a random number of times
            for (int i = 0; i< Random.Range(1, 4); i++)
            {
                n.Spin();
            }

            //
            PipeTile newtile = Instantiate(prefab, new Vector3(n.getPosition().x, n.getPosition().y), Quaternion.identity);


            //place blocks in correct tiles
            {

                if (n.getTile().Equals('s'))
                {
                    newtile.SetSprite(sprites[0]);
                    n.glowing = true;
                }
                else if (n.getTile().Equals('e'))
                {
                    newtile.SetSprite(sprites[0]);
                }
                else if (n.getTile().Equals('i'))
                {
                    newtile.SetSprite(sprites[2]);
                }
                else if (n.getTile().Equals('l'))
                {
                    newtile.SetSprite(sprites[3]);
                }
                else if (n.getTile().Equals('t'))
                {
                    newtile.SetSprite(sprites[4]);
                }
                else if (n.getTile().Equals('x'))
                {
                    newtile.SetSprite(sprites[5]);
                }

            }

            //rotate blocks to correct orientation
            newtile.SetRotation(n.getRotation() * 90);
            newtile.transform.SetParent(contentTransform);
            n.PipeNode = newtile;
        }
    }

    void FixedUpdate()
    {
        if(selector == null || rotator == null)
        {
            return;
        }

        //if the player presses the green button, rotate the 
        if (selector.HorizontalPress > 0)
        {
            selectedX = Mathf.Clamp(selectedX + 1, 0, boardWidth - 1);
            UpdateSelection();
        }
        if (selector.HorizontalPress < 0)
        {
            selectedX = Mathf.Clamp(selectedX - 1, 0, boardWidth - 1);
            UpdateSelection();
        }
        if (selector.VerticalPress > 0)
        {
            selectedY = Mathf.Clamp(selectedY + 1, 0, boardHeight - 1);
            UpdateSelection();
        }
        if (selector.VerticalPress < 0)
        {
            selectedY = Mathf.Clamp(selectedY - 1, 0, boardHeight - 1);
            UpdateSelection();
        }

        if(rotator.IsGreenDown())
        {
            // TODO: Rotate piece
            Node currentNode = nodes[selectedX, selectedY];

            currentNode.Spin();

            currentNode.PipeNode.desiredRotation = currentNode.getRotation() * 90.0f;
            // nodes[selectedY, selectedX].PipeNode.Rotate() ???
            // Rotate sprite as well
        }
    }

    private void Update()
    {

        if (rotator.IsGreenDown())
        {
            for (int i = 0; i < nodes[2, 2].getConnections().Count(); i++)
            { 
                print(nodes[2, 2].getConnections()[i].getPosition().ToString());
             }
            foreach (Node n in nodes)
            {
                if (n.getConnections().Any(item => item.glowing))
                {
                    n.glowing = true;
                }
                else
                {
                    if (n != startNode)
                    {
                        n.glowing = false;
                    }
                }


                if (n.glowing)
                {
                    n.PipeNode.SetGlowing(true);
                }
            }


        }
    }

    void UpdateSelection()
    {
        for(int x = 0; x < 5; ++x)
        {
            for(int y = 0; y < 5; ++y)
            {
                nodes[x, y].PipeNode.SetSelected(false);
            }
        }

        nodes[selectedX, selectedY].PipeNode.SetSelected(true);
        // foreach(node ... )
        // remove selection

        // nodes[selectedX, selectedY].select();
    }
}

public class Node
{
    public bool glowing;
    private char tile;
    private Vector2Int position;
    private List<Node> connections = new List<Node>();
    private int rotation = 0;
    private List<Vector2Int> connectionDirections = new List<Vector2Int>();

    public PipeTile PipeNode;

    public Node(Vector2Int position)
    {
        this.position = position;
    } // function that created a node at location "position"

    public int getRotation() { return rotation; }

    // function used in initial setup to connect all adjacent nodes to one another
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

    // function that lists the nodes connected to the current node
    public List<Node> getConnections()
    {
        return this.connections;
    } 


    // function that returns the position of the current node
    public Vector2Int getPosition()
    {
        return this.position;
    } 

    //function to set the tile type
    public void setTile(char type)
    {
        this.tile = type;
    }

    public char getTile()
    {
        return this.tile;
    } //function for getting the type of tile the selected node is

    public List<Vector2Int> getConnectionDirections()
    {
        return connectionDirections;
    } //function that returns the directions in which the node is trying to connect with other nodes

    public void setConnections(List<Vector2Int> Connections)
    {
        this.connections.Clear();
        foreach (Vector2Int c in Connections)
        {
            connectionDirections = Connections;

            int x = this.getPosition().x + c.x;
            int y = this.getPosition().y + c.y;

            
            if (x>0 && x<4 && y>0 && y<4)
            {
                this.connections.Add(PipesPuzzleScript.nodes[this.getPosition().x + c.x, this.getPosition().y + c.y]);
            }
        }
       

    } //function to change which nodes the current node is connected to

    public void Spin()
    {
        
        List<Vector2Int> connections = new List<Vector2Int>() {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};
        List<Vector2Int> connected = new List<Vector2Int>();

        if (this.tile.Equals('l'))
        {
            if (rotation == 0)
            {
                connected.Add(Vector2Int.down);
                connected.Add(Vector2Int.left);

            }
            if (rotation == 1)
            {
                connected.Add(Vector2Int.down);
                connected.Add(Vector2Int.right);
            }

            if (rotation == 2)
            {
                connected.Add(Vector2Int.up);
                connected.Add(Vector2Int.right);
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
            if (rotation == 1)
            {
                connected.Add(Vector2Int.up);
                connected.Add(Vector2Int.down);

            }

            if (rotation == 0)
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
                connected.Add(Vector2Int.up);
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
                connected.Add(Vector2Int.down);
                connected.Add(Vector2Int.left);
                connected.Add(Vector2Int.right);
            }

            setConnections(connected);
            rotation++;
            if (rotation == 4) { rotation = 0; }


        }

        if (this.tile.Equals('x')) { setConnections(connections); }

        if (this.tile.Equals('s') || this.tile.Equals('e'))
        {
            if (rotation == 0)
            {
                connected.Add(Vector2Int.down);
                setConnections(connected);
            }
            if (rotation == 1)
            {
                connected.Add(Vector2Int.left);
                setConnections(connected);
            }
            if (rotation == 2)
            {
                connected.Add(Vector2Int.up);
                setConnections(connected);
            }
            if (rotation == 3)
            {
                connected.Add(Vector2Int.right);
                setConnections(connected);
            }
            setConnections(connected);
            rotation++;
            if (rotation == 4) { rotation = 0; }
        }

       

    } //function to rotate the selected node 90 degrees clockwise

}

