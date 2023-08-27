using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrate : MonoBehaviour
{
    public GunConfig currentGun;
    // Shooting player;
    public Animator animator;
    public SpriteRenderer display;

    void Start()
    {
        SetDisplay();
    }

    void Update()
    {
        // if(player)
        // {
        //     if(Input.GetKeyDown(KeyCode.E))
        //     {
        //         GunConfig gun = player.GetGun();
        //         player.SetGun(currentGun);
        //         currentGun = gun;
        //         SetDisplay();
        //     }
        // }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // player = col.GetComponent<Shooting>();
        // if(player)
        // {
        //     animator.SetBool("on",true);
        // }
    }

    void SetDisplay()
    {
        display.sprite = currentGun.graphic;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        // if(col.GetComponent<Shooting>() == player)
        // {
        //     player = null;
        //     animator.SetBool("on",false);
        // }
    }
}

