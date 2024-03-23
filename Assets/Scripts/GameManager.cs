using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int lives = 3;
    public int score = 0;
    public int totalBricks; // Add this line to track the total number of bricks

    public Text livesText;
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject winPanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
         gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        UpdateUI();
    }

      public void LoseLife()
    {
        lives--;
        UpdateUI();
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            // Optionally reset the player or ball position here
        }
    }

     public void DestroyBrick()
    {
        totalBricks--;
        // You could add score incrementing here
        score += 100; // For example, adding 100 points per brick destroyed
        UpdateUI();

        // Check if all bricks have been destroyed
        if (totalBricks <= 0)
        {
            // Trigger win condition
            WinGame();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    void UpdateUI()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        // Freeze gameplay or offer a restart option
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        // Trigger win conditions, could be loading a new level or showing a win screen
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        lives = 3; // Reset lives
        score = 0; // Reset score
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        UpdateUI();
    }


}
