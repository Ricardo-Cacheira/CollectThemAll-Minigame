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

    private void Awake()
    {
        Instance = this;
    }
}