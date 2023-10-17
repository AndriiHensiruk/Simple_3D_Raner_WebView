using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public Button playGame;
    public Button webView;
    bool gameComplete;

    void Start()
    {
        gameComplete = PlayerPrefs.GetInt("GameComplete") == 1 ? false : true;
        if (gameComplete)
        {
            playGame.gameObject.SetActive(true);
            webView.gameObject.SetActive(false);
        }
        else
        {
            playGame.gameObject.SetActive(false);
            webView.gameObject.SetActive(true);

        }

    }
    public void LoadTo(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
