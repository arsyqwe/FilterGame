using UnityEngine;
using UnityEngine.UIElements;

public class ScaleFromAudio : MonoBehaviour
{
    
    public Vector3 minScale;
    public Vector3 maxScale;
    public AudioDetection detector;
    public float loudnessSens = 100;
    public float threshold = 0.1f;
    public float smoothSpeed = 30f;
    void Start()
    {
       
    }

    
    void Update()
    {
        float loudness = detector.GetLoudnessFromMicrpohone()*loudnessSens;
     
        if(loudness < threshold)
        {
            loudness = 0;
        }
        Vector3 targetScale = Vector3.Lerp(minScale, maxScale, loudness);
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, smoothSpeed * Time.deltaTime);
        Debug.Log(smoothSpeed * Time.deltaTime);
        
    }

    
}
