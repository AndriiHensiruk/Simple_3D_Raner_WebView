using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check that the object we collided with is the player
        if (other.gameObject.name != "Player")
        {
            return;
        }

        // Add to the player's score
        GameManager.inst.YouLoose();

        // Destroy this Player object
        Destroy(other.gameObject);
    }
}