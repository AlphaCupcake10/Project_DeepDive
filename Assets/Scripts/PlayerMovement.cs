using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;
    CharacterController2D controller;
    float horizontalMove = 0f;
    bool isJumping = false;

    bool isCrouching = false;

    public Animator animator;

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

        animator.SetFloat("Speed",Mathf.Abs(controller.getXVelocity()));
        animator.SetBool("isCrouching",isCrouching || controller.isSliding());
        animator.SetBool("isGrounded",controller.isGrounded());
        
    }

    public void OnLanding()
    {
        
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
    }
}
