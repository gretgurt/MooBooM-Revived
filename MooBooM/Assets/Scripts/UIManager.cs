using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject optionsPanel;    //temp private because don't have an options panel yet
    private GameObject endOfLevelCanvas;
    private GameObject levelSelectCanvas;
    private GameController gameController;
    [SerializeField] private Button playNextButton;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        levelSelectCanvas = GameObject.Find("LevelSelectCanvas");
        levelSelectCanvas.SetActive(false);
        endOfLevelCanvas = GameObject.Find("EndOfLevelCanvas");
        disableEndOfLevelCanvas();
    }

    public void OptionsPanel()
    {
        Time.timeScale = 0;
        optionsPanel.SetActive(true);
    }

    public void ReturnToGame()
    {
        Time.timeScale = 1;
        optionsPanel.SetActive(false);
    }

    public void EndOfLevel()
    {
        endOfLevelCanvas.SetActive(true);
        DisplayRightAmountOfStars();

        if (!gameController.isLevelWon())
        {
            DisablePlayNextButton();
        }
    }
    
    public void openLevelSelect() {
        levelSelectCanvas.SetActive(true);
    }

    public void closeLevelSelect() { 
        levelSelectCanvas.SetActive(false);
    }

    public void disableEndOfLevelCanvas() {
        if(endOfLevelCanvas != null)    //In overworld there is not endOfLevelCanvas, so need this check here
            endOfLevelCanvas.SetActive(false);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DisplayRightAmountOfStars() {
        int starsEarned = gameController.getStarsCount();

        if (gameController.isLevelWon())
        {
            for (int i = 1; i <= starsEarned; i++)
            {
                endOfLevelCanvas.transform.GetChild(0).Find("Star" + i).gameObject.SetActive(true);
            }
        }
    }

    private void DisablePlayNextButton() {
            playNextButton.interactable = false;
    }
}
