using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RandomPosition();
    }

    void RandomPosition()
    {
        int x = Random.Range(-3,3);
        int y = Random.Range(-3,3);
        transform.position = new Vector2(x,y);
    }

    void OnTriggerEnter2D(Collider2D collisionObject)
    {
        RandomPosition();
    }
}
