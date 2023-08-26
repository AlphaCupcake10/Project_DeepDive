using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootfrom;
    public GunConfig Main;

    void Update()
    {
        for(int i = 0 ; i < Main.FireModes.Length ; i ++)
            CheckShoot(Main,i);
    }

    void CheckShoot(GunConfig Gun,int mode)
    {
        if(Gun.FireModes[mode].CurrentTimer >= Gun.FireModes[mode].FireTimer)
        {
            if (Input.GetMouseButton(mode))
            {
                Shoot(Gun,mode);
                Gun.FireModes[mode].CurrentTimer = 0;
            }
        }
        else
            Gun.FireModes[mode].CurrentTimer += Time.deltaTime;
    }

    void Shoot(GunConfig Gun,int mode)
    {   
        GameObject bulletPrefab = Main.FireModes[mode].CustomBullet;
        if(bulletPrefab == null)bulletPrefab = bullet;
        GameObject newBullet = Instantiate(bulletPrefab, shootfrom.transform.position, Quaternion.identity);

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

    public void SetGun(GunConfig gun)
    {
        Main = gun;
    }
    public GunConfig GetGun()
    {
        return Main;
    }

}
