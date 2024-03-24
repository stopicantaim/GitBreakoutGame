using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives = 3;
    public int score = 0;
    // Assuming you're tracking the total number of bricks to determine the win condition
    public int totalBricks;

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
    }

    public void DestroyBrick()
    {
        totalBricks--;
        score += 100; // Example: Adding 100 points per brick destroyed.
        if (totalBricks <= 0)
        {
            // If all bricks are destroyed, player wins.
            winPanel.SetActive(true);
        }
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            gameOverPanel.SetActive(true);
            // Consider pausing the game or providing a restart option here.
        }
        else
        {
            // Reset the ball position or other game elements as needed.
        }
    }

    // Include additional methods as needed for your game logic.
}
