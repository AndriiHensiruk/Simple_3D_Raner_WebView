using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
   private void Update()
    {
        if (SwipeController.swipeRight)
        {
            transform.Translate(Vector3.right * 3f * Time.deltaTime);

        }

        if (SwipeController.swipeLeft)
        {
            transform.Translate(-Vector3.right * 3f * Time.deltaTime);

        }
    }

   
}
