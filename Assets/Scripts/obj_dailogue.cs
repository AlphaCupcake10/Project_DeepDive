using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class obj_dailogue : MonoBehaviour
{
    // Start is called before the first frame update
    public string names;

    [TextArea(3,10)]
    public string[] sentences;
}
