using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController2D controller;    
    Gravity gravity;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        gravity = GetComponent<Gravity>();
    }
    void Update()
    {
        controller.ANGLE = gravity.angle;
        GetInput();
    }
    void GetInput()
    {

        bool _InputJump = Input.GetButton("Jump");
        bool _InputCrouch = Input.GetButton("Crouch");
        Vector2 _InputAxis = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        controller.SetInput(_InputAxis,_InputJump,_InputCrouch);
    }
}
