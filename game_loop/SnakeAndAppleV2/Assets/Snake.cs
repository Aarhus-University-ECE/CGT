using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    Vector2 direction;
    public GameObject segment;
    List<GameObject> segments = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    void Reset()
    {
        transform.position = new Vector2(0,-1);
        direction = Vector2.right;
        Time.timeScale = 0.1f; // speed
        ResetSegments();
    }

    void ResetSegments()
    {
        // Destroy segments
        for (int i = 1; i < segments.Count; ++i)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(gameObject);

        for (int i = 0; i < 3; ++i)
        {
            Grow();
        }
    }

    void Grow()
    {
        GameObject newSegment = Instantiate(segment);
        newSegment.transform.position = segments[segments.Count - 1].transform.position;
        segments.Add(newSegment);
    }

    // Update is called once per frame
    void Update()
    {
        GetUserInput();
    }

    void GetUserInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
            transform.rotation = Quaternion.Euler(0,0,180);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
            transform.rotation = Quaternion.Euler(0,0,-90);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
            transform.rotation = Quaternion.Euler(0,0,90);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }


    void FixedUpdate()
    {
        MoveSegments();
        MoveSnake();
    }

    void MoveSegments()
    {
     for (int i = segments.Count - 1; i > 0; --i)
     {
        segments[i].transform.position = segments[i-1].transform.position;
     }   
    }

    void MoveSnake()
    {
        float x = transform.position.x + direction.x/2;
        float y = transform.position.y + direction.y/2;
        transform.position = new Vector2(x,y);
    }

    void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.tag == "Obstacle")
        {
            Time.timeScale = 0;
        }
        else if (collisionObject.tag == "Food")
        {
            Grow();
        }
    }
}
