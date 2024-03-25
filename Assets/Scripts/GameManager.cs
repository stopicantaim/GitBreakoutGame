using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Add this line to use TextMeshPro

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public int lives = 3;
    public int bricks = 20;
    public float resetDelay = 1f;
    public TextMeshProUGUI livesText; // Changed from Text to TextMeshProUGUI
    public GameObject gameOver;
    public GameObject youWon;
    public GameObject bricksPrefab;
    public GameObject paddle;
    private GameObject clonePaddle;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        Setup();
    }

    public void Setup() {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity);
        Instantiate(bricksPrefab, transform.position, Quaternion.identity);
    }

    void CheckGameOver() {
        if (bricks < 1) {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
                // Give a bonus life, except on the last level
                lives++;
                livesText.text = "Lives: " + lives; // Text assignment for TextMeshPro

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else {
                youWon.SetActive(true);
                Time.timeScale = .25f;
                Invoke("Reset", resetDelay);
            }
        }
        if (lives < 1) {
            gameOver.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);
        }
    }

    public void LoseLife() {
        lives--;
        livesText.text = "Lives: " + lives; // Text assignment for TextMeshPro
        Destroy(clonePaddle);
        Invoke("SetupPaddle", resetDelay);
        CheckGameOver();
    }

    void SetupPaddle() {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity);
    }

    public void DestroyBrick() {
        bricks--;
        CheckGameOver();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
