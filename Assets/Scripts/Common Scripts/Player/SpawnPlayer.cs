using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform spawnPos;
    private GameObject playerObj;
    private void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            playerObj = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos.position, spawnPos.rotation);
        }
        Renderer playerRenderer = playerObj.GetComponent<Renderer>();
        SetPlayerPrefabColor(PhotonNetwork.LocalPlayer, playerRenderer);
    }
    public void SetPlayerPrefabColor(Player player, Renderer playerRenderer)
    {
        if (player.CustomProperties.ContainsKey("color"))
        {
            Vector3 colorVector = (Vector3)player.CustomProperties["color"];
            Color playerColor = new Color(colorVector.x, colorVector.y, colorVector.z);
            if (playerRenderer != null)
            {
                playerRenderer.material.color = playerColor;
                Debug.Log("Color set for player: " + player.NickName + "|" + playerColor);
            }
        }

        else
        {
            Debug.LogWarning("Color not set for " + player.NickName);
        }
    }
}