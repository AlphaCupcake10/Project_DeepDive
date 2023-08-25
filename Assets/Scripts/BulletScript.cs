using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{  
    public GameObject Particle;
    private void OnCollisionEnter2D(Collision2D col)
    {
        ContactPoint2D contact = col.contacts[0];
        Enemy enemy = col.collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(25f);
        }
        
        GameObject particle = Instantiate(Particle);
        particle.transform.position = contact.point;
        particle.transform.up = contact.normal;
        Destroy(particle,5);
        Destroy(gameObject);
    }
}
