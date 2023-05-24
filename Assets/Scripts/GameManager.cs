using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct SColor
{
    public string name;
    public int id;
    public Color color;
}

public class GameManager : MonoBehaviour
{
    public static Action<int,int> StartGameEvent;
    public static GameManager Instance {get; private set;}

    public List<SColor> colors;
    public BoardManager boardManager;
    public Camera mainCamera;

    private void Awake()
    {
        Instance = this;        
        mainCamera = Camera.main;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        //Moves, Goal
        StartGameEvent?.Invoke(Random.Range(10,18), Random.Range(5,11)*10);
    }

    public void Restart()
    {
        StartGame();
        boardManager.Create();
    }

    public float GetDiagonalDistance()
    {
        return Vector2.Distance(Vector2.zero, Vector2.one)+ 0.05f;
    }
}