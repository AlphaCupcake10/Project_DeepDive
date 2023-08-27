using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Gravity : MonoBehaviour
{
    public float angle;
    Rigidbody2D RB;
    float defaultGravityScale = 1;
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        defaultGravityScale = RB.gravityScale;
        RB.gravityScale = 0;
    }

    void FixedUpdate()
    {
        RB.AddForce(new Vector2(Mathf.Cos((angle+90)*Mathf.Deg2Rad),Mathf.Sin((angle+90)*Mathf.Deg2Rad))*RB.mass*Physics2D.gravity.y);
    }

    
    void onDisable()
    {
        RB.gravityScale = defaultGravityScale;
    }
}
