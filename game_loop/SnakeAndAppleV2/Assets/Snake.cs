using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    Vector2 direction;
    public GameObject segment;
    List<GameObject> segments = new List<GameObject>();
    int snakeInitialLength = 0;
    private int score = 0;
    public Text txt;
    public Button resetBtn;
    public Button pauseBtn;
    private bool playFlag = false;
    private bool snakeDead = false;

    // Start is called before the first frame update
    void Start()
    {   
        resetBtn.onClick.AddListener(Reset);
        pauseBtn.onClick.AddListener(PausePlayGame);
        score = 0;
        txt.text = "Score : " + score.ToString();
        Reset();
    }

    void Reset()
    {
        snakeDead = false;
        transform.position = new Vector2(0,-1);
        direction = Vector2.right;
        transform.rotation = Quaternion.Euler(0,0,-90);
        Time.timeScale = 0.1f; // speed
        ResetScore();
        ResetSegments();
        PlayGame();
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

        for (int i = 0; i < snakeInitialLength; ++i)
        {
            Grow();
        }
    }

    void ResetScore()
    {
        score = 0;
        txt.text = "Score : " + score.ToString();
    }

    void Grow()
    {
        GameObject newSegment = Instantiate(segment);
        newSegment.transform.position = segments[segments.Count - 1].transform.position;
        segments.Add(newSegment);
        Time.timeScale += 0.02f;
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
            ResetScore();
            PauseGame();
            snakeDead = true;
        }
        else if (collisionObject.tag == "Food")
        {
            Grow();
            score = segments.Count - 1;
            txt.text = "Score : " + score.ToString();
        }
    }


    void PausePlayGame()
    {
        if (playFlag)
        {
            PauseGame();
            Time.timeScale = 0;
        }
        else
        {
            PlayGame();
            if (snakeDead)
                Reset();
            else
                Time.timeScale = 0.1f;
        }
    }

    void PlayGame()
    {
        playFlag = true;
        (pauseBtn.GetComponentInChildren(typeof(Text)) as Text).text = "Pause";
    }

    void PauseGame()
    {
        playFlag = false;
        (pauseBtn.GetComponentInChildren(typeof(Text)) as Text).text = "Play";
    }
}
