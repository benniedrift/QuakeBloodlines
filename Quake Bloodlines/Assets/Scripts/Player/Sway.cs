using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sway : MonoBehaviourPunCallbacks
{
    #region Variables

    [SerializeField]
    private float intensity;
    [SerializeField]
    private float smooth;

    private Quaternion origin_Rotation;

    #endregion

    #region Monobehaviour Callbacks

    private void Start()
    {
        origin_Rotation = transform.localRotation;
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        UpdateSway();
    }

    #endregion

    #region Private Methods

    private void UpdateSway()
    {
        //controls
        float temp_xMouse = Input.GetAxis("Mouse X");
        float temp_yMouse = Input.GetAxis("Mouse Y");

        //calc target rotation
        Quaternion temp_xAdjustment = Quaternion.AngleAxis(-intensity * temp_xMouse, Vector3.up);
        Quaternion temp_yAdjustment = Quaternion.AngleAxis(intensity * temp_yMouse, Vector3.right);
        Quaternion target_rotation = origin_Rotation * temp_xAdjustment * temp_yAdjustment;

        //rotate to trget
        transform.rotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);
    }

    #endregion
}
