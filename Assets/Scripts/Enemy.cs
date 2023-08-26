using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  GameManager playerManager;

  CharacterController2D controller;

  public Vector2 distance;

  public float maxHealth = 100f;

  public Animator animator;

  public float moveThreshold = 0.1f;
  public float detectionRadius = 10f;

  public float speed = 1f;

  public bool isDead = false;

  public LayerMask collisionMask;

  Rigidbody2D rb;

  void Start()
  {
    playerManager = FindObjectOfType<GameManager>();

    controller = GetComponent<CharacterController2D>();

    rb = GetComponent<Rigidbody2D>();

    distance = (playerManager.player.transform.position - transform.position);
  }

  void Update()
  {
    if (playerManager.player != null)
    {
      distance = (playerManager.player.transform.position - transform.position);
    }
  }

  void FixedUpdate()
  {
    if(distance.magnitude < detectionRadius && distance.magnitude > moveThreshold)
    {
      RaycastHit2D hit = Physics2D.Raycast(transform.position, distance.normalized, detectionRadius, collisionMask);

      Debug.DrawRay(transform.position, distance.normalized * detectionRadius, Color.red);

      if(hit.collider != null)
      {
        if(hit.collider.gameObject.tag == "Player")
        {
          animator.SetFloat("speed", Mathf.Abs(distance.normalized.x));
          controller.Move(distance.normalized.x * speed * Time.fixedDeltaTime, false, false);
        }
        else
        {
          animator.SetFloat("speed", 0);
          controller.Move(0, false, false);
        }
      }
      else
      {
        animator.SetFloat("speed", 0);
        controller.Move(0, false, false);
      }
    }
    else
    {
      animator.SetFloat("speed", 0);
      controller.Move(0, false, false);
    }

    if(isDead)
    {
      animator.SetBool("isDead", true);
      if(animator.GetCurrentAnimatorStateInfo(0).IsName("death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
      {
        Destroy(gameObject, 30f);
        Destroy(this);
        Destroy(GetComponent<Collider2D>());
        Destroy(rb);
        Destroy(controller);
      }
    }
  }

  public void TakeDamage(float damage)
  {
    maxHealth -= damage;
    if(maxHealth <= 0)
    {
      isDead = true;
    }
  }
}
