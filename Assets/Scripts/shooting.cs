using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootfrom;
    public GunConfig Main;
    public float[] timerMain;

    void Update()
    {
        for(int i = 0 ; i < Main.FireModes.Length ; i ++)
            CheckShoot(Main,ref timerMain[i],i);
    }

    void CheckShoot(GunConfig Gun,ref float timer,int mode)
    {
        if(timer >= Gun.FireModes[mode].FireTimer)
        {
            if (Input.GetMouseButton(mode))
            {
                Shoot(Gun,mode);
                timer = 0;
            }
        }
        else
            timer += Time.deltaTime;
    }

    void Shoot(GunConfig Gun,int mode)
    {   
        GameObject newBullet = Instantiate(bullet, shootfrom.transform.position, Quaternion.identity);

        Rigidbody2D bulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = shootfrom.transform.right * Gun.FireModes[mode].Speed * (transform.localScale.x);
        }
        BulletScript bulletScript = newBullet.GetComponent<BulletScript>();
        if(bulletScript)
        {
            bulletScript.DestroyBullet(Gun.FireModes[mode].DestroyTime);
            bulletScript.Set(Gun.FireModes[mode].Damage,Gun.FireModes[mode].NormalForce);
        }
    }
}
