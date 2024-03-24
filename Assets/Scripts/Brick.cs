using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject brickParticle; // Assign a particle effect prefab in the Inspector.

    void Start()
    {
        // If additional initialization is needed, add it here.
    }

    void OnCollisionEnter(Collision other)
    {
        // Instantiate particle effect at the brick's position upon collision.
        Instantiate(brickParticle, transform.position, Quaternion.identity);

        // Call the GameManager's method to handle a brick being destroyed.
        GameManager.instance.DestroyBrick();

        // Then destroy the brick GameObject.
        Destroy(gameObject);
    }
}
