using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    static float MOVEMENT_SPEED_MODIFIER = 1.75f;
    [Header("Player Variables")]
    public float Horizontal;
    public float Vertical;
    public bool Red;
    public bool Green;
    public bool Yellow;
    public bool Blue;
    public Vector2 Speed = new Vector3(0, 0);

    [Header("Linked Scripts")]
    public Rigidbody2D rigidbody_;
    public PlayerInput input_;
    private bool mush;

    // Start is called before the first frame update
    void Start()
    {
        if (rigidbody_.name.Contains("Mush"))
            mush = true;
        else
            mush = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (mush)
        {
            // If we are mush, then we are player one
            Vertical = input_.P1Vertical;
            Horizontal = input_.P1Horizontal;

            Red = input_.P1Red;
            Green = input_.P1Green;
            Yellow = input_.P1Yellow;
            Blue = input_.P1Blue;


        }
        else
        {
            // otherwise we are player 2
            Vertical = input_.P2Vertical;
            Horizontal = input_.P2Horizontal;

            Red = input_.P2Red;
            Green = input_.P2Green;
            Yellow = input_.P2Yellow;
            Blue = input_.P2Blue;
        }

        Speed.x = Horizontal;
        Speed.y = Vertical;
        
        //move the player based on the player input
        rigidbody_.AddForce(Speed.normalized * MOVEMENT_SPEED_MODIFIER);
        //rigidbody_.velocity.Set(input_.P1Horizontal, input_.P1Vertical);
        //rigidbody_.transform.SetPositionAndRotation((rigidbody_.position + Speed), rigidbody_.rotation);


        //if the player is near a grabbable object, pick it up

        if (Green) //green button will be used for interaction, anything in these brackets will be called when the green button is pressed 
        {

        }

        if (Red) // if the red button is being pressed for the player
        {

        }

        if (Blue) // if the blue button is being pressed
        {

        }

        if (Yellow) //if the yellow button is being pressed
        {

        }
    }

}
