using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviourPunCallbacks
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
    private float currentCooldown = 0f;

    #endregion

    #region Monobehaviour Callbacks

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //photonView.RPC(RPC string name, RPC traget, first parameter, second parameter etc...);
            photonView.RPC("Equip", RpcTarget.All, 0);
        }

        if(currentWeapon != null)
        {
            if(Input.GetMouseButtonDown(0) && currentCooldown <= 0)
            {
                photonView.RPC("Shoot", RpcTarget.All);
            }

            //weapon position elasticity
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);
            currentWeapon.transform.localRotation = Quaternion.Lerp(currentWeapon.transform.localRotation, Quaternion.identity, Time.deltaTime * 4f);

            //Firerate
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }
        }
    }

    #endregion

    #region Private Methods

    [PunRPC]
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
        temp_newWeapon.GetComponent<Sway>().isMine = photonView.IsMine;

        currentWeapon = temp_newWeapon;
    }

    [PunRPC]
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

            if(photonView.IsMine)
            {
                //if shootign a player
                if(temp_Hit.collider.gameObject.layer == 12)
                {
                    //photonView.RPC("TakeDamage", RpcTarget.All, loadout[currentIndex].damage); - throws error
                    //FIXED by adding the raycast from the gun - used IsMine and damaged the player that was doing the shooting
                    //RPC call for damaging enemy player if hit
                    temp_Hit.collider.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, loadout[currentIndex].damage);
                }
            }
        }

        //gun fx
        currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].kickback;

        //firerate
        currentCooldown = loadout[currentIndex].fireRate;
    }

    [PunRPC]
    private void TakeDamage(int p_damage)
    {
        GetComponent<Motion>().TakeDamage(p_damage);
    }

    #endregion
}
