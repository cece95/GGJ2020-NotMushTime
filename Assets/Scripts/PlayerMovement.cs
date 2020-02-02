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
    private bool mush;
    Animator animator;
    AnimationCurve depth_scale_curve;
    PlayerController player_controller;

    private bool allowsMovement = true;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody_ = GetComponent<Rigidbody2D>();

        // Set-Up Depth scale curve
        depth_scale_curve = new AnimationCurve();
        depth_scale_curve.AddKey(-2.4f, 1.15f);
        depth_scale_curve.AddKey(2.4f, 0.85f);
    }

    public void SetAllowMovement(bool allow)
    {
        allowsMovement = allow;
    }

    public void SetPlayerController(PlayerController controller)
    {
        player_controller = controller;
    }

    // Update is called once per frame
    void Update()
    {
        // Scale based on Y-Axis position in order to give perception of deapth
        transform.localScale = Vector3.one * depth_scale_curve.Evaluate(transform.position.y);
    }
    void FixedUpdate()
    {
        if (allowsMovement && player_controller != null)
        {
            Speed.x = player_controller.Horizontal;
            Speed.y = player_controller.Vertical;
            rigidbody_.AddForce(Speed.normalized * MOVEMENT_SPEED_MODIFIER);
            if (Speed.magnitude > 0.01f)
                animator.SetBool("Moving", true);
            else
                animator.SetBool("Moving", false);

        }
        else
            animator.SetBool("Moving", false);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        // Bounce between players
        if (other.gameObject.tag == "Player")
        {
            if (other.GetContact(0).relativeVelocity.magnitude > PLAYER_BOUNCE_THRESHOLD)
                animator.SetTrigger("Bounce");

            rigidbody_.AddForce(other.GetContact(0).relativeVelocity * PLAYER_BOUNCE_MODIFIER);
        }
    }
}
