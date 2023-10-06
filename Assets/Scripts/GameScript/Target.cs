using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float speed;
    public float distance;
    private bool moovingRight = true;
    public Transform goalDetection;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        RaycastHit2D goalInfo = Physics2D.Raycast(goalDetection.position, Vector3.down, distance);
        if (goalInfo.collider == false)
        {
            if (moovingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moovingRight = true;
            }

        }
    }
}
