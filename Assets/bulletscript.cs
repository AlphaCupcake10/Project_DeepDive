using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    
    public float bspeed=50f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 posi=transform.position;
        GetComponent<Rigidbody2D>().AddForce(posi*bspeed);
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy= other.GetComponent<Enemy>();
        if(enemy!=null){
            //enemy damage
            enemy.TakeDamage(25f);
        }
        //particle effect
    }
}
