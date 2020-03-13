using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody rig;

    private void Start()
    {
        Camera.main.enabled = false;
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float temp_Hmove = Input.GetAxisRaw("Horizontal");
        float temp_Vmove = Input.GetAxisRaw("Vertical");

        Vector3 temp_direction = new Vector3(temp_Hmove, 0, temp_Vmove);
        temp_direction.Normalize();

        rig.velocity = transform.TransformDirection(temp_direction) * speed * Time.deltaTime;
    }
}