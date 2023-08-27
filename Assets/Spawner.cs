using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int count;
    bool infinite = false;

    public float SpawnDelay = 1;
    float CurrentTimer;
    public Vector2 RBForce = Vector2.zero;

    void Start()
    {
        infinite = count == 0;
    }

    void Update()
    {
        CurrentTimer += Time.deltaTime;
        if(CurrentTimer >= SpawnDelay)
        {
            CurrentTimer = 0;
            Spawn();
            if(!infinite)
                count --;
            if(count <= 0 && !infinite)
            {
                Destroy(this);
            }
        }
    }


    void Spawn()
    {
        GameObject Spawned = Instantiate(prefab,transform.position,transform.rotation);
        Spawned.GetComponent<Rigidbody2D>()?.AddForce(RBForce);
    }


}
