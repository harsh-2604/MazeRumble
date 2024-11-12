using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public static float timer = 0f;
    public TMP_Text timerText;
    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = Mathf.FloorToInt(timer).ToString();
        PlayerPrefs.SetFloat("TimeTaken", timer);
    }
}