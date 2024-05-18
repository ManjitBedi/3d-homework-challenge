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


     // Mouse pointer ray cast has deteted a carrot game object.
     // Pick it out of the ground.
     private void OnMouseDown() 
     {
          mousePosition = Input.mousePosition - GetMousePos();
          gameManager.selectedCarrot = carrotGameObject;
          carrotGameObject.Grabbed();
     }


     // Update the position of the selected carrot - move it with the mouse.
     private void OnMouseDrag()
     {
          // Need to adjust the mouse position to use in world space.
          transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
          
     }

     // The carrot has been released, play an animation.
     private void  OnMouseUp() 
     {          
          carrotGameObject.AnimateRemove();
     }
}

