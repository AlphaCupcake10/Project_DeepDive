using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Gun", order = 1)]
public class GunConfig : ScriptableObject
{
    public GunConfigSingle[] FireModes;
}
[System.Serializable]
public class GunConfigSingle
{
    public float FireTimer = 0;//0 means rapid
    public float Speed = 5f;
    public float DestroyTime=5f;
    public float Damage = 10f;
    public float NormalForce = 25;
}