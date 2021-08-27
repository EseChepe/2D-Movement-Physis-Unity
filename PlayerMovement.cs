using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;
    private bool facingRight = true;
    const float groundCheckRadius = 0.37f;

    private float movementX;
    private Rigidbody2D myBody;
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    private SpriteRenderer sr;
    [SerializeField] private bool isGrounded = false;
    // private string GROUND_TAG = "Ground";

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    private void Awake() {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        PlayerJump();
        WallJump();
    }

    private void FixedUpdate() {
        PlayerMoveKeyboard();
        FlipAction();
    }

    //Horizontal movement of the player
    void PlayerMoveKeyboard() {
        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.fixedDeltaTime;
    }

    //Flips the player whenever it moves left or right
    void FlipAction() {
        if (facingRight == false && movementX > 0) {
            Flip();
        } else if (facingRight == true && movementX < 0) {
            Flip();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    //Makes the player jump
    void PlayerJump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    //Checks when the player is colliding with a GROUND element to moke sure it doent's jump when it's airbone
    void GroundCheck() {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0) {
            isGrounded = true;
        }
    }

    //Checks if the player is holding against a GROUND element
    //Reduces it's falling speed when it's holding against a GROUND element
    //Makes it so that the player can jump while it's holding against a GROUND element
    void WallJump() {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, groundCheckRadius, groundLayer);
        
        if (isTouchingFront == true && isGrounded == false && movementX != 0) {
            wallSliding = true;
        } else {
            wallSliding = false;
        }

        if (wallSliding) {
            myBody.velocity = new Vector2(myBody.velocity.x, Mathf.Clamp(myBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetButtonDown("Jump") && wallSliding == true) {
            wallJumping = true;
            Invoke("SetWallJumpFalse", wallJumpTime);
        }

        if (wallJumping == true) {
            myBody.velocity = new Vector2(xWallForce * -movementX, yWallForce);
        }
    }

    void SetWallJumpFalse() {
        wallJumping = false;
    }
}
