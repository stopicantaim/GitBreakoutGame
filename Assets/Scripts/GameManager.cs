using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public int lives = 3;
    public int totalBricks = 20; // To be updated per level if needed
    public int remainingBricks;
    public float resetDelay = 1f;
    public TextMeshProUGUI livesTextPrefab;
    public GameObject gameOverPanelPrefab; // Assuming prefabs for panels
    public GameObject youWonPanelPrefab;
    public GameObject bricksPrefab;
    public GameObject paddlePrefab;
    private GameObject clonePaddle;
    private TextMeshProUGUI livesTextInstance;
    private GameObject gameOverPanelInstance;
    private GameObject youWonPanelInstance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        remainingBricks = totalBricks; // Reset the brick count for the new level
        SetupSceneComponents();
    }

    public void SetupSceneComponents() {
        SetupPaddleAndBall();
        SetupUIComponents();
        // Optionally reset other level-specific settings here
    }

    void SetupPaddleAndBall() {
        if (clonePaddle != null) Destroy(clonePaddle);
        clonePaddle = Instantiate(paddlePrefab, new Vector3(0, -9.5f, 0), Quaternion.identity);
        // If the ball is a separate entity from the paddle and not instantiated with it, instantiate the ball here as well
    }

    void SetupUIComponents() {
        // Instantiate or find canvas to avoid duplications
        GameObject canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (!canvas) {
            // Log error or ensure a canvas is present in all scenes
            Debug.LogError("Canvas not found in the scene.");
            return;
        }

        // Lives Text
        if (livesTextInstance != null) Destroy(livesTextInstance.gameObject);
        livesTextInstance = Instantiate(livesTextPrefab, canvas.transform);
        UpdateLivesText();

        // Game Over Panel
        if (gameOverPanelInstance != null) Destroy(gameOverPanelInstance);
        gameOverPanelInstance = Instantiate(gameOverPanelPrefab, canvas.transform);
        gameOverPanelInstance.SetActive(false);

        // You Won Panel
        if (youWonPanelInstance != null) Destroy(youWonPanelInstance);
        youWonPanelInstance = Instantiate(youWonPanelPrefab, canvas.transform);
        youWonPanelInstance.SetActive(false);
    }

    public void LoseLife() {
        lives--;
        UpdateLivesText();
        Destroy(clonePaddle);
        Invoke(nameof(SetupPaddleAndBall), resetDelay);
        if (lives <= 0) {
            GameOver();
        }
    }

    public void DestroyBrick() {
        remainingBricks--;
        if (remainingBricks <= 0) {
            LevelCompleted();
        }
    }

    void GameOver() {
        // Show Game Over UI and potentially reset the game or return to the main menu
        gameOverPanelInstance.SetActive(true);
        // Consider adding a delay or a button to restart
    }

    void LevelCompleted() {
        // Go to next level or show victory screen if it was the last level
        LoadNextLevel();
    }

    void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            // Last level completed
            youWonPanelInstance.SetActive(true);
            Debug.Log("All levels completed!");
            // Consider adding logic to return to the main menu or restart the game
        }
    }

    private void UpdateLivesText() {
        if (livesTextInstance != null) livesTextInstance.text = "Lives: " + lives;
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
