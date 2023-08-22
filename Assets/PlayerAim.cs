using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAim : MonoBehaviour
{
    Vector3 MousePosition = Vector2.zero;
    public float angle = 0; 
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = MousePosition - transform.position;

        angle = Mathf.Atan2(diff.y,Mathf.Abs(diff.x))*Mathf.Rad2Deg;

        if(angle < -22.5f)playerMovement.setState(3);
        if(Mathf.Abs(angle) < 22.5f)playerMovement.setState(2);
        if(angle > 22.5f && angle < 67.5f)playerMovement.setState(1);
        if(angle > 67.5f)playerMovement.setState(0);
    }
}
