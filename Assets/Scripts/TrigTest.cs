using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class TrigTest : MonoBehaviour
{

    public float angleInDegrees;

    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    void Update()
    {
        Vector3 direction = new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        Debug.DrawLine(transform.position, direction * 10, Color.green);

        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(inputDirection * Time.deltaTime * 5, Space.World);
        if (inputDirection != Vector3.zero)
        {
            float inputAngle = 90 - Mathf.Atan2(inputDirection.z, inputDirection.x) * Mathf.Rad2Deg;

            transform.eulerAngles = Vector3.up * inputAngle;
        }

    }
}
