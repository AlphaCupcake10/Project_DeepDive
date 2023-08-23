using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  GameManager playerManager;

  CharacterController2D controller;

  public Vector2 distance;


  public Animator animator;

  public float moveThreshold = 0.1f;
  public float detectionRadius = 10f;

  public float speed = 1f;

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
      animator.SetFloat("speed", Mathf.Abs(distance.normalized.x));
      controller.Move(distance.normalized.x * speed * Time.fixedDeltaTime, false, false);
    }
    else
    {
      animator.SetFloat("speed", 0);
      controller.Move(0, false, false);
    }

    
  }
}
