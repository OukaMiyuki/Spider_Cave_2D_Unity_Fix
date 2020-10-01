using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMoveController : MonoBehaviour {

    [Header("Animation Component")]
    [SerializeField] private Animator animator;

    [Header("Horizontal Movement")]
    [SerializeField] private float moveSpeed = 10f;
    public float facingDirection;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float jumpDelay = 0.25f;
    private float jumpTimer = 0;

    [Header("Physics")]
    [SerializeField] private float maxSpeed = 7f;
    [SerializeField] private float linearDrag = 4f;
    [SerializeField] private float gravity = 2f;
    [SerializeField] private float fallMultiplier = 10f;

    [Header("Components")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask fallGroundLayer;

    [Header("Collisions")]
    [SerializeField] public bool onGround = false;
    [SerializeField] public bool onFallGround = false;
    [SerializeField] private float groundLength = 0.6f;
    [SerializeField] private Vector3 colliderOffset;

    private bool moveLeft;
    private bool moveRight;

    public static PlayerMoveController instance;

    private void Awake() {
        if (instance == null) { // if the instance is null, initialise this class
            instance = this;
        }

    }

    // Update is called once per frame
    void Update() {
        // get axis direction from input
        //direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // initialize onGround true or false by using raycast Raycast(transform origin, direction, distance, layermask)
        // Read More about it : https://gamedevacademy.org/learn-and-understand-raycasting-in-unity3d/#:~:text=%E2%80%9CRaycasting%20is%20the%20process%20of,the%20path%20of%20the%20ray.%E2%80%9D&text=Raycasting%20in%20Unity3D%20is%20broken,of%20which%20are%20physics%20objects.
        // Learn More about it : https://youtu.be/EINgIoTG8D4
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer)
            || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (PlayerDeath.instance.GetDieOrAlive() && !PlayerWin.instance.GetWin()) {
            if (CrossPlatformInputManager.GetButtonDown("Jump")) {
                jumpTimer = Time.time + jumpDelay;
            }
        }
    }

    void FixedUpdate() {
        if (PlayerDeath.instance.GetDieOrAlive()) {
            if (!PlayerWin.instance.GetWin()) {
                moveCharacter();
                if (jumpTimer > Time.time && onGround) {
                    Jump();
                    //animator.SetFloat("vertical", rb.velocity.y);
                }
                modifyPhysics();
            }
            RunAnimation.instance.RunningAnimation();
        } else {
            rb.gravityScale = 10;
            rb.drag = 0;
        }
    }

    public void setMovingDirection(bool moveLeft) {
        this.moveLeft = moveLeft;
        this.moveRight = !moveLeft;
    }

    public void playerStopMoving() {
        this.moveLeft = false;
        this.moveRight = false;
    }

    void moveCharacter() {
        //move
        //rb.AddForce(Vector2.right * horizontal * moveSpeed * 100 * Time.deltaTime); 

        if (moveLeft) {
            facingDirection = -1f;
        } else if (moveRight) {
            facingDirection = 1f;
        } else {
            facingDirection = 0f;
        }

        rb.AddForce(Vector2.right * facingDirection * moveSpeed * 100 * Time.deltaTime);

        //if (Input.GetKey(KeyCode.A)) {
        //    rb.AddForce(Vector2.right * -1f * moveSpeed * 100 * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.D)) {
        //    rb.AddForce(Vector2.right * 1f * moveSpeed * 100 * Time.deltaTime);
        //}

        // if character is move to right (horizontal > 0) and it's not facing right or character move to left (horizontal < 0) and it's facing right
        if ((facingDirection > 0 && !facingRight) || (facingDirection < 0 && facingRight)) {
            flip(); // do a flip based on left or right movement
        }

        // limit the speed of the player movement
        // Read more about Mathf.Sign https://www.geeksforgeeks.org/mathf-sign-method-in-c-sharp-with-examples/
        // For short, Mathf.Sign will return 0, 1 or -1 based on some conditions, read link above
        if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // if character is on the ground
        if ((onGround && !CrossPlatformInputManager.GetButtonDown("Jump")) || (onGround && !CrossPlatformInputManager.GetButton("Jump"))) {
            animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x)); // set animation float value based on horizontal movement
        }

        //animator.SetFloat("vertical", rb.velocity.y); // set animation float value based on vertical movement
    }

    void flip() {
        facingRight = !facingRight;
        // if facing right is true than rotate to 0 if false then rotate on 180° relative on y axes
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed * Time.deltaTime * 100, ForceMode2D.Impulse);
        jumpTimer = 0f;
    }

    void modifyPhysics() {
        // if character is facing right then change dirrection or press left or change to other direction
        bool changeDirectionLinear = (facingDirection > 0 && rb.velocity.x < 0) || (facingDirection < 0 && rb.velocity.x > 0);

        if (onGround) { // if character on ground
            // so when character is about to stop or stop (the direction set to 0.4 or the speed (almost 0 or means stop, that's why it's using Mathf.abs, so no matter where direction is it))
            // or when character is change direction suddenly (so like ehen you long press the right arrow then immediately press left (changeDirectionLinear) )
            if (facingDirection == 0f || changeDirectionLinear) {
                rb.drag = linearDrag; // then apply linearvdrag, so it'll not slide
            } else {
                rb.drag = 0f; // then, when character is moving, change the linear drag back to 0, so it'll move faster and smoother
            }

            // if you still confuse with the if condition above then,
            // basically when character is top, or about to stop or when it moving then change direction immediately, then apply linear drag.
            // so the character won't slide on the ground, then when it moving change the linear drag back to 0 so then it'll move or run more faster
            // try to observe the liner drag value on Player's RigidBody2D when you run the game and move the character

            // set the gravity scale to 0
            rb.gravityScale = 0;
        } else {
            rb.gravityScale = gravity; // if character is not on the ground or it means it jumping then when it falling apply gravity scale on it
            rb.drag = linearDrag * 0.1f; // set the linear drag value
            if (rb.velocity.y < 0) { // then when it jump (basically this is condition when player long press the jump button)
                rb.gravityScale = gravity * fallMultiplier; // then apply grafity scale (10)
            } else if (rb.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump")) { // limit the jump by set the grafity scale when the y velocity above 0 and the player didn't lomg press the jump button
                rb.gravityScale = gravity * (fallMultiplier / 4); //then apply the gravity scale to 10/4
            }

            // if you still don't get what the code saying, then basically the first condition set to when the player long press the jump button and the other when player didn't long press the jump button
            // the different of jump height was actually on the if condition, the first condition less than 0, so the velocity will go till negative numbers, and will cause player to jump higher till the edege of thejump then the grafity scale applied
            // the other condition set to greater than 0 so it'll automatically apply the 2nd condition then set thegravity scale when it greater than 0, so it still hasn't reached the edge of the jump but the gravity scale has applied, so it'll fall
        }
    }

    void OnDrawGizmos() {
        // draw gizmo line
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

    public void BouncePlayer(float force) {
        if (onGround) {
            onGround = false;
            rb.AddForce(new Vector2(0, (force*Time.deltaTime*100)));
        }
    }
}
