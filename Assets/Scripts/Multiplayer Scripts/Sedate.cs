using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Sedate : MonoBehaviour
{
    [SerializeField] private float timeDuration = 15f;

    [PunRPC]
    public void sedatePlayerRPC()
    {
        //setSensitivity.sensitivity = 10f;
        if(timeDuration > 0)
        {
            timeDuration -= Time.deltaTime;
            Debug.Log(timeDuration);
        }
        else
        {
            timeDuration = 15f;
        }

        if(timeDuration <= 0)
        {
            Debug.Log("Players are sedated");
        }
    }
}
