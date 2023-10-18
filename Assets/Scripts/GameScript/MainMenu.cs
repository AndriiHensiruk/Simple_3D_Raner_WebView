using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject editMenu;

    [SerializeField] PlayerController playerController;
    [SerializeField] TileManager tileManager;

    public Slider sppedSlider;
    public Slider lengthBasketballCourtSlider;

    [SerializeField] float maxSpeedPlayer;
    [SerializeField] float jumpPowerPlayer;

    //[SerializeField] float lengthBasketballCourt;

    private void Start()
    {
        sppedSlider.value = playerController.forwardSpeed;

        //maxSpeedPlayer = playerController.maxSpeed;
        //jumpPowerPlayer = playerController.jumpHeight;

        lengthBasketballCourtSlider.value = tileManager.basketballCount;
    }

    public void QuitMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        editMenu.SetActive(false);
        Time.timeScale = playerController.forwardSpeed;
    }

    public void SetSpped()
    {
        playerController.forwardSpeed = sppedSlider.value;
    }

    public void SetBasketballCourt()
    {
        tileManager.basketballCount = lengthBasketballCourtSlider.value;
    }
}
