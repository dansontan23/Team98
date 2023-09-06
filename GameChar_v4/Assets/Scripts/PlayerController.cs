using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 150f;
    public float collisionOffset = 0.05f;
    //[SerializeField] float maxSpeed = 8f;
    //[SerializeField] float idleFriction = 0.9f;

    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;
    //bool isMoving = false;

    Vector2 cursorPosition;
    float lookAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(canMove)
        { 
            if(movementInput != Vector2.zero) {
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
            }

            if(movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if(movementInput.x > 0) {
                spriteRenderer.flipX = false;                
            }
        }
    }
    
    
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
    }
    
    // WASD/arrow keys movement
    void OnMove(InputValue movementValue) 
    {
        movementInput = movementValue.Get<Vector2>();
    }
    
    // Left mouse button for sword attack
    void OnFire() 
    {
        animator.SetInteger("swordAttackIndex", Random.Range(0, 3));
        animator.SetTrigger("swordAttack");
    }
    
    // Right mouse button (for now) for bow attack 
    // (Might add weapon swapping logic, not sure how to for now though)
    void OnBowAttack() 
    {
        animator.SetTrigger("bowAttack");
        //GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
    }
    
    public void SwordAttack()
    {
        LockMovement();
        if(spriteRenderer.flipX == true) {
            swordAttack.AttackLeft();
        } else {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    // Stops character movement during animations
    public void LockMovement() 
    {
        canMove = false;
    }
    
    // Unlocks character movement after animations
    public void UnlockMovement() 
    {
        canMove = true;
    }
    
    
}
