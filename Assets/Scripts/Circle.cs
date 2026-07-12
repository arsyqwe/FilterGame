using UnityEngine;

public class Circle : MonoBehaviour
{
    public float baseSpeed = 100f;
    public float currentSpeed;
    public float changeInterval;
    public float direction = 1f;
    public bool isRotating;

    void Start()
    {
        currentSpeed = baseSpeed;
        getNewInterval();
        isRotating = true;
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(0f, 0f, direction * currentSpeed * Time.deltaTime);
        }
    }

    void getNewInterval()
    {
        changeInterval = Random.Range(1.5f, 3f);
    }


    public void SetupNewKnife(Knife newKnife)
    {
        newKnife.targetCircle = this.transform;

        float circleRadius = 0f;
        float knifeHalfLength = 0f;

        
        SpriteRenderer circleSprite = GetComponent<SpriteRenderer>();
        if (circleSprite != null && circleSprite.sprite != null)
        {
            circleRadius = circleSprite.sprite.bounds.extents.y * transform.lossyScale.y;
        }

      
        SpriteRenderer knifeSprite = newKnife.GetComponent<SpriteRenderer>();
        if (knifeSprite != null && knifeSprite.sprite != null)
        {
            knifeHalfLength = knifeSprite.sprite.bounds.extents.y * newKnife.transform.lossyScale.y;
        }

    
        newKnife.collisionDistance = circleRadius + knifeHalfLength;
    }
}