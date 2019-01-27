using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour
{
    public Camera cam;
    public float speed = 1.0f;
    public Color startC;
    public Color endC;
    float startTime;
    void Start()
    {
        startTime = Time.time;
    }    
    void Update()
    {
        float t = Mathf.Sin(Time.time - startTime) * speed;
        cam.backgroundColor = Color.LerpUnclamped(startC, endC, t);
    }
}
