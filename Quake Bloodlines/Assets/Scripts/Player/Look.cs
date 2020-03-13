﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    #region Variables

    public static bool cursorLocked = true;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform cams;
    [SerializeField]
    private Transform weapon;

    private Quaternion camCentre;

    public float xSensitivty;
    public float ySensitivty;
    public float maxAngle;

    #endregion

    #region Monobehaviour Callbacks
    
    private void Start()
    {
        camCentre = cams.localRotation;
    }

    private void Update()
    {
        SetY();
        SetX();

        LockCursor();
    }

    #endregion

    #region Private Methods

    void SetY()
    {
        float temp_Input = Input.GetAxis("Mouse Y") * ySensitivty * Time.deltaTime;
        Quaternion temp_Adjustment = Quaternion.AngleAxis(temp_Input, -Vector3.right);
        Quaternion temp_Delta = cams.localRotation * temp_Adjustment;

        if(Quaternion.Angle(camCentre, temp_Delta) < maxAngle)
        {
            cams.localRotation = temp_Delta;
        }

        weapon.rotation = cams.rotation;
    }

    void SetX()
    {
        float temp_Input = Input.GetAxis("Mouse X") * xSensitivty * Time.deltaTime;
        Quaternion temp_Adjustment = Quaternion.AngleAxis(temp_Input, Vector3.up);
        Quaternion temp_Delta = player.localRotation * temp_Adjustment;

        player.localRotation = temp_Delta;
    }

    void LockCursor()
    {
        if(cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLocked = true;
            }
        }
    }

    #endregion
}
