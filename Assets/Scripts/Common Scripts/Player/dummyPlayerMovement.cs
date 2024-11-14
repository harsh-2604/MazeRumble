using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Debug.Log("My sensitivity is: " + setSensitivity.sensitivity);
    }
    private void MoveBall()
    {
#if UNITY_EDITOR
        Vector3 movement = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")) * setSensitivity.sensitivity;
#else
    Vector3 acceleration = Input.acceleration;
    Vector3 filteredAcceleration = Vector3.Lerp(previousAcceleration, acceleration, 0.5f);
    previousAcceleration = filteredAcceleration;
    Vector3 movement = new Vector3(-filteredAcceleration.y, 0, filteredAcceleration.x) * setSensitivity.sensitivity;
#endif
        rb.AddForce(movement);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isOnGround = true;
        if (collision.gameObject.CompareTag("Game Over"))
        {
            SceneManager.LoadScene("Game Over");
            GameOver.isWon = true;

            /*
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.LogWarning("No more scenes to load.");
            }*/
        }
    }   
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isOnGround = false;
    }
}


