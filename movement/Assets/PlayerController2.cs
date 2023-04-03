using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    Rigidbody2D rb;

    float walkSpeed = 4f;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;

    Animator animator;
    string currentState;
    const string PLAYER_IDLE = "idle";
    const string PLAYER_WALK = "walk";
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal =  Input.GetAxisRaw("Horizontal");   
        inputVertical =  Input.GetAxisRaw("Vertical");   

    }

    void FixedUpdate()
    {
        // if (inputHorizontal != 0 || inputVertical !=0){
        //     rb.velocity = new Vector2(inputHorizontal*walkSpeed, inputVertical*walkSpeed);
        // }
        rb.velocity = new Vector2(inputHorizontal, inputVertical).normalized * walkSpeed ;    
        if (inputHorizontal  !=0 || inputVertical != 0){
            changeAnimationState(PLAYER_WALK);
        }
        else {
            changeAnimationState(PLAYER_IDLE);
        }
        
    }


    void changeAnimationState(string newState){
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

}
