using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ControllerEvents
{
    public UnityEvent onJump;
    public UnityEvent onSlideJump;
    public UnityEvent onLand;
}

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Parameters")]
    public float MaxVelocity = 4;
    [Range(1,25)] public float Smoothing = 7;
    [Range(0,1)] public float AirControlCoef = .2f;
    public float JumpHeight = 1;
    [Range(0,1)] public float CrouchSpeedCoef = 0.25f; 
    [Range(0,1)] public float SlidingSmoothness = .99f; 
    [Range(0,1)] public float SlideStartThreshold = .7f; 
    [Range(0,1)] public float SlideStopThreshold = .5f; 
    [Range(0,1)] public float SlideJumpStartThreshold = .7f;
    [Range(0,2)] public float SlideJumpSpeedBoost = .75f;
    public float SlideJumpHeight = 2;
    public float CayoteTimeMS = 100;
    public float JumpCooldownMS = 500;

    // REFERENCES
    Rigidbody2D RB;

    [Header("Checks")]
    public LayerMask WhatIsGround;
    public float CheckRadius = .2f;
    public Transform GroundCheckPoint;
    public Transform CeilingCheckPoint;
    
    [Header("Misc")]
    public Collider2D CrouchCollider;
    public ControllerEvents Events;
    
    // INPUT VARIABLES
    Vector2 InputAxis = Vector2.zero; // stores WASD
    bool InputJump = false; //store Jump
    bool InputJumpBuffer = false; //store Jump
    bool InputCrouch = false; //store Crouch
    

    // STATE VARIABLES
    bool isGrounded = false;
    bool p_isGrounded = false;
    bool isGroundedCayote = false;
    bool isSliding = false;
    bool isCrouching = false;
    float jumpTimer = 0;

    public float ANGLE = 0;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    public void SetInput(Vector2 _InputAxis,bool _InputJump,bool _InputCrouch)
    {
        InputAxis = _InputAxis;
        InputJump = _InputJump;
        if(InputJump)
        {
            UpdateJumpBuffer();
            CancelInvoke("UpdateJumpBuffer");
        }
        else
        {
            Invoke("UpdateJumpBuffer",100/1000);
        }
        InputCrouch = _InputCrouch;
    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0,0,ANGLE);
        Move();
    }

    void CheckGroundCeil()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheckPoint.position,CheckRadius,WhatIsGround);
        if(isGrounded && !p_isGrounded)
        {
            Events.onLand.Invoke();
            p_isGrounded = isGrounded;
        }
        else if(isGrounded != p_isGrounded)
        {
            p_isGrounded = isGrounded;
        }

        if(isGrounded)//Delaying Cayote only when false
        {
            UpdateCayote();CancelInvoke("UpdateCayote");
        }
        else
        {
            Invoke("UpdateCayote",CayoteTimeMS/1000);
        }

        isCrouching = Physics2D.OverlapCircle(CeilingCheckPoint.position,CheckRadius,WhatIsGround);
    }
    void UpdateCayote()
    {
        isGroundedCayote = isGrounded;
    }
    void UpdateJumpBuffer()
    {
        InputJumpBuffer = InputJump;
    }
    void Move()
    {
        // Initialization
        CheckGroundCeil();
        if(InputCrouch)isCrouching = true;
        Vector2 velocity = RB.velocity;
        if(!isSliding && isCrouching && isGrounded && Mathf.Abs(Vector2.Dot(transform.right,velocity)) > MaxVelocity * SlideStartThreshold)
        {
            isSliding = true;
        }
        if(!isGroundedCayote)
        {
            isSliding = false;
        }

        if(CrouchCollider)CrouchCollider.enabled = !((isCrouching || isSliding) && isGrounded);

        //Handle X movement
        if(isGrounded)
        {
            if(isSliding)
            {    
                float along = Vector2.Dot(transform.right,velocity);
                along *= SlidingSmoothness;
                velocity = transform.right * along + transform.up * Vector2.Dot(transform.up,velocity);
                if(Mathf.Abs(Vector2.Dot(transform.right,velocity)) < MaxVelocity * SlideStopThreshold)
                {
                    isSliding = false;
                }
            }
            else if(isCrouching)
            {
                velocity += (Vector2)transform.right * (MaxVelocity * CrouchSpeedCoef * InputAxis.x - Vector2.Dot(transform.right,velocity))/Smoothing;
            }
            else
            {
                float TargetVelocity = Mathf.Max(Mathf.Abs(Vector2.Dot(transform.right,velocity)),MaxVelocity);//Don't slow down if going fast
                velocity += (Vector2)transform.right * (TargetVelocity * InputAxis.x - Vector2.Dot(transform.right,velocity))/Smoothing;
            }
        }
        else//Air Movement
        {
            if(InputAxis.x != 0)//To preserve momentum
            {
                float TargetVelocity = Mathf.Max(Mathf.Abs(Vector2.Dot(transform.right,velocity)),MaxVelocity);//Don't slow down if going fast
                velocity += (Vector2)transform.right * (TargetVelocity * InputAxis.x - Vector2.Dot(transform.right,velocity))/Smoothing*AirControlCoef;
            }
        }
        jumpTimer += Time.fixedDeltaTime;
        if(isGroundedCayote && InputJumpBuffer && jumpTimer > JumpCooldownMS/1000)
        {
            if(isSliding)
            {
                if(Mathf.Abs(Vector2.Dot(transform.right,velocity)) <= MaxVelocity * SlideJumpStartThreshold)
                {
                    Events.onSlideJump.Invoke();
                    float along = Vector2.Dot(transform.up,velocity);
                    along = Mathf.Sqrt(Mathf.Abs(2*Physics2D.gravity.y*SlideJumpHeight));
                    velocity = (Vector2)(transform.up * along + transform.right * Vector2.Dot(transform.right,velocity));
                    velocity += (Vector2)transform.right * (MaxVelocity * SlideJumpSpeedBoost * Mathf.Sign(Vector2.Dot(transform.right,velocity)) - Vector2.Dot(transform.right,velocity))/Smoothing;
                    ResetJumpVars();
                }
            }
            else
            {
                Events.onJump.Invoke();
                float along = Vector2.Dot(transform.up,velocity);
                along = Mathf.Sqrt(Mathf.Abs(2*Physics2D.gravity.y*JumpHeight));
                velocity = (Vector2)(transform.up * along + transform.right * Vector2.Dot(transform.right,velocity));
                ResetJumpVars();
            }
        }
        RB.velocity = velocity;
    }
    void ResetJumpVars()
    {
        jumpTimer = 0; 
        isGrounded = false;
        UpdateCayote();
        UpdateJumpBuffer();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(GroundCheckPoint.position, CheckRadius);
        Gizmos.DrawSphere(CeilingCheckPoint.position, CheckRadius);
    }
}
