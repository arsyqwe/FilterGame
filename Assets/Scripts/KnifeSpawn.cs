using UnityEngine;

public class KnifeSpawn : MonoBehaviour
{
    public GameObject knifePrefab; 
    public Transform spawnPoint;   

    void Start()
    {
        SpawnKnife();
    }

    public void SpawnKnife()
    {
        if (knifePrefab == null) return;

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        
        GameObject newKnifeObj = Instantiate(knifePrefab, pos, Quaternion.identity);
        Knife newKnifeScript = newKnifeObj.GetComponent<Knife>();

        Circle circleScript = Object.FindFirstObjectByType<Circle>();
        if (circleScript != null && newKnifeScript != null)
        {
            circleScript.SetupNewKnife(newKnifeScript);
        }
    }
}