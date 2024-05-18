using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CarrotGameObject : MonoBehaviour
{
     [SerializeField]
     AudioSource popAudioSource;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The carrot has been grabbed.
    public void Grabbed()
    {
        gameManager.PlayAudio(GameAudio.Pop);
    }

    public void AnimateRemove()
    {
        // audio
        gameManager.PlayAudio(GameAudio.Rocket);

        // animation
        var position = gameObject.transform.position;
        gameObject.transform.DOMove(new Vector3(position.x, 20, position.z), 3)
            .onComplete = Destroy;
    }

    private void Destroy() 
    {
       gameManager.RemoveCarrotFromScene(this);
    }
}
