using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefab;
    public int count;
    bool infinite = false;

    public float SpawnDelay = 1;
    float CurrentTimer;
    public Vector2 RBForce = Vector2.zero;

    public bool Absolute = false;

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
      GameObject Spawned = null;
        if(Absolute)
            Spawned = Instantiate(prefab[Random.Range(0, prefab.Length)],transform.position,transform.rotation);
        else
          Spawned = Instantiate(prefab[Random.Range(0, prefab.Length)],transform.position + (Vector3)Random.insideUnitCircle,transform.rotation);
        Spawned.GetComponent<Rigidbody2D>()?.AddForce(RBForce);
    }
}
