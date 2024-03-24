using UnityEngine;

public class BrickLayoutGenerator : MonoBehaviour
{
    public GameObject brickPrefab;
    public int rows = 7;
    public int columns = 13;
    public float spacing = 1.1f;
    public Vector2 startOffset = new Vector2(-6f, 3f);
    public Color[] rowColors; // Define colors for each row in the Inspector

    void Start()
    {
        Debug.Log("Starting to generate bricks...");
        GenerateLayout();
    }

    void GenerateLayout()
    {
        if (brickPrefab == null)
        {
            Debug.LogError("Brick prefab is not assigned to BrickLayoutGenerator.");
            return;
        }

        Vector3 startPosition = transform.position + (Vector3)startOffset;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = startPosition + new Vector3(col * (brickPrefab.transform.localScale.x + spacing), -row * (brickPrefab.transform.localScale.y + spacing), 0);
                GameObject brickInstance = Instantiate(brickPrefab, position, Quaternion.identity, this.transform);

                // Check if we have a color defined for the current row, then apply it
                if (row < rowColors.Length)
                {
                    Renderer brickRenderer = brickInstance.GetComponent<Renderer>();
                    if (brickRenderer != null)
                    {
                        brickRenderer.material.color = rowColors[row];
                    }
                    else
                    {
                        Debug.LogWarning("Renderer component not found on brick prefab.");
                    }
                }
            }
        }

        Debug.Log("Finished generating bricks.");
    }
}
