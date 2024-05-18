using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null && transform.position.y < -0.5)
        {
            Destroy(gameObject);
        }
    }
}
