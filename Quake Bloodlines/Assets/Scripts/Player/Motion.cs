using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody rig;
    [SerializeField]
    private float sprintModifier;
    [SerializeField]
    private Camera normalCam;
    private float baseFOV;
    [SerializeField]
    private float sprintFOV = 1.25f;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private Transform groundDetector;

    private void Start()
    {
        baseFOV = normalCam.fieldOfView;
        Camera.main.enabled = false;
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
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
        if(isJumping)
        {
            rig.AddForce(Vector3.up * jumpForce);
        }

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
}