using UnityEngine;
// for animation
using DG.Tweening;
public class CarrotGameObject : MonoBehaviour
{
    [SerializeField]
    AudioSource riseAudioSource;

    [SerializeField]
    AudioSource growAudioSource;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("carrot game object created");
        StartGrowAnimationSequence();
    }

    // The carrot has been grabbed.
    public void Grabbed()
    {
        gameManager.PlayAudio(GameAudio.Pop);
    }

    void StartGrowAnimationSequence()
    {
        riseAudioSource.Play();
        // Note: the trail renderer needs to be disabaled for this to work!
        var position = gameObject.transform.position;
        gameObject.transform.position = new Vector3(position.x, position.y - 0.1f, position.z);
         gameObject.transform.DOMove(new Vector3(position.x, position.y, position.z), 0.5f)
            .onComplete = StartAnimationSequence2;
    }

    void StartAnimationSequence2() 
    {
        growAudioSource.Play();
        transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 2, 10, 1f);
    }

    public void AnimateRemove()
    {
        Debug.Log("Animate object to go off screen");
        // audio
        gameManager.PlayAudio(GameAudio.Rocket);
        var position = gameObject.transform.position;

        gameObject.transform.DOMove(new Vector3(position.x, 20, position.z), 3)
            .onComplete = MovementFinished;

    }

    private void MovementFinished()
    {
        gameManager.RemoveCarrotFromScene(this);
    }

    public void ActivateTrail() 
    {
        gameObject.GetComponent<TrailRenderer>().emitting = true;
    }
}
