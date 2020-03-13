using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Gun[] loadout;
    [SerializeField]
    private Transform weaponParent;
    private GameObject currentWeapon;

    #endregion

    #region Monobehaviour Callbacks

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }
    }

    #endregion

    #region Private Methods

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

    #endregion
}
