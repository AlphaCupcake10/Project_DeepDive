using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;
    CharacterController2D controller;
    float horizontalMove = 0f;
    bool isJumping = false;

    bool isCrouching = false;

    public Animator animator;
    public AnimatorOverrideController[] animationControllers;
    [Range(0,3)]
    int state = 0;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }

        animator.runtimeAnimatorController = animationControllers[state];

        animator.SetFloat("Speed",Mathf.Abs(controller.getXVelocity()));
        animator.SetBool("isCrouching",isCrouching || controller.isSliding());
        animator.SetBool("isGrounded",controller.isGrounded());
        animator.SetBool("isSliding",controller.isSliding());
        
    }

    public void setState(int value)
    {
        state = value;
    }

    void FixedUpdate()
    {
        if(Input.GetButton("Fire3"))horizontalMove *= 0.01f*speed;
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
    }
}
