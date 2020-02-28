using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 7f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Joint Options: ")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 30f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        //calculate movement velocity as a 3d vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //final movement vector
        Vector3 _velcoity = (_movHorizontal + _movVertical).normalized * speed;
        
        //apply movement
        motor.Move(_velcoity);

        //calculate rotation as a 3d vector
        float _yRot = Input.GetAxis("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //apply roation
        motor.Rotate(_rotation);

        //calculate camera rotation as a 3d vector
        float _xRot = Input.GetAxis("Mouse Y");
        float _cameraRotationX = _xRot * lookSensitivity;

        //apply camera roation
        motor.RotateCamera(_cameraRotationX);

        //calculate and apply thruster force to player
        Vector3 _thrusterForce = Vector3.zero;

        if(Input.GetButtonDown("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive 
        { 
            mode = jointMode, positionSpring = jointSpring, maximumForce = jointMaxForce
        };
    }
}
