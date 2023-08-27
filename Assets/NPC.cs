using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    PlayerMovement player;
    public Animator animator;
    public Dialogue dialogue;
    public SpriteRenderer image;

    public Sprite stand;
    public Sprite sit;

    void Start()
    {
        if(dialogue.isSitting)
        {
            image.sprite = sit;
        }
        else
        {
            image.sprite = stand;
        }
    }



    void Update()
    {
        if(player)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(DialogueManager.Instance.HasActiveDialogue())
                {
                    DialogueManager.Instance.DisplayNextSentnce();
                }
                else
                {
                    DialogueManager.Instance.StartDialogue(dialogue);
                }
            }
        }
    }

    public void SetDialogue(Dialogue val)
    {
        dialogue = val;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player = col.GetComponent<PlayerMovement>();
        if(player)
        {
            animator.SetBool("on",true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.GetComponent<PlayerMovement>() == player)
        {
            player = null;
            animator.SetBool("on",false);
            DialogueManager.Instance.EndDialogue();
        }
    }
}
