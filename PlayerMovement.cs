using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool facingRight = true;
    private bool isJumping;
    const float groundCheckRadius = 0.37f;

    private float hDirection;
    private float yDirection;

    private float movementX;
    private Rigidbody2D myBody;
    [SerializeField] LayerMask groundLayer;
    private SpriteRenderer sr;
    [SerializeField] private bool isGrounded = false;
    // private string GROUND_TAG = "Ground";

    bool isTouchingGround;
    public Transform groundCheckCollider;
    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    [SerializeField] private float dashSpeed;
    private float dashTime;
    [SerializeField] private float starDashTime;
    private int direction;
    public bool canDash = true;
    bool dashIsVertical;

    SpriteRenderer spriteRenderer;


    private void Start() {
        dashTime = starDashTime;
        myBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void FixedUpdate() {
        PlayerMoveKeyboard();
        JumpDuration();
    }

    // Update is called once per frame
    void Update()
    {   
        Dash();
        GroundCheck();
        WallJump();
        FlipAction();
        PlayerJump();
        ChangeColor();
    }

    //Horizontal movement of the player
    void PlayerMoveKeyboard() {
        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.fixedDeltaTime;

        if (myBody.velocity.x > 0 && movementX < 0) {
            myBody.AddForce(new Vector2(-myBody.velocity.x, 0), ForceMode2D.Impulse);
        } else if (myBody.velocity.x < 0 && movementX > 0) {
            myBody.AddForce(new Vector2(-myBody.velocity.x, 0), ForceMode2D.Impulse);
        }
    }

    //Flips the player whenever it moves left or right
    void FlipAction() {
        if (facingRight == false && movementX > 0) {
            Flip();
        } else if (facingRight && movementX < 0) {
            Flip();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    //Checks when the player is colliding with a GROUND element to moke sure it doent's jump when it's airbone
    void GroundCheck() {
        isGrounded = false;
        isTouchingGround = Physics2D.OverlapCircle(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (isTouchingGround) {
            isGrounded = true;
            canDash = true;
        }
    }

    //Makes the player jump
    void PlayerJump() {
        if ((Input.GetButtonDown("Jump")) && isGrounded) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            if (myBody.velocity.x > 0 || myBody.velocity.x == 0) {
                myBody.AddForce(new Vector2((movementX * myBody.velocity.x) * -(-movementX), jumpForce), ForceMode2D.Impulse);
            } else if (myBody.velocity.x < 0) {
                myBody.AddForce(new Vector2(-(movementX * myBody.velocity.x) * -(movementX), jumpForce), ForceMode2D.Impulse);
            } 
            
        } 

        if ((Input.GetButton("Jump")) && isGrounded == false) {
            isJumping = true;
        }
    }

    void JumpDuration() {
        //The longer the player holds the JUMP button the higher it will jump
        if (Input.GetButton("Jump") && isJumping) {
            if (jumpTimeCounter > 0) {
                myBody.AddForce(new Vector2(movementX, jumpForce), ForceMode2D.Impulse);
                jumpTimeCounter -= Time.fixedDeltaTime;
            }
        } else {
            isJumping = false;
        }

        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
        }
    }

    //Checks if the player is holding against a GROUND element
    //Reduces it's falling speed when it's holding against a GROUND element
    //Makes it so that the player can jump while it's holding against a GROUND element
    void WallJump() {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, 0.1f, groundLayer);
        
        if (isTouchingFront && isGrounded == false) {
            wallSliding = true;
        } else {
            wallSliding = false;
        }

        if (wallSliding) {
            myBody.velocity = new Vector2(myBody.velocity.x, Mathf.Clamp(myBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
            canDash = true;
        }

        if (Input.GetButtonDown("Jump") && wallSliding) {
            wallJumping = true;
            Invoke("SetWallJumpFalse", wallJumpTime);
        }

        if (wallJumping == true) {
            myBody.velocity = new Vector2((xWallForce * -movementX) + (-myBody.velocity.x), yWallForce);
        }
    }

    void SetWallJumpFalse() {
        wallJumping = false;
    }

    //Dash move, GetButton("Dash") is refering to the L key
    void Dash() {
        if (direction == 0) {

            if ((Input.GetButton("Left") && Input.GetButton("Up")) && Input.GetButtonDown("Dash") && canDash) {
                direction = 1;
            } else if (Input.GetButton("Right") && Input.GetButton("Up") && Input.GetButtonDown("Dash") && canDash) {
                direction = 2;
            } else if (Input.GetButton("Left") && Input.GetButton("Down") && Input.GetButtonDown("Dash") && canDash) {
                direction = 3;
            } else if (Input.GetButton("Right") && Input.GetButton("Down") && Input.GetButtonDown("Dash") && canDash) {
                direction = 4;
            } else if (Input.GetButton("Up") && Input.GetButtonDown("Dash") && canDash) {
                direction = 5;
            } else if (Input.GetButton("Down") && Input.GetButtonDown("Dash") && canDash) {
                direction = 6;
            } else if ((Input.GetButton("Left") && Input.GetButtonDown("Dash") && canDash) || (Input.GetButtonDown("Dash") && isGrounded && facingRight == false)) {
                direction = 7;
            } else if ((Input.GetButton("Right") && Input.GetButtonDown("Dash") && canDash) || (Input.GetButtonDown("Dash") && isGrounded && facingRight)) {
                direction = 8;
            }

        } else {
            
            //Makes sure the player doesn't dash undefinetely

            //First if statement prevents the player from impulsing itself in case it dahes vertically
            if (dashTime <= 0 && dashIsVertical) {
                canDash = false;
                direction = 0;
                dashTime = starDashTime;
                myBody.velocity = new Vector2(0 , myBody.velocity.y / 4f);
            } else if (dashTime <= 0 && facingRight && dashIsVertical == false) {
                canDash = false;
                direction = 0;
                dashTime = starDashTime;
                myBody.velocity = new Vector2(((myBody.velocity.x / 10) * 0.5f * movementX + (moveForce * movementX)), myBody.velocity.y / 4f);
            } else if (dashTime <= 0 && facingRight == false && dashIsVertical == false) {
                canDash = false;
                direction = 0;
                dashTime = starDashTime;
                myBody.velocity = new Vector2((-(myBody.velocity.x / 10) * 0.5f * movementX + (moveForce * movementX)), myBody.velocity.y / 4f);
            } else {
                dashTime -= Time.deltaTime;

                //Applies the Dash move to the Rigidbody
                if (direction == 1) {
                    dashIsVertical = false;
                    myBody.velocity = (Vector2.left * dashSpeed) + (Vector2.up * dashSpeed);
                } else if (direction == 2) {
                    dashIsVertical = false;
                    myBody.velocity = (Vector2.right * dashSpeed) + (Vector2.up * dashSpeed);
                } else if (direction == 3) {
                    dashIsVertical = false;
                    myBody.velocity = (Vector2.left * dashSpeed) + (Vector2.down * dashSpeed);
                } else if (direction == 4) {
                    dashIsVertical = false;
                    myBody.velocity = (Vector2.right * dashSpeed) + (Vector2.down * dashSpeed);
                } else if (direction == 5) {
                    dashIsVertical = true;
                    myBody.velocity = Vector2.up * (dashSpeed * 1.5f);
                } else if (direction == 6) {
                    dashIsVertical = true;
                    myBody.velocity = Vector2.down * (dashSpeed * 1.5f);
                } else if (direction == 7) {
                    dashIsVertical = false;
                    myBody.velocity = Vector2.left * dashSpeed;
                } else if (direction == 8) {
                    dashIsVertical = false;
                    myBody.velocity = Vector2.right * dashSpeed;
                }
            }

        }
    }

    void ChangeColor() {
        if (canDash == false) {
            spriteRenderer.color = Color.red;
        } else if (canDash) {
            spriteRenderer.color = new Color(0.2704254f, 0.8392157f, 0.9716981f, 1f);
        }
    }

}
