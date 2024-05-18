using UnityEngine;

/// <summary>
/// Class to drag & drop (release) the selected game object using the old input system.
/// When the object is released, play an animation sequence.
/// </summary>

// TODO: implement this using the newer input system

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

     private void OnMouseDown() 
     {
          mousePosition = Input.mousePosition - GetMousePos();
          gameManager.selectedCarrot = gameObject.GetComponent<CarrotGameObject>();
     }

     private void OnMouseDrag()
     {
          // Need to adjust the mouse position to use in world space.
          transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
          gameManager.selectedCarrot = null;
     }


     private void  OnMouseUp() 
     {          
          
          carrotGameObject.AnimateRemove();
     }
}

