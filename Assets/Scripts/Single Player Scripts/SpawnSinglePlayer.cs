using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSinglePlayer : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        Instantiate(player, this.transform.position, Quaternion.identity);
    }
}
