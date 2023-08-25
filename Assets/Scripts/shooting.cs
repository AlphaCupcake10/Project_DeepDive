using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootfrom;
    public float speed = 5f;
    public float destroytime=5f;

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {   
        GameObject newBullet = Instantiate(bullet, shootfrom.transform.position, Quaternion.identity);

        Rigidbody2D bulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = shootfrom.transform.right * speed * (transform.localScale.x);
        }

        Destroy(newBullet, destroytime);
    }
}
