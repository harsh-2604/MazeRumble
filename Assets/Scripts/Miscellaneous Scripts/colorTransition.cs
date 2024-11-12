using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorTransition : MonoBehaviour
{
    public Material material;
    public Color[] color;
    public float time;

    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetPoint;

    void Update()
    {
        Transition();
    }

    void Transition()
    {
        targetPoint += Time.deltaTime/time;
        material.color = Color.Lerp(color[currentColorIndex], color[targetColorIndex], targetPoint);
        if(targetPoint >= 1f)
        {
            targetPoint = 0f;
            currentColorIndex = targetColorIndex;
            targetColorIndex++;
            if(targetColorIndex == color.Length) targetColorIndex = 0;
        }
    }
}
