using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    bool gameComplete = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check that the object we collided with is the player
        if (other.gameObject.name != "Puck")
        {
            return;
        }

        // Add to the player's score
        GameManager.inst.IncrementScore();

        // Destroy this puck object
        Destroy(other.gameObject);

        PlayerPrefs.SetInt("GameComplete", gameComplete ? 1 : 0);
        PlayerPrefs.Save();

        for (int i = 0; i < 5; i++)
        {
            if (i > 5)
            {
                SceneManager.LoadScene(2);
            }
 
        }
    }

}
