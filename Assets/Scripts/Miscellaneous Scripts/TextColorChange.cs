using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class TextColorChange : MonoBehaviour
{
    #region Public Components
    public TMP_Text text;
    public Color[] color;
    public float time;
    public float transitionDuration = 1f;
    #endregion

    #region private Components
    private int currerntColorIndex = 0;
    #endregion
    void Start()
    {
        if (color.Length > 0 && text != null) StartCoroutine(ChangeColor());   
    }

   IEnumerator ChangeColor()
    {
        while (true)
        {
            Color startColor = color[currerntColorIndex];
            int nextColorIndex = (currerntColorIndex + 1) % color.Length;
            Color targetColor = color[nextColorIndex];
            currerntColorIndex = nextColorIndex;

            float elapsedTime = 0f;

            while(elapsedTime < transitionDuration)
            {
                text.color = Color.Lerp(targetColor, startColor, elapsedTime/transitionDuration); 
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            text.color = targetColor;
            yield return new WaitForSeconds(time);
        }
    }
}
