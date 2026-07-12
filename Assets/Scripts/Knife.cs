using UnityEngine;
using UnityEngine.InputSystem;

public class Knife : MonoBehaviour
{
    public float flySpeed;
    public bool isFlying = false;
    public bool isOnCircle = false;

    public Transform targetCircle;
    public float collisionDistance; 

    private void Start()
    { 
        if (targetCircle == null)
        {
            GameObject circleObj = GameObject.Find("Circle");
            if (circleObj != null) targetCircle = circleObj.transform;
        }
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.isPressed)
        {
            Application.targetFrameRate = 3;
        }
        if (Keyboard.current.wKey.wasPressedThisFrame && !isFlying && !isOnCircle)
        {
            isFlying = true;
        }

        if (isFlying && !isOnCircle && targetCircle != null)
        {
            float step = flySpeed * Time.deltaTime;
            Vector3 nextPosition = transform.position + (Vector3.up * step);

            float targetHitY = targetCircle.position.y - collisionDistance;

            if (nextPosition.y >= targetHitY)
            {
        
                SpriteRenderer knifeSprite = GetComponent<SpriteRenderer>();
                float knifeWidth = 0.3f;
                if (knifeSprite != null && knifeSprite.sprite != null)
                {
                    knifeWidth = knifeSprite.sprite.bounds.size.x * transform.lossyScale.x;
                }

              
                float dynamicAngleThreshold = (knifeWidth / collisionDistance) * Mathf.Rad2Deg;
                
            
                dynamicAngleThreshold *= 1.15f; 

                Knife[] allKnives = Object.FindObjectsByType<Knife>(FindObjectsSortMode.None);
                foreach (Knife otherKnife in allKnives)
                {
                    if (otherKnife != this && otherKnife.isOnCircle)
                    {
                        float otherAngle = otherKnife.transform.localEulerAngles.z;
                        if (otherAngle > 180) otherAngle -= 360f; 

                      
                        if (Mathf.Abs(otherAngle) < dynamicAngleThreshold) 
                        {
                            transform.position = new Vector3(targetCircle.position.x, targetHitY, transform.position.z);
                            isFlying = false;
                            GetComponent<SpriteRenderer>().color = Color.red;

                            Circle circleScript = targetCircle.GetComponent<Circle>();
                            if (circleScript != null) circleScript.isRotating = false;
                            return;
                        }
                    }
                }

                transform.position = new Vector3(targetCircle.position.x, targetHitY, transform.position.z);
                onCircle();
            }
            else
            {
                transform.position = nextPosition;
            }
        }
    }

    void onCircle()
    {
        isFlying = false;
        isOnCircle = true;

        Vector3 perfectSnapPosition = new Vector3(targetCircle.position.x, targetCircle.position.y - collisionDistance, transform.position.z);
        transform.position = perfectSnapPosition;
        transform.rotation = Quaternion.identity;

        transform.SetParent(targetCircle, true);

       
        KnifeSpawn spawner = Object.FindFirstObjectByType<KnifeSpawn>();
        if (spawner != null)
        {
            spawner.SpawnKnife();
        }
    }
}