using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameLoop : MonoBehaviour
{
    private Vector2 _direction;
    private bool _directionChanged;
    [SerializeField] private Snake _snakePrefab;
    private Snake _snake;
    [SerializeField] private Food _foodPrefab;
    private Food _food;
    public GridManager grid;

    void Start()
    {
        int width = grid.GetWidth();
        int height = grid.GetHeight();
        int center_x = width/2 - 1;
        int center_y = height/2 - 1;

        // Initialise objects
        _direction = Vector2.right;
        _directionChanged = false;
        _snake = Instantiate(_snakePrefab, new Vector3(center_x,center_y,-1), Quaternion.identity); // instantiate snake object
        _snake.SetGridLimits(width, height);
        _food = Instantiate(_foodPrefab, new Vector3(0,0,-1), Quaternion.identity); // instantiate snake object
        _food.SetGridLimits(width, height);
        _food.RandomizePosition();

    }

    void Update()
    {
        // process inputs
        InputMovement();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        // update game world
        if (_directionChanged)
            _snake.MoveSnake(_direction);
        
        if (!_snake.alive)
        {
            Time.timeScale = 0; // pauses the game for now
        }

        // generate outputs
    }

    void InputMovement()
    {
        if (Input.GetKey("up"))
        {
            _direction = Vector2.up;
            _directionChanged = true;
        }
        else if (Input.GetKey("down"))
        {
            _direction = Vector2.down;
            _directionChanged = true;
        }
        else if (Input.GetKey("left"))
        {
            _direction = Vector2.left;
            _directionChanged = true;
        }
        else if (Input.GetKey("right"))
        {
            _direction = Vector2.right;
            _directionChanged = true;
        }
    }
}
