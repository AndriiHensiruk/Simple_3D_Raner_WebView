using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    int score;
    float speed = 5f;
    public static GameManager inst;
    [SerializeField] Text scoreText;

    [SerializeField] PlayerMovement playerMovement;

    public int ShowScore()
    {
        return score;
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;
        // Increase the player's speed
        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }

    public void PlayerZeroSpeed(bool Item)
    {
        playerMovement.speed = 0;
    }

    private void Awake()
    {
        inst = this;
    }

 
}