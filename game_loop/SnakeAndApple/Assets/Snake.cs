using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject snakeElement;
    private int _snakeLength = 1;
    private float speed = 1f;
    private (int widthLim,int heightLim) gridLimits; 
    public bool alive = true;


    public void MoveSnake(Vector2 direction)
    {
        float newPosX = Mathf.Round(this.transform.position.x) + direction.x * speed;
        float newPosY = Mathf.Round(this.transform.position.y) + direction.y * speed;
        Debug.Log("x: " + newPosX + ", y: " + newPosY);

        if (newPosX >= gridLimits.widthLim || 
            newPosY >= gridLimits.heightLim ||
            newPosX <= -1 || 
            newPosY <= -1)
        {
            Debug.Log("Snake position (" + newPosX + "," + newPosY + ") exceeded grid (" + gridLimits.widthLim + "," + gridLimits.heightLim + ")");
            alive = false;
            return;
        } 

        this.transform.position = new Vector3(
            newPosX,
            newPosY,
            -1.0f
        );
    }

    public void ExtendSnake()
    {
        // get position of current snake-end
        // instantiate new snake element
        // create rigid body and attach new snake element to the end of the snake
        // update snake length
    }

    public void SetGridLimits(int widthLim, int heightLim)
    {
        gridLimits = (widthLim, heightLim);
    }
}
