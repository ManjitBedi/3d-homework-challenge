using UnityEngine;

// for animation
using DG.Tweening;

/// <summary>
/// This glass deals with the audio & visual aspect of the carrot game object.
/// </summary>
public class CarrotGameObject : MonoBehaviour
{
    [SerializeField]
    AudioSource riseAudioSource;

    [SerializeField]
    AudioSource growAudioSource;

    [SerializeField]
    GameObject [] leaves;

    [SerializeField]
    GameObject smokeVFX;

    [SerializeField]
    bool animateLeaves = false;

    [SerializeField]
    float growDuration = 2.0f;

    [SerializeField]
    float height = 2.5f;

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

        if (animateLeaves)
        {
            HideLeaves();
        }
    }

    void HideLeaves()
    {
        foreach(GameObject leaf in leaves)
        {
            leaf.SetActive(false);
        }
    }

    void StartAnimationSequence2() 
    {
        growAudioSource.Play();
        transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), growDuration, 10, 1f);

        if (animateLeaves)
        {
            GrowLeaves();
        }
    }

    void GrowLeaves()
    {
        foreach(GameObject leaf in leaves)
        {
            leaf.SetActive(true);
            leaf.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            leaf.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), growDuration);
        }
    }

    public void AnimateRemove()
    {
        Debug.Log("Animate object to go off screen");
        // audio
        gameManager.PlayAudio(GameAudio.Rocket);
        var position = gameObject.transform.position;

        gameObject.transform.DOMove(new Vector3(position.x, position.y + height, position.z), 1.1f)
            .onComplete = MovementFinished;
    }

    private void MovementFinished()
    {
        smokeVFX.SetActive(true);
        smokeVFX.transform.parent = null;
        gameManager.PlayAudio(GameAudio.Poof);
        gameManager.RemoveCarrotFromScene(this);
        if (animateLeaves)
        {
            DropLeaves();
        }
    }

    // Animate leaves fall down
    void DropLeaves()
    {
        foreach(GameObject leaf in leaves)
        {
            // remove leaf from parent
            leaf.transform.parent = null;
            var position = leaf.transform.position;

            position.x += Random.Range(-3f, 3f);
            position.z += Random.Range(-3f, 3f);
            // animate leaf falling
            Sequence s = DOTween.Sequence();
            s.Append(leaf.transform.DOMove(new Vector3(position.x, -height, position.z), 12));
            var angle = 90f;
            var rotation = new Vector3(Random.Range(-angle, angle), Random.Range(-angle, angle), Random.Range(-angle, angle));
            s.Join(leaf.transform.DORotate(rotation, 12));
        }
    }

    public void ActivateTrail() 
    {
        gameObject.GetComponent<TrailRenderer>().emitting = true;
    }
}
