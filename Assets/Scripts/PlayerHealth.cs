using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
  public Canvas canvas;
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

  public void Die()
  {
    canvas.enabled=true;
    print("You died!");
  }
}
