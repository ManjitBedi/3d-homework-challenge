using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

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
            SpawnCarrot(i);
        }
    }

    private void SpawnCarrot(int index)
    {
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


    // Coroutine method that waits for the delay, then performs the action
    private IEnumerator SpawnCarrotAfterDelay(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("spawn a new carrot game objects");
        SpawnCarrot(index);
    }

    // Audio handling
    public void PlayAudio(GameAudio gameAudio) 
    {
        audioManager.PlayAudio(gameAudio);
    }
}
