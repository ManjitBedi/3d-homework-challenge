using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CarrotGameObject carrotPrefab;

    [SerializeField]
    GameObject[] spawnPoints;

    // state info
    CarrotGameObject[] spawnedObjects;

    // Start is called before the first frame update
    void Start()
    {
        SetupGameScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupGameScene() 
    {
        spawnedObjects = new CarrotGameObject[spawnPoints.Length];

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            var position = spawnPoints[i].transform.position;
            spawnedObjects[i] = Object.Instantiate(carrotPrefab, position, Quaternion.identity);
        }
    }
}
