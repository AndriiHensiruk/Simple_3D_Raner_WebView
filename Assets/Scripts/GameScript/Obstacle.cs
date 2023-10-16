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

        GameManager.inst.DeleteScore();
        FindObjectOfType<AudioManager>().PlaySound("Damage");
        transform.Translate(Vector3.right * 3f * Time.deltaTime);

        if (GameManager.inst.ShowScore() < 0)
        {
            GameManager.inst.YouLoose();
            Destroy(other.gameObject);
        }
        
    }
}