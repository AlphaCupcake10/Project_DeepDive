using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NPC Dialogue")]
public class Dialogue : ScriptableObject
{
    public string Name;
    public string[] Sentences;
    public bool isSitting = false;
}