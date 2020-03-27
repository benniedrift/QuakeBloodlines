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
    [SerializeField]
    private GameObject bulletHolePrefab;
    [SerializeField]
    private LayerMask canBeShot;

    private GameObject currentWeapon;
    private int currentIndex;

    #endregion

    #region Monobehaviour Callbacks

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }

        if(currentWeapon != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

            //weapon position elasticity
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);
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

        currentIndex = p_ind;

        GameObject temp_newWeapon = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        temp_newWeapon.transform.localPosition = Vector3.zero;
        temp_newWeapon.transform.localEulerAngles = Vector3.zero;

        currentWeapon = temp_newWeapon;
    }

    private void Shoot ()
    {
        Transform temp_Spawn = transform.Find("Cameras/Normal Camera");

        //bloom
        Vector3 temp_Bloom = temp_Spawn.position + temp_Spawn.forward * 1000f;
        temp_Bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * temp_Spawn.up;
        temp_Bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * temp_Spawn.right;
        temp_Bloom -= temp_Spawn.position;
        temp_Bloom.Normalize();

        //raycast
        RaycastHit temp_Hit = new RaycastHit();
        if(Physics.Raycast(temp_Spawn.position, temp_Bloom, out temp_Hit, 1000f, canBeShot))
        {
            GameObject temp_NewBulletHole = Instantiate(bulletHolePrefab, temp_Hit.point + temp_Hit.normal * 0.001f, Quaternion.identity) as GameObject;
            temp_NewBulletHole.transform.LookAt(temp_Hit.point + temp_Hit.normal);
            Destroy(temp_NewBulletHole, 5f);
        }

        //gun fx
        currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
        currentWeapon.transform.position -= currentWeapon.transform.position * loadout[currentIndex].kickback;
    }

    #endregion
}
