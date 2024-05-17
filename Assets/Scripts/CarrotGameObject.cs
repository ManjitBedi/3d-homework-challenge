using UnityEngine;
using DG.Tweening;

public class CarrotGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateRemove()
    {
        var position = gameObject.transform.position;  
       gameObject.transform.DOMove(new Vector3(position.x, 20, position.z), 3)
            .onComplete = Destroy;
    }

    private void Destroy() 
    {
        Destroy(gameObject);
    }
}
