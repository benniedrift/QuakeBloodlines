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
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float temp_Hmove = Input.GetAxis("Horizontal");
        float temp_Vmove = Input.GetAxis("Vertical");

        Vector2 temp_direction = new Vector2(temp_Hmove, temp_Vmove);
        temp_direction.Normalize();

        rig.velocity = transform.TransformDirection(temp_direction) * speed * Time.deltaTime;
    }
}
