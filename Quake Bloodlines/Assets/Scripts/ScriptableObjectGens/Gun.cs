using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string name;
    public int damage;
    public float fireRate;
    public float bloom;
    public float recoil;
    public float kickback;
    public GameObject prefab;
}
