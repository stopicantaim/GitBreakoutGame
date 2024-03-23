using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject brickParticle;

    private GameManager GM;

    void Start()
    {
        // Find the GameManager in the scene and store a reference to it
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        // Instantiate the brick particle effect at the brick's position and rotation
        Instantiate(brickParticle, transform.position, Quaternion.identity);

        // Call the GameManager's function to handle the brick's destruction
        GM.DestroyBrick();

        // Destroy the brick game object
        Destroy(gameObject);
    }
}
