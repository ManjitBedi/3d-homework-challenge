using UnityEngine;

/// <summary>
/// Class to drag & drop (release) the selected game object using the old input system.
/// Handle when the user grabs a carrot & throws it.
/// If the mouse movement is not too large, just propel the carrot up.
/// Full discloure I used some code form here to save on development time:
/// https://pastebin.com/2JPEYMiw
/// </summary>

// TODO: implement this using the newer input system

public class DragAndDrop : MonoBehaviour
{
     Vector3 mousePosition;

     CarrotGameObject carrotGameObject;

     public GameManager gameManager;

     float startTime, endTime, swipeDistance, swipeTime;
     private Vector2 startPos;
     private Vector2 endPos;

     public float MinSwipDist = 0;
     private float objectVelocity = 0;
     private float objectSpeed = 0;
     public float maxObjectSpeed = 350;
     private Vector3 angle;

     private bool thrown, holding;
     private Vector3 newPosition, resetPos;
     Rigidbody rb;

    private void Start()
    {
          carrotGameObject = gameObject.GetComponent<CarrotGameObject>();

          rb = GetComponent<Rigidbody>();
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

          startTime = Time.time;
          startPos = Input.mousePosition;

          carrotGameObject.ActivateTrail();
     }


     // Update the position of the selected carrot - move it with the mouse.
     private void OnMouseDrag()
     {
          // Need to adjust the mouse position to use in world space.
          transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
     }

     // The carrot has been released, play an animation.
     private void OnMouseUp() 
     {          
          endTime = Time.time;
          endPos = Input.mousePosition;
          swipeDistance = (endPos - startPos).magnitude;
          swipeTime = endTime - startTime;

          if (swipeTime < 0.5f && swipeDistance > 30f)
          {
               //throw the game object
               CalSpeed();
               CalAngle();
               rb.AddForce(new Vector3(angle.x * objectSpeed, angle.y * objectSpeed / 3, angle.z * objectSpeed * 2));
               rb.useGravity = true;
               holding = false;
               thrown = true;
               gameManager.PlayAudio(GameAudio.Throw); 
               Invoke("RemoveObject", 4f);
          }
          else
          {
               carrotGameObject.AnimateRemove();
          }
     }

     private void CalAngle()
     {
          angle = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, Camera.main.nearClipPlane + 5));
     }

    void CalSpeed()
    {
        if (swipeTime > 0)
        {
            objectVelocity = swipeDistance / (swipeDistance - swipeTime);
        }

        objectSpeed = objectVelocity * 40;

        if (objectSpeed <= maxObjectSpeed)
        {
            objectSpeed = maxObjectSpeed;
        }
        swipeTime = 0;
    }

    void RemoveObject() 
    {
          gameManager.RemoveCarrotFromScene(carrotGameObject);
    }
}

