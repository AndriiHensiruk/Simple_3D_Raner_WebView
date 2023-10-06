using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPuck : MonoBehaviour
{
    [SerializeField] float shockForse = 90f;
    [SerializeField] Transform target;
    [SerializeField] float turnSpeed = 30f;
    private void Shoot()
    {
        Vector3 Shoot = (target.position - this.transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(Shoot * shockForse, ForceMode.Impulse);
    }

    private void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        
            if (SwipeController.swipeUp)
            {
                Shoot();
                Destroy(gameObject, 5f);
             }
        
    }
}
