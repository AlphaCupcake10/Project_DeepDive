using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    Rigidbody2D RB;
    public GameObject bullet;
    public GameObject shootfrom;
    public GunConfig Main;
    public SpriteRenderer sprite;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        sprite.sprite = Main.graphic;
    }

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
                for(int i = 0 ; i < Gun.FireModes[mode].Count ; i ++)
                {
                    Shoot(Gun,mode);
                }
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
            Vector2 direction = ((Vector2)shootfrom.transform.right + Random.insideUnitCircle * Gun.FireModes[mode].Spray) * (transform.localScale.x);
            bulletRigidbody.velocity = direction * Gun.FireModes[mode].Speed;
            RB.AddForce(-direction * Gun.FireModes[mode].RecoilForce);
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
        if(CameraZoom.Instance)CameraZoom.Instance.SetZoom(gun.zoom);
        sprite.sprite = gun.graphic;
    }
    public GunConfig GetGun()
    {
        return Main;
    }

}
