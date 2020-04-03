using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    #region Variables

    public string name;
    public int damage;
    public float fireRate;
    public float bloom;
    public float recoil;
    public float kickback;
    public GameObject prefab;
    public int ammo;
    public int clipsize;
    public float reloadTime;

    //current ammo in clip
    private int clip;
    //total ammo on player
    private int stach;

    #endregion

    #region Private Methods

    internal void Initialize()
    {
        stach = ammo;
        clip = clipsize;
    }

    internal bool FireBullet()
    {
        if(clip > 0)
        {
            clip -= 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    internal void Reload()
    {
        stach += clip;
        clip = Mathf.Min(clipsize, stach);
        stach -= clip;
    }

    internal int GetStach()
    {
        return stach;
    }

    internal int GetClip()
    {
        return clip;
    }

    #endregion
}
