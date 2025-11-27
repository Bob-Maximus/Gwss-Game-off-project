using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "projectile data", menuName = "Projectile")]
public class ProjectileData : ScriptableObject
{
    public GameObject projectile;
    public string key;
    public float speed;
    public float lifeTime;
}
