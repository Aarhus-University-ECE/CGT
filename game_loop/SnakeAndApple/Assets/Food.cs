using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject food;
    private (int widthLim,int heightLim) gridLimits; 

    // Start is called before the first frame update
    void Start()
    {
        RandomizePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGridLimits(int widthLim, int heightLim)
    {
        gridLimits = (widthLim, heightLim);
    }

    public void RandomizePosition()
    {
        float x = Random.Range(0, gridLimits.widthLim);
        float y = Random.Range(0, gridLimits.heightLim);

        this.transform.position = new Vector3(x, y, -1f);
    }
}
