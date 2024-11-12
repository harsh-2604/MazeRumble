using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    public static PlayerListing instance;
    public TMP_Text playerListingText;
    public Player Player {  get;  set; }
    public Image playerImageColor;

    private void Start()
    {
        SetPlayerColor(Player);
    }
    private void Awake()
    {
        instance = this;
    }
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        playerListingText.text = player.NickName; 
    }

    public void SetPlayerColor(Player player)
    {
        if (player.CustomProperties.ContainsKey("color"))
        {
            Vector3 colorVector = (Vector3)player.CustomProperties["color"];
            Color playerColor = new Color(colorVector.x, colorVector.y, colorVector.z);

            if (playerImageColor != null)
            {
                playerImageColor.color = playerColor;
            }
        }

        else
        {
            if(playerImageColor == null)
            {
                Debug.LogWarning("Color not set for " + player.NickName);
            }

        }
    }
}
