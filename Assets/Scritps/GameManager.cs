using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SColor
{
    public string name;
    public int id;
    public Color color;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public List<SColor> colors;
    public BoardManager boardManager;

    private void Awake()
    {
        Instance = this;
    }

    public float GetDiagonalDistance()
    {
        return Vector2.Distance(Vector2.zero, Vector2.one * boardManager.spacing)+ 0.05f;
    }
}