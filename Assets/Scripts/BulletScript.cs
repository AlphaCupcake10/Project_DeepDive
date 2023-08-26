using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{  
    public GameObject Particle;
    float Damage = 10;
    float NormalForce = 10;
    public GameObject Trail;
    private void OnCollisionEnter2D(Collision2D col)
    {
        ContactPoint2D contact = col.contacts[0];
        Enemy enemy = col.collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(Damage);
        }

        Rigidbody2D rb = col.collider.GetComponent<Rigidbody2D>();

        if(rb != null)
        {
            rb.AddForce(contact.normal * -NormalForce);
        }

        if(Particle)
        {
            GameObject particle = Instantiate(Particle);
            particle.transform.position = contact.point;
            particle.transform.up = contact.normal;
            Destroy(particle,5);
        }
        DestroyBullet();
    }
    public void DestroyBullet(float time)
    {
        Invoke("DestroyBullet",time);
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
        if(Trail)Trail.transform.SetParent(Trail.transform.parent.parent);
        Destroy(Trail,10);
        Trail = null;
    }
    public void Set(float damage,float normalForce)
    {
        Damage = damage;
        NormalForce = normalForce;
    }
}
