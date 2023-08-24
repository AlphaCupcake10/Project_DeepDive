using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootfrom;
    public Vector3 posi;
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
    {   Vector2 direction = transform.right;

        GameObject newBullet = Instantiate(bullet, shootfrom.transform.position, Quaternion.identity);

        Rigidbody2D bulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = direction.normalized * speed;
        }

        Destroy(newBullet, 0.5f);
    }
    
    
}
