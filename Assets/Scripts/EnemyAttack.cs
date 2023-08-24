using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
  Enemy enemy;
  public float attackSpeed = 1f;
  float lastAttackTime = 0f;
  public Animator animator;

  public AttackType[] attackTypes;

  public float damagePerHit = 10f;

  GameObject player;

  void Start()
  {
    enemy = GetComponent<Enemy>();

    player = FindObjectOfType<GameManager>().player;
  }

  void FixedUpdate()
  {
    if(enemy.distance.magnitude < enemy.moveThreshold)
    {
      int randAttackIdx = Random.Range(0, attackTypes.Length);
      if(Time.time - lastAttackTime > attackSpeed)
      {
        for(int i = 0; i < attackTypes.Length; i++)
        {
          if(i == randAttackIdx)
          {
            animator.SetBool(attackTypes[i].StateName, true);
            break;
          }
        }
        lastAttackTime = Time.time;
      }
      else 
      {
        foreach(AttackType attackType in attackTypes)
        {
          if(animator.GetCurrentAnimatorStateInfo(0).IsName(attackType.ClipName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
          {
            ApplyDamage();
            animator.SetBool(attackType.StateName, false);
          }
        }
      }
    }
    else
    {
      foreach(AttackType attackType in attackTypes)
      {
        animator.SetBool(attackType.StateName, false);
      }
    }
  }

  void ApplyDamage()
  {
    player.GetComponent<PlayerHealth>().TakeDamage(damagePerHit);
  }
}
