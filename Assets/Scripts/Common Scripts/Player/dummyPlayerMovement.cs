using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyPlayerMovement : MonoBehaviour
{
    #region Private Components
    private Rigidbody rb;
    private Vector3 previousAcceleration;
    #endregion

    #region Public Components
    public bool isOnGround;
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (isOnGround) MoveBall();
    }
    private void MoveBall()
    {
        Vector3 acceleration = Input.acceleration;
        Vector3 filteredAcceleration = Vector3.Lerp(previousAcceleration, acceleration, 0.5f);
        previousAcceleration = filteredAcceleration;
        Vector3 movement = new Vector3(-filteredAcceleration.y, 0, filteredAcceleration.x) * setSensitivity.sensitivity;
        rb.AddForce(movement);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isOnGround = true;
    }   
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isOnGround = false;
    }
}


