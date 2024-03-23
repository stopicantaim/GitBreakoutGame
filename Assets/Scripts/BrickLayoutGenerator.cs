using UnityEngine;

public class BrickLayoutGenerator : MonoBehaviour
{
    public GameObject brickPrefab;
    public BrickType[] brickTypes; // Assign these in the Inspector
    public int rows = 5;
    public int columns = 10;
    public float spacing = 1.1f;
    public Vector2 startOffset = new Vector2(-4.5f, 2); // Adjust based on level size and camera setup

    private void Start()
    {
        GenerateLayout();
    }

    private void GenerateLayout()
    {
        Vector3 startPosition = transform.position + (Vector3)startOffset;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calculate position based on a grid layout
                Vector3 position = startPosition + new Vector3(col * spacing, -row * spacing, 0);
                CreateBrickAtPosition(position, row);
            }
        }
    }

    private void CreateBrickAtPosition(Vector3 position, int row)
    {
        GameObject brickInstance = Instantiate(brickPrefab, position, Quaternion.identity, transform);

        // Determine the brick type based on the row or other logic
        int typeIndex = row % brickTypes.Length;
        BrickType brickType = brickTypes[typeIndex];

        CustomizeBrick(brickInstance, brickType);
    }

    private void CustomizeBrick(GameObject brick, BrickType brickType)
    {
        // Apply color; additional properties can be set here as needed
        Renderer brickRenderer = brick.GetComponent<Renderer>();
        if (brickRenderer != null)
        {
            brickRenderer.material.color = brickType.color;
        }

        // If the Brick script on the prefab needs to be configured, do it here
        // Example: brick.GetComponent<Brick>().SetBrickType(brickType);
    }
}
