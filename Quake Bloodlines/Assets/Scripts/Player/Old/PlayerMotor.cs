using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 thrusterForce = Vector3.zero;

    private float cameraRotationX = 0f;
    private float currentCameraRotX = 0f;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    [SerializeField]
    private Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //gets a movment vector
    public void Move (Vector3 _velocity)
    {
        velocity = _velocity;
    }
    //gets a roational vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    //get force vector for thruster
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    //run every physics iteration
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //perform movemnt based on movement velocity
    private void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if(thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
    //Perform roations
    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            //BUG -> camera freely rotates with no limits -> FIXED

            //old rotational calculations
            //cam.transform.Rotate(-cameraRotation);
            //new rotational calculations
            currentCameraRotX -= cameraRotationX;
            currentCameraRotX = Mathf.Clamp(currentCameraRotX, -cameraRotationLimit, cameraRotationLimit);
            cam.transform.localEulerAngles = new Vector3(currentCameraRotX, 0f, 0f);
        }
    }
}
