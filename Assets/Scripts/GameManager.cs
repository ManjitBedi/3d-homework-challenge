using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The Game Manager class to deal with state & spawn game objects
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    CarrotGameObject carrotPrefab;

    [SerializeField]
    GameObject[] spawnPoints;

    // state info
    CarrotGameObject[] spawnedObjects;

    [SerializeField]
    private Camera gameCamera; 

    Ray lastRay;

    public CarrotGameObject selectedCarrot;

    bool isDragging = false;

    void Awake() 
    {
    }

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
            var newCarrotGameObject = Instantiate(carrotPrefab, position, Quaternion.identity);
            spawnedObjects[i] = newCarrotGameObject;
            // Important set the game manager property on the spawned carrrot.
            newCarrotGameObject.GetComponent<DragAndDrop>().gameManager = this;  
        }
    }
}
