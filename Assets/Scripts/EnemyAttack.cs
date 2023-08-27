using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
  Melee,
  Ranged
}

public class EnemyAttack : MonoBehaviour
{
  Enemy enemy;
  public float attackSpeed = 1f;
  float lastAttackTime = 0f;
  public Animator animator;

  public DamageType damageType;

  public AttackType[] attackTypes;

  public float damagePerHit = 10f;

  public GameObject bullet;

  GameObject player;

  public float bulletSpeed = 15f;

  public GameObject shootPos;
  bool isAttacked = false;

  void Start()
  {
    enemy = GetComponent<Enemy>();

    player = FindObjectOfType<GameManager>().player;
  }

  void FixedUpdate()
  {

    if(damageType == DamageType.Ranged)
    {
      RaycastHit2D hit = Physics2D.Raycast(shootPos.transform.position, (player.transform.position - shootPos.transform.position).normalized, enemy.detectionRadius, enemy.collisionMask);
      bool canSeePlayer = false;
      if(hit.collider != null && hit.collider.gameObject == player)
      {
        Debug.DrawRay(shootPos.transform.position, enemy.distance.normalized * enemy.detectionRadius, Color.red);
        canSeePlayer = true;
      }
      else
      {
        Debug.DrawRay(shootPos.transform.position, enemy.distance.normalized * enemy.detectionRadius, Color.green);
      }
      if(Mathf.Abs(enemy.distance.x) < enemy.detectionRadius && canSeePlayer)
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
            if(animator.GetCurrentAnimatorStateInfo(0).IsName(attackType.ClipName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f && !isAttacked)
            {
              isAttacked = true;
              ApplyDamage();
              GameObject projectile = Instantiate(bullet, shootPos.transform.position - (Vector3)(enemy.distance.normalized)*bulletSpeed*Time.fixedDeltaTime, Quaternion.identity);

              Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

              if (projectileRigidbody != null)
              {
                Vector2 direction = ((Vector2)enemy.distance.normalized);
                projectileRigidbody.velocity = direction * bulletSpeed;
              }
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName(attackType.ClipName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
              animator.SetBool(attackType.StateName, false);
              isAttacked = false;
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
    else
    {

    if(Mathf.Abs(enemy.distance.x) < enemy.moveThreshold)
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
          if(animator.GetCurrentAnimatorStateInfo(0).IsName(attackType.ClipName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f && !isAttacked)
          {
            isAttacked = true;
            ApplyDamage();
          }
          if(animator.GetCurrentAnimatorStateInfo(0).IsName(attackType.ClipName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
          {
            isAttacked = false;
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
  }

  void ApplyDamage()
  {
    // player.GetComponent<PlayerHealth>().TakeDamage(damagePerHit);
    player.GetComponent<PlayerHealth>().TakeDamage(damagePerHit,transform.right*transform.localScale.x);
  }
}
