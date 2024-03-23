using UnityEngine;

public class BallController : MonoBehaviour
{
    public float launchPower = 600f; // Adjust as needed
    public Rigidbody2D rb; // Reference to the Rigidbody2D component
    public bool isLaunched = false; // Check if the ball has been launched

    private Vector3 startPosition;
    private Vector2 paddleToBallVector;

    void Start()
    {
        // Save the starting position
        startPosition = transform.position;
        // Calculate and save the initial offset between paddle and ball
        paddleToBallVector = transform.position - GameObject.FindGameObjectWithTag("Paddle").transform.position;
    }

    void Update()
    {
        if (!isLaunched)
        {
            // Lock the ball's position relative to the paddle before launch
            Vector3 paddlePosition = GameObject.FindGameObjectWithTag("Paddle").transform.position;
            transform.position = paddlePosition + (Vector3)paddleToBallVector;

            // Launch the ball on mouse click or spacebar press
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(Random.Range(-0.5f, 0.5f), 1).normalized * launchPower;
                isLaunched = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Optional: tweak the ball's velocity after collision to ensure it doesn't slow down or get stuck in a loop
        if (isLaunched)
        {
            Vector2 velocityTweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
            rb.velocity += velocityTweak;
        }

        // Implement logic for what happens when hitting different objects, if needed
    }

    public void ResetBall()
    {
        // Reset the ball to its start position and state when needed (e.g., losing a life)
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        isLaunched = false;
    }
}
