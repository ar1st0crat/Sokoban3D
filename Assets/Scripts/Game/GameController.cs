using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class GameController : MonoBehaviour, IGameController // TODO : ITickable
{
    private Box.Factory _boxFactory;

    [Inject]
    public void Construct(Box.Factory boxFactory)
    {
        _boxFactory = boxFactory;
    }

    /// <summary>
    /// This object provides configuration of all assets for each level of the game
    /// </summary>
    private LevelsConfigurator _levelsConfigurator = new LevelsConfigurator();

    /// <summary>
    /// Generated boxes
    /// </summary>
    private List<Box> _boxes = new List<Box>();

    /// <summary>
    /// Generated box platforms
    /// </summary>
    private List<GameObject> _platforms = new List<GameObject>();

    /// <summary>
    /// Generated walls
    /// </summary>
    private List<GameObject> _walls = new List<GameObject>();

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
        StartGame();
    }

    void Update()
    {
    }

    public void StartGame()
    {
        SetLevelScene(0);
    }

    public void UpdateGame()
    {
    }

    public void SetLevelScene(int levelNo)
    {
        var level = _levelsConfigurator.Get(levelNo);

        // add and place boxes to scene
        var boxPositions = level.BoxPositions.Split(';');

        foreach (var position in boxPositions)
        {
            var x = byte.Parse(position.Split(',')[0]);
            var z = byte.Parse(position.Split(',')[1]);

            var newBox = _boxFactory.Create();

            newBox.Row = x;
            newBox.Column = z;
            newBox.Position = new Vector3(x - 0.5f, 0.5f, z - 0.5f);
            newBox.Rotation = Quaternion.identity;

            _boxes.Add(newBox);
        }

        // add and place platforms to scene
        var platformPositions = level.PlatformPositions.Split(';');

        foreach (var position in platformPositions)
        {
            var x = byte.Parse(position.Split(',')[0]);
            var z = byte.Parse(position.Split(',')[1]);

            // so far no need for dependency injection here
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

            // so far no need for dependency injection here as well
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
