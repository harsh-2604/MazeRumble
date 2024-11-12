using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Movement : MonoBehaviour
{
    #region Private Components
    private Rigidbody rb;
    private Vector3 previousAcceleration;
    private GameObject sedateButton;
    private Sedate sedate;
    #endregion

    #region Public Components
    public bool isOnGround;
    public PhotonView pView;
    //public TMP_Text playerName;
    #endregion

    private void Awake()
    {
        sedateButton = GameObject.Find("SedateButton");
        Debug.Log("Button Found: " + sedateButton); 
        if(sedateButton != null ) sedateButton.SetActive(false);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pView = GetComponent<PhotonView>();
        if (pView == null)
        {
            Debug.LogError("PhotonView component is missing!");
            return; 
        }
        Debug.Log("Is this view mine: " + pView.IsMine);
        Debug.Log("My sensitivity is: " + setSensitivity.sensitivity);
    }
    void FixedUpdate()
    {
        if (pView.IsMine && isOnGround) MoveBall();
    }
    private void MoveBall()
    {
        Vector3 acceleration = Input.acceleration; 
        Vector3  filteredAcceleration = Vector3.Lerp(previousAcceleration, acceleration, 0.5f);
        previousAcceleration = filteredAcceleration;
        Vector3 movement = new Vector3(-filteredAcceleration.y, 0, filteredAcceleration.x) * setSensitivity.sensitivity;
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

        if (collision.gameObject.CompareTag("Sedate"))
        {
            Destroy(collision.gameObject);
            sedateButton.SetActive(true);
            Debug.Log("Sedate Button is active");
            Debug.Log("Object destroyed: " +  gameObject.name);
        }
    }

    public void sedatePlayer()
    {
        pView.RPC("sedate.sedatePlayerRPC", RpcTarget.Others);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isOnGround = false;
        if(collision.gameObject.CompareTag("Sedate")) sedateButton.SetActive(false);
    } 
}
