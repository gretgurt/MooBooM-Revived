using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private LevelSelect levelSelect;
    private Gyroscope gyroScope;
    private UIManager uiManager;
    private Cow cow;
    private int explodedBombs;
    private int numberOfStars;
    private int startingNumberOfBombs;
    private int bombsPickedUp;

    private bool mineExploded;
    private bool wonLevel;
    private bool cowTakenDamage;
    private bool gameReady;
    private bool gameOver;

    //If 3 bombs explode the goal is not reached.
    private const int GOAL_NOT_REACHED = 3;

    public List<Bomb> bombList;

    private float timeUntilStart = 1.5f;
    private float startTimer;



    private void Start()
    {
        //LoadProgress();
        gameOver = false;
        gameReady = false;
        wonLevel = false;
        cowTakenDamage = false;
        explodedBombs = 0;
        numberOfStars = 3;
        bombsPickedUp = 0;
        Time.timeScale = 1; //If previous level ended with a slow-motion pickUp this will reset the next level

        levelSelect = FindObjectOfType<LevelSelect>();
        uiManager = FindObjectOfType<UIManager>();
        gyroScope = FindObjectOfType<Gyroscope>();
        cow = FindObjectOfType<Cow>();

        //Finds all bomb-object in the game and adds them to a list
        Bomb[] bombArray = FindObjectsOfType<Bomb>();
        bombList = new List<Bomb>(bombArray);
        startingNumberOfBombs = bombList.Count;

    }

    private void Update()
    {
        startTimer += Time.deltaTime;

        if (!gameReady)
        {
            if (!gyroScope.IsCalibrated())
            {
                gyroScope.IsCalibrated();
            }
            else
            {
                if (startTimer > timeUntilStart) {
                    cow.Release();
                    gameReady = true;
				}
            }
        }
        
    }

    public void BombExploded(Bomb bomb) {
        
        explodedBombs++;
        bombList.Remove(bomb);
        if(!gameOver)
            CheckIfLastBomb();
    }

    public void BombPickedUp(Bomb bomb) {
        bombList.Remove(bomb);
        if (!gameOver)
        {
            bombsPickedUp++;
            CheckIfLastBomb();
        }
    }

    private void CheckIfLastBomb() {
        if (bombList.Count == 0) 
        {
            GameOver();
		}
    }

    public void MineExploded() {
        //If the player has already won (taken all bombs) they can't 'unwin' after enOfLevelCanvas is displayed
        if (!gameOver)  
        {
            mineExploded = true;
            if (!mineExploded && !cowTakenDamage)
            {
                GameOver();
            }
        }
    }

    public void GameOver() {
        //If a mine didn't explode and cow hasn't taken damage and exploded bombs are less than 3
        if (!mineExploded && !cowTakenDamage && explodedBombs < GOAL_NOT_REACHED)
        {
            numberOfStars = numberOfStars - explodedBombs;
        }
        else
        {
            numberOfStars = 0;
        }

        wonLevel = numberOfStars > 0;
        gameOver = true;
        uiManager.EndOfLevel();
        Debug.Log("GAME OVER! -  " + numberOfStars + " *! \nWon?: " + wonLevel);
        SaveProgress();
    }

    public void SaveProgress() {
        SaveManager.SaveLevelStars(this, levelSelect);
    }

    public void LoadProgress() {
        /*return*/
        int[][] chapterLevelStarsList = SaveManager.LoadSaveProgress();
    }

    public void CowTakesDamage() {
        cowTakenDamage = true;
    }

    public int getExplodedBombsCount() {
        return explodedBombs;
    }

    public int getPickedUpBombsCount() {
        return bombsPickedUp;
    }

    public int getStartngBombCount() {
        return startingNumberOfBombs;
    }

    public int getStarsCount() {
        return numberOfStars;
    }

    public bool isLevelWon() {
        return wonLevel;
    }

    public bool isGameReady() {
        return gameReady;
    }

    public bool isGameOver() {
        return gameOver;
    }

    /*
    public void AddStar() {
        numberOfStars++;
    } */
}
