using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerAnimationStrings animationStrings;
    public float moveSpeed = 1f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    } 

    

    void FixedUpdate() 
    {
        if(moveInput != Vector2.zero) {
            animator.SetFloat("moveX", moveInput.x);
            animator.SetFloat("moveY", moveInput.y);
        }
        
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        UpdateAnimation();
    }

    void UpdateAnimation() {
        
            if(moveInput != Vector2.zero) {
                animator.Play("Base Layer.player_walk");
            } else {
                animator.Play("Base Layer.player_idle");
            }
        
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
