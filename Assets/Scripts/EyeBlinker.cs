using System.Collections.Generic;
using Mediapipe.Tasks.Components.Containers;
using UnityEngine;

public class EyeBlinker : MonoBehaviour
{
    
    public static EyeBlinker Instance;

   
    [Range(0f, 0.05f)]
    public float blinkThreshold = 0.03f;

    public bool IsEyeClosed { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

   
    public void CheckBlink(NormalizedLandmarks landmarks)
    {

      
        if ( landmarks.landmarks == null || landmarks.landmarks.Count == 0) return;

        var list = landmarks.landmarks;

        
        Vector3 leftUpper = new Vector3(list[159].x, list[159].y, list[159].z);
        Vector3 leftLower = new Vector3(list[145].x, list[145].y, list[145].z);

      
        Vector3 rightUpper = new Vector3(list[386].x, list[386].y, list[386].z);
        Vector3 rightLower = new Vector3(list[374].x, list[374].y, list[374].z);

       
        float leftEyeDist = Vector3.Distance(leftUpper, leftLower);
        float rightEyeDist = Vector3.Distance(rightUpper, rightLower);
        Debug.Log($"Left: {leftEyeDist} Right: {rightEyeDist}");

        if (leftEyeDist < blinkThreshold || rightEyeDist < blinkThreshold)
        {
            IsEyeClosed = true;
        }
        else
        {
            IsEyeClosed = false;
        }
    }
}