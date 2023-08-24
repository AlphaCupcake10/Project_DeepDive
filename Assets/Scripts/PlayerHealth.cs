using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  public float maxHealth = 100f;

  public float currentHealth;

  void Start()
  {
    currentHealth = maxHealth;
  }

  public void TakeDamage(float damage)
  {
    currentHealth -= damage;
    if(currentHealth <= 0)
    {
      Die();
    }
  }

  void Die()
  {
    print("You died!");
  }
}
