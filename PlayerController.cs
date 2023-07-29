// chunks of code that are commented out are just backups and figuring out things
// just a test base, don't use this

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float collisionOffset = 0.05f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;
    
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    
    Vector2 movementInput = Vector2.zero;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    // List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    bool canMove = true;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() 
    {
        if(canMove == true && movementInput != Vector2.zero) {            
            // Move animation and add velocity
            
            // Accelerate while run direction is pressed
            // Don't allow player to run faster than max speed
            
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * moveSpeed * Time.deltaTime), maxSpeed);
            
            /*if(movementInput != Vector2.zero) 
            {
                bool success = TryMove(movementInput);

                if(!success && movementInput.x > 0) {
                    success = TryMove(new Vector2(movementInput.x, 0));

                    if(!success && movementInput.y > 0) {
                        success = TryMove(new Vector2(movementInput.y, 0));
                    }  
                }



                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }*/

            // Set direction of sprite to movement direction
            if(movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if(movementInput.x > 0) {
                spriteRenderer.flipX = false;                
            }
            
            IsMoving = true;
        } else {
            // No movement so interpolate velocity towards 0
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            
            IsMoving = false;
        }
        
    }
    
    public bool IsMoving {
        set{
            isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }
    
    /*
    private bool TryMove(Vector2 direction){
        //if(direction != Vector2.zero) {
            // Check for potental collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collsions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        //} else {
            // Can't move if there's no direction to move in
        //    return false;
        //}
    }*/
    
    void OnMove(InputValue movementValue) 
    {
        movementInput = movementValue.Get<Vector2>();
    }
    
    void OnFire() 
    {
        animator.SetTrigger("swordAttack");    
    }
    
    public void SwordAttack() 
    {
        LockMovement();
        
        if(spriteRenderer.flipX == true){ 
            swordAttack.AttackLeft();
        } else {
            swordAttack.AttackRight();
        }
    }
    
    public void EndSwordAttack() {
        UnlockMovement();
        swordAttack.StopAttack();
    }
    
    public void LockMovement() 
    {
        canMove = false;
    }
    
    public void UnlockMovement() 
    {
        canMove = true;
    }
    
}
