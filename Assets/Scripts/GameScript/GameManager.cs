using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    int score;
    float forwardSpeed = 5f;
    public static GameManager inst;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject  youWinText;
    [SerializeField] GameObject youLoosText;
    [SerializeField] GameObject editMenu;
    bool gameComplete = true;
    [SerializeField] PlayerController playerController;

    public int ShowScore()
    {
        return score;
    }

    public void IncrementScore()
    {
        score++;
        FindObjectOfType<AudioManager>().PlaySound("Ball");
        scoreText.text = "SCORE: " + score;

    }
    public void DeleteScore()
    {
        int i = 5;
        score = score -i;
        scoreText.text = "SCORE: " + score;

    }


    public void PlayerZeroSpeed(bool Item)
    {
        playerController.forwardSpeed = 0;
    }

    public void Win()
    {
        youWinText.SetActive(true);
       
        Invoke("NextScene", 3);

        PlayerPrefs.SetInt("GameComplete", gameComplete ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void YouLoose()
    {
        youLoosText.SetActive(true);
        PlayerPrefs.SetInt("GameComplete", gameComplete ? 1 : 0);
        PlayerPrefs.Save();
        Invoke("NextScene" , 3);
    }

    private void Awake()
    {
        inst = this;
    }

    public void EditMenuShov ()
    {
        editMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void NextScene()
    {
        SceneManager.LoadScene(2);
    }

   

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}