using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Gun[] loadout;
    [SerializeField]
    private Transform weaponParent;
    private GameObject currentWeapon;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }
    }

    void Equip(int p_ind)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        GameObject temp_newWeapon = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        temp_newWeapon.transform.localPosition = Vector3.zero;
        temp_newWeapon.transform.localEulerAngles = Vector3.zero;

        currentWeapon = temp_newWeapon;
    }
}
