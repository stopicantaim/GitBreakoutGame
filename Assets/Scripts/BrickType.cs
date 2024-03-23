using UnityEngine;

[CreateAssetMenu(fileName = "New BrickType", menuName = "Game/Brick Type", order = 1)]
public class BrickType : ScriptableObject
{
    public Color color;
    public int hitPoints = 1;
    public int pointsValue = 100;
    public bool spawnsPowerUp = false;
}
