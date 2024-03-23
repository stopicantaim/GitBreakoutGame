using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f; // Speed at which the paddle moves
    public float limit = 8f; // Limit to how far the paddle can move to the sides

    void Update()
    {
        // Get player's horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the new position based on input and speed
        Vector3 newPosition = transform.position + new Vector3(horizontalInput, 0, 0) * speed * Time.deltaTime;

        // Clamp the paddle's position to prevent it from going off-screen
        newPosition.x = Mathf.Clamp(newPosition.x, -limit, limit);

        // Update the paddle's position
        transform.position = newPosition;
    }
}
