using UnityEngine;
using System.Collections;


/// <summary>
/// The Game Manager class to deal with state & spawn game objects
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    CarrotGameObject carrotPrefab;

    [SerializeField]
    GameObject[] spawnPoints;

    [SerializeField]
    AudioManager audioManager;

    // Collection of carrots in the scene.
    // TODO: there are not so many game objects if there were, use object pooling.
    CarrotGameObject[] spawnedObjects;

    [SerializeField]
    private Camera gameCamera; 

    public CarrotGameObject selectedCarrot;

    private   QueueManager methodQueue; 
    

    // Start is called before the first frame update
    void Start()
    {
        methodQueue = gameObject.AddComponent<QueueManager>();
        SetupGameScene();
    }
 
    void SetupGameScene() 
    {
        spawnedObjects = new CarrotGameObject[spawnPoints.Length];
        for(int index = 0; index < spawnPoints.Length; index++)
        {
            methodQueue.AddToQueue(SpawnCarrotAfterDelay(0.5f, index));
        }
    }
    
    // use a coroutine to add a delay
    private IEnumerator SpawnCarrotAfterDelay(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        SpawnCarrot(index);
    }

    // Spawn a carrot game object from a prefab
    private void SpawnCarrot(int index)
    {
        // Position game object at spawn point.
        var position = spawnPoints[index].transform.position;
        var newCarrotGameObject = Instantiate(carrotPrefab, position, Quaternion.identity);
        spawnedObjects[index] = newCarrotGameObject;
        // Important set the game manager property on the spawned carrot.
        // TODO: use closures perhaps?
        newCarrotGameObject.GetComponent<DragAndDrop>().gameManager = this;  
        newCarrotGameObject.GetComponent<CarrotGameObject>().gameManager = this;
    }

    public void RemoveCarrotFromScene(CarrotGameObject carrotGameObject, bool spawnNewCarrot = true) 
    {
        // Remove reference from the array of game obejcts 
        // TODO: maybe use a set collection?
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            var spawnedObject = spawnedObjects[i];
            if (carrotGameObject  == spawnedObject) 
            {
                Debug.Log("destroying carrot instance.");
                selectedCarrot = null;
                spawnedObjects[i] = null;
                Destroy(carrotGameObject.gameObject);
                if (spawnNewCarrot)
                {
                    float delay = 2.0f; // Delay in seconds
                    StartCoroutine(SpawnCarrotAfterDelay(delay, i));
                }
            }
        }
    }


 

    // Audio handling
    public void PlayAudio(GameAudio gameAudio) 
    {
        audioManager.PlayAudio(gameAudio);
    }
}
