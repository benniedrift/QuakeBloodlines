using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Motion : MonoBehaviourPunCallbacks
{
    #region Variables

    [SerializeField]
    private float speed;
    private Rigidbody rig;
    [SerializeField]
    private float sprintModifier;
    [SerializeField]
    private Camera normalCam;
    [SerializeField]
    private GameObject cameraParent;
    [SerializeField]
    private Transform weaponParent;

    private float baseFOV;
    private Vector3 weaponParentOrigin;

    [SerializeField]
    private float sprintFOV = 1.25f;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private Transform groundDetector;

    private float movementCounter;
    private float idleCounter;
    private Vector3 targetWeaponBobPosition;

    #endregion

    #region Monobehaviour Callbacks

    private void Start()
    {
        cameraParent.SetActive(photonView.IsMine);

        baseFOV = normalCam.fieldOfView;
        Camera.main.enabled = false;
        rig = GetComponent<Rigidbody>();
        weaponParentOrigin = weaponParent.localPosition;
    }

    private void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        //axis
        float temp_Hmove = Input.GetAxisRaw("Horizontal");
        float temp_Vmove = Input.GetAxisRaw("Vertical");

        //controls
        bool temp_sprint = Input.GetKey(KeyCode.LeftShift);
        bool temp_jump = Input.GetKeyDown(KeyCode.Space);

        //states
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = temp_jump && isGrounded;
        bool isSprinting = temp_sprint && temp_Vmove > 0 && !isJumping && isGrounded;

        //jjumping
        if (isJumping)
        {
            rig.AddForce(Vector3.up * jumpForce);
        }

        //headbob
        if(temp_Hmove == 0 && temp_Vmove ==0)
        {
            HeadBob(idleCounter, 0.025f, 0.025f);
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
        }
        else if(!isSprinting)
        {
            HeadBob(movementCounter, 0.035f, 0.035f);
            movementCounter += Time.deltaTime * 5;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6f);
        }
        else
        {
            HeadBob(movementCounter, 0.05f, 0.08f);
            movementCounter += Time.deltaTime * 7;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        //axis
        float temp_Hmove = Input.GetAxisRaw("Horizontal");
        float temp_Vmove = Input.GetAxisRaw("Vertical");

        //controls
        bool temp_sprint = Input.GetKey(KeyCode.LeftShift);
        bool temp_jump = Input.GetKeyDown(KeyCode.Space);

        //states
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        bool isJumping = temp_jump && isGrounded;
        bool isSprinting = temp_sprint && temp_Vmove > 0 && !isJumping && isGrounded;

        //movement
        Vector3 temp_direction = new Vector3(temp_Hmove, 0, temp_Vmove);
        temp_direction.Normalize();

        float temp_AdjustedSpeed = speed;
        if(isSprinting) temp_AdjustedSpeed *= sprintModifier;

        Vector3 temp_targetVelocity = transform.TransformDirection(temp_direction) * temp_AdjustedSpeed * Time.deltaTime;
        temp_targetVelocity.y = rig.velocity.y;
        rig.velocity = temp_targetVelocity;

        //FOV
        if (isSprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOV, Time.deltaTime * 8f);
        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
        }
    }

    #endregion

    #region Private Methods

    void HeadBob(float p_z, float p_xIntensity, float p_yIntensity)
    {
        targetWeaponBobPosition = weaponParentOrigin = new Vector3 (Mathf.Cos(p_z) * p_xIntensity, Mathf.Sin(p_z * 2) * p_yIntensity, 0);
    }

    #endregion
}