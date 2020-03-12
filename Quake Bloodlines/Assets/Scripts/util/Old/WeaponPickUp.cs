using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField]
    private GameObject playerWeapon;
    [SerializeField]
    private GameObject spawnWeapon;

    private void Start()
    {
        playerWeapon.SetActive(false);
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if(_collider.gameObject.tag == "Player")
        {
            playerWeapon.SetActive(true);
            spawnWeapon.SetActive(false);
        }
    }
}
