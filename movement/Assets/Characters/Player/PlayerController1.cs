using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Movement and animation for a player
public class PlayerController : MonoBehaviour
{
    // The names of each animation and animation parameter for the player stored in a scriptable object
    public PlayerAnimationStrings animationStrings; 

    bool IsMoving { 
        set {
            isMoving = value;

            if(isMoving) {
                rb.drag = moveDrag;
            } else {
                rb.drag = stopDrag;
            }
        }
    }

    public bool CharacterPhysicsEnabled {
        get {
            return CharacterPhysicsEnabled;
        }
        set {
            if(value == true) {
                rb.simulated = true;
            } else {
                rb.velocity = Vector2.zero;
                rb.simulated = false;
            }
        }
    }

    public float moveSpeed = 1250f;

    // Drag when player is moving around the level
    public float moveDrag = 15f;
    
    // Drag when player is not able or trying to move
    public float stopDrag = 25f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    
    Collider2D swordCollider;
    Vector2 moveInput = Vector2.zero;

    bool isMoving = false;
    bool canMove = true;
    bool animLocked = false;


    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        // Control animation parameters
        if(!animLocked && moveInput != Vector2.zero) {
            animator.SetFloat(animationStrings.moveX, moveInput.x);
            animator.SetFloat(animationStrings.moveY, moveInput.y);
        }

        if(rb.bodyType == RigidbodyType2D.Dynamic) {
            if(canMove == true && moveInput != Vector2.zero) {
                // Move animation and add velocity
                // Accelerate the player while run direction is pressed (limited by rigidbody linear drag)
                rb.AddForce(moveInput * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);

                IsMoving = true;

                } else {
                    IsMoving = false;
                }
        }

        UpdateAnimation();
    }

    // When animation can be freely selected based on movement, select the animation to play
    void UpdateAnimation() {
        if(!animLocked && canMove) {
            if(moveInput != Vector2.zero) {
                animator.Play(animationStrings.walk);
            } else {
                animator.Play(animationStrings.idle);
            }
        }
    }


    // Get input values for player movement
    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    // Play Attack animation and try to do damage
    void OnFire() {
        if(canMove) {
            animator.Play(animationStrings.attack);
            canMove = false;
        }
    }

    // Allow animations to be freely selected and the character to move again
    void UnlockAnimation() {
        canMove = true;
        animLocked = false;
    }

    // Lock animation into hit animation
    public void OnHit() {
        animLocked = true;
        animator.Play(animationStrings.hit);
    }

    // Lock animation into death animation
    public void OnDeath() {
        canMove = false;
        animLocked = true;
        animator.Play(animationStrings.die);
    }
}
