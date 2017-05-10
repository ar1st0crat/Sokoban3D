using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Generated boxes
    /// </summary>
    private List<GameObject> _boxes = new List<GameObject>();

    /// <summary>
    /// Generated box platforms
    /// </summary>
    private List<GameObject> _platforms = new List<GameObject>();

    /// <summary>
    /// Generated walls
    /// </summary>
    private List<GameObject> _walls = new List<GameObject>();

    /// <summary>
    /// Box PreFab placeholder
    /// </summary>
    public Transform box;

    /// <summary>
    /// Platform PreFab placeholder
    /// </summary>
    public Transform platform;

    /// <summary>
    /// Wall PreFab placeholder
    /// </summary>
    public Transform wall;


    void Start()
    {
        SetLevelScene(0);
    }

    void Update()
    {
	}

    private void SetLevelScene(int levelNo)
    {
        var level = LevelsConfigurator.Get(levelNo);

        // add and place boxes to scene
        var boxPositions = level.BoxPositions.Split(';');

        foreach (var position in boxPositions)
        {
            var x = byte.Parse(position.Split(',')[0]);
            var z = byte.Parse(position.Split(',')[1]);

            var newBox = Instantiate(box, new Vector3(x - 0.5f, 0.5f, z - 0.5f), Quaternion.identity);

            _boxes.Add(newBox.gameObject);
        }

        // add and place platforms to scene
        var platformPositions = level.PlatformPositions.Split(';');

        foreach (var position in platformPositions)
        {
            var x = byte.Parse(position.Split(',')[0]);
            var z = byte.Parse(position.Split(',')[1]);

            var newPlatform = Instantiate(platform, new Vector3(x - 0.5f, 0.001f, z - 0.5f), Quaternion.identity);

            _platforms.Add(newPlatform.gameObject);
        }

        // add and place walls to scene
        var wallPositions = level.WallPositions.Split(';');
        var scaleVector = new Vector3();

        foreach (var position in wallPositions)
        {
            var rect = position.Split(',');
            var z1 = byte.Parse(rect[0]);
            var x1 = byte.Parse(rect[1]);
            var z2 = byte.Parse(rect[2]);
            var x2 = byte.Parse(rect[3]);

            var newWall = Instantiate(wall, new Vector3((x2 + x1)/2f - 0.5f, 0.3f, (z2 + z1)/2f - 0.5f), Quaternion.identity);
            scaleVector.x = x2 - x1 + 1;
            scaleVector.z = z2 - z1 + 1;
            scaleVector.y = newWall.localScale.y;

            newWall.localScale = scaleVector;

            _walls.Add(newWall.gameObject);
        }
    }

    public void TryMovingBox(Collider collider, Vector3 direction)
    {
        var expectedPosition = collider.transform.position + direction;

        foreach (var box in _boxes)
        {
            if (box.transform.position == expectedPosition)
            {
                return;
            }
        }

        foreach (var wall in _walls)
        {
            if (CheckWallCollision(wall, expectedPosition))
            {
                return;
            }
        }

        collider.transform.position = expectedPosition;
    }

    /// <summary>
    /// Testing for box and wall collision is more sophisticated
    /// </summary>
    private bool CheckWallCollision(GameObject wall, Vector3 expectedPosition)
    {
        float width = wall.transform.localScale.x;
        float height = wall.transform.localScale.z;

        //if (expectedPosition.x != wall.transform.position.x &&
        //    expectedPosition.z != wall.transform.position.z)
        //{
        //    return false;
        //}

        if (expectedPosition.x == wall.transform.position.x)
        {
            return (expectedPosition.z >= wall.transform.position.z - height / 2 &&
                    expectedPosition.z <= wall.transform.position.z + height / 2);
        }

        if (expectedPosition.z == wall.transform.position.z)
        {
            return (expectedPosition.x >= wall.transform.position.x - width / 2 &&
                    expectedPosition.x <= wall.transform.position.x + width / 2);
        }
        
        return false;
    }
}
