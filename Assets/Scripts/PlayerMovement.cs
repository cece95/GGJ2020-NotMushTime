using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    static float MOVEMENT_SPEED_MODIFIER = 1.75f;
    static float PLAYER_BOUNCE_MODIFIER = 4.00f;
    static float PLAYER_BOUNCE_THRESHOLD = 3.0f;
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
    Animator animator;
    AnimationCurve depth_scale_curve;

    // Start is called before the first frame update
    void Start()
    {
        if (rigidbody_.name.Contains("Mush"))
            mush = true;
        else
            mush = false;

        animator = GetComponentInChildren<Animator>();

        // Set-Up Depth scale curve
        depth_scale_curve = new AnimationCurve();
        depth_scale_curve.AddKey(-2.4f, 1.15f);
        depth_scale_curve.AddKey(2.4f, 0.85f);
    }


    // Update is called once per frame
    void Update()
    {
        // Scale based on Y-Axis position in order to give perception of deapth
        transform.localScale = Vector3.one * depth_scale_curve.Evaluate(transform.position.y);
    }
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
        
        rigidbody_.AddForce(Speed.normalized * MOVEMENT_SPEED_MODIFIER);


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
    
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        // Bounce between players
        if(other.gameObject.tag == "Player")
        {            
            if(other.GetContact(0).relativeVelocity.magnitude > PLAYER_BOUNCE_THRESHOLD)
                animator.SetTrigger("Bounce");
            
            rigidbody_.AddForce(other.GetContact(0).relativeVelocity * PLAYER_BOUNCE_MODIFIER);
        }
    }

}
