using UnityEngine.Networking;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField]
    private Camera cam;

    public PlayerWeapons weapon;

    private void Start()
    {
        if(cam == null)
        {

            Debug.LogError("No Cam Referanced +_+ PlayerShoot");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
    }
}
