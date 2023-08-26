using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_healthbar : MonoBehaviour
{
    [SerializeField] private Slider healthbar;
    
    public void HealthInBar(float maxHealth,float health){
        healthbar.value=maxHealth/health;
    }
    // Start is called before the first frame update
    
}
