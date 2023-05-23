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

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        UIManager.GameEndedEvent += OnGameEnded;
    }

    private void OnDisable()
    {
        UIManager.GameEndedEvent -= OnGameEnded;
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

    private void OnGameEnded(bool win)
    {
        //TODO Show PopUp
        Debug.Log("You " + (win ? "WON!" : "Lost :("));
    }

    public float GetDiagonalDistance()
    {
        return Vector2.Distance(Vector2.zero, Vector2.one * boardManager.spacing)+ 0.05f;
    }
}