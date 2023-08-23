using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
  Enemy enemy;
  public float attackSpeed = 1f;
  float lastAttackTime = 0f;
  public Animator animator;

  void Start()
  {
    enemy = GetComponent<Enemy>();
  }

  void FixedUpdate()
  {
    if(enemy.distance.magnitude < enemy.moveThreshold)
    {
      bool isKick = Random.Range(0, 2) == 0;
      if(Time.time - lastAttackTime > attackSpeed)
      {
        if(!isKick)
        {
          animator.SetBool("isAttack", true);
        }
        else
        {
          animator.SetBool("isKick", true);
        }
        lastAttackTime = Time.time;
      }
      else 
      {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieAttack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
          animator.SetBool("isAttack", false);
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("ZombieKick") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
          animator.SetBool("isKick", false);
        }
      }
    }
    else
    {
      animator.SetBool("isAttack", false);
      animator.SetBool("isKick", false);
    }
  }

  void ApplyDamage()
  {
    // apply damage to player
  }
}
