using UnityEngine;

/// <summary>
/// Class to drag & drop the selected game object using the old input system
/// </summary>
public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePosition;

    CarrotGameObject carrotGameObject;

    public GameManager gameManager;

    private void Start()
    {
        carrotGameObject = gameObject.GetComponent<CarrotGameObject>();
    }

    private Vector3 GetMousePos() 
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    // TODO: implement this using the newer input system

    private void OnMouseDown() 
   {
        mousePosition = Input.mousePosition - GetMousePos();
        gameManager.selectedCarrot = gameObject.GetComponent<CarrotGameObject>();
   }

   private void OnMouseDrag()
   {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        gameManager.selectedCarrot = null;
   }

   private void  OnMouseUp() {
        Debug.Log("carrot released - Do something");
        carrotGameObject.AnimateRemove();
   }
}

