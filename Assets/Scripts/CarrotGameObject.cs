using UnityEngine;
// for animation
using PrimeTween;
using DG.Tweening;
public class CarrotGameObject : MonoBehaviour
{
     [SerializeField]
     AudioSource popAudioSource;

    public GameManager gameManager;

    bool useDOTween = true;

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
        // Note: the trail renderer needs to be disabaled for this to work!

        if (useDOTween)
        {
            transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 2, 10, 1f);
        }
        else
        {
            PrimeTween.Tween.Scale(transform, new Vector3(1.15f, 0.9f, 1.15f), 0.1f, PrimeTween.Ease.OutSine, 5, CycleMode.Yoyo).OnComplete(transform, _transform => {
                Debug.Log("animation completed");
            }, warnIfTargetDestroyed: false);
        }
    }

    public void AnimateRemove()
    {
        Debug.Log("Animate object to go off screen");
        // audio
        gameManager.PlayAudio(GameAudio.Rocket);

        // animation
        var position = gameObject.transform.position;
        PrimeTween.Tween.Position(transform, new Vector3(position.x, 20, position.z), duration: 1, ease: PrimeTween.Ease.InOutSine);
    }


    public void ActivateTrail() 
    {
        gameObject.GetComponent<TrailRenderer>().emitting = true;
    }

    private void Destroy() 
    {
        Debug.Log("destroy carrot game object");
        gameManager.RemoveCarrotFromScene(this);
    }
}
