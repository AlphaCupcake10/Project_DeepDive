using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float speed = 4f;

  public CharacterController2D controller;

  public Animator animator;

  float horizontalMove = 0f;
  bool isJumping = false;

  bool isCrouching = false;

  void Update()
  {
    horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

    animator.SetFloat("speed", Mathf.Abs(horizontalMove));

    if(Input.GetButtonDown("Jump"))
    {
      isJumping = true;
    }

    if(Input.GetButtonDown("Crouch"))
    {
      isCrouching = true;
    } else if(Input.GetButtonUp("Crouch"))
    {
      isCrouching = false;
    }
  }

  public void OnLanding()
  {
    animator.SetBool("isJumping", isJumping);

    isJumping = false;
  }

  void FixedUpdate()
  {
    controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);

    animator.SetBool("isCrouching", isCrouching);
  }
}
