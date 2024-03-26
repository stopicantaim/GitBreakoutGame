using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public int lives = 3;
    public int totalBricks; // Dynamically adjusted based on the current level
    public int remainingBricks;
    public float resetDelay = 1f;
    public TextMeshProUGUI livesTextPrefab;
    public GameObject gameOverPanelPrefab; 
    public GameObject youWonPanelPrefab;
    public GameObject bricksPrefab;
    public GameObject paddlePrefab; // Assuming a prefab for the paddle
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
        SetBrickCountBasedOnLevel(scene.name);
        SetupSceneComponents();
    }

    public void SetupSceneComponents() {
        SetupPaddleAndBall();
        SetupUIComponents();
    }

    void SetupPaddleAndBall() {
        if (clonePaddle != null) Destroy(clonePaddle);
        clonePaddle = Instantiate(paddlePrefab, new Vector3(0, -9.5f, 0), Quaternion.identity);
    }

    void SetupUIComponents() {
        GameObject canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (!canvas) {
            Debug.LogError("Canvas not found in the scene.");
            return;
        }

        if (livesTextInstance != null) Destroy(livesTextInstance.gameObject);
        livesTextInstance = Instantiate(livesTextPrefab, canvas.transform);
        UpdateLivesText();

        if (gameOverPanelInstance != null) Destroy(gameOverPanelInstance);
        gameOverPanelInstance = Instantiate(gameOverPanelPrefab, canvas.transform);
        gameOverPanelInstance.SetActive(false);

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
        gameOverPanelInstance.SetActive(true);
    }

    void LevelCompleted() {
        LoadNextLevel();
    }

    void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            youWonPanelInstance.SetActive(true);
            Debug.Log("All levels completed!");
        }
    }

    private void UpdateLivesText() {
        if (livesTextInstance != null) livesTextInstance.text = "Lives: " + lives;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            LoadNextLevel();
        }
    }

    void SetBrickCountBasedOnLevel(string sceneName) {
        // Dynamically adjust totalBricks based on the current level
        switch (sceneName) {
            case "LevelOne":
                totalBricks = 20;
                break;
            case "LevelTwo":
                totalBricks = 35;
                break;
            case "LevelThree":
                totalBricks = 42;
                break;
            default:
                totalBricks = 20; // Default value for any other levels or unexpected values
                break;
        }
        remainingBricks = totalBricks;
    }

    void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
