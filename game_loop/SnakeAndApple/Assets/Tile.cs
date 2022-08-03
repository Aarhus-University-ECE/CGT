using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? new Color(0.7f, 0.7f, 0.7f, 1) : new Color(0.8f, 0.8f, 0.8f, 1);
    }
}
