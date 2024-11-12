using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TImeTaken : MonoBehaviour
{
    public TMP_Text timeTakenText;

    private void Start()
    {
        float timeTaken = PlayerPrefs.GetFloat("TimeTaken", 0);
        timeTakenText.text = "Time Taken: " + Mathf.FloorToInt(timeTaken) + " seconds";
    }
}
