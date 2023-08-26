using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
  public float maxHealth = 100f;

  public float currentHealth;
  public float KnockBackForce = 100;
  Rigidbody2D rb;
  void Start()
  {
    currentHealth = maxHealth;
    rb = GetComponent<Rigidbody2D>();
  }

  public void TakeDamage(float damage,Vector3 right)
  {
    currentHealth -= damage;

    rb.AddForce(KnockBackForce*(Vector3.up+right));

    if(currentHealth <= 0)
    {
      Die();
    }
  }

  public void Die()
  {
    GameManager.Instance.HandleDeath();
    currentHealth = maxHealth;
  }

  public float GetHealthRatio()
  {
    return currentHealth/maxHealth;
  }

}
