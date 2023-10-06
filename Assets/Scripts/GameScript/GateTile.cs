using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTile : MonoBehaviour
{
    void Awake()
    {
        GateTile[] controllers = FindObjectsOfType<GateTile>();
       

        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
