using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dailogue_trig : MonoBehaviour
{
    // Start is called before the first frame update
    public obj_dailogue Dailogue;
    public void trig_dia(){
        FindAnyObjectByType<dialogue_manager>().startdialogue(Dailogue);
    }
}
