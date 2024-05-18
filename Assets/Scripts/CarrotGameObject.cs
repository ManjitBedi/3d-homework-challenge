using UnityEngine;
// for animation
using PrimeTween;
public class CarrotGameObject : MonoBehaviour
{
     [SerializeField]
     AudioSource popAudioSource;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("carrot game object created");
    }

    void Awake() 
    {
        StartGrowAnimationSequence();        
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

    void StartGrowAnimationSequence()
    {
        // TODO: fix me, does not work - the cloned object gets deleted.
        Tween.Scale(transform, new Vector3(1.15f, 0.9f, 1.15f), 0.2f, Ease.OutSine, 30, CycleMode.Yoyo);
    }

    public void AnimateRemove()
    {
        Debug.Log("Animate object to go off screen");
        // audio
        gameManager.PlayAudio(GameAudio.Rocket);

        // animation
        var position = gameObject.transform.position;
        Tween.Position(transform, new Vector3(position.x, 20, position.z), duration: 1, ease: Ease.InOutSine);
    }

    private void Destroy() 
    {
        Debug.Log("destroy carrot game object");
        gameManager.RemoveCarrotFromScene(this);
    }
}
