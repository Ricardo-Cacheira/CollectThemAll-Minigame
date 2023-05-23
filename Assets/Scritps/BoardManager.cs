using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoardManager : MonoBehaviour
{
    public Transform board;
    public GameObject sphere;
    [Space]
    public int boardSize = 7;
    public Sphere[,] boardArray;

    [ContextMenu("Create Board")]
    public void Create()
    {
        for (int i = this.transform.childCount; i > 0; --i)
        {
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        }
        boardArray = new Sphere[boardSize,boardSize];
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                Debug.Log(x + "," + y);
                var obj = Instantiate(sphere, new Vector3(x, -y, 0), Quaternion.identity, board);
                boardArray[x,y] = obj.GetComponent<Sphere>();
            }
        }

        Camera.main.transform.position = new Vector3(boardSize/2,-(boardSize/2),-10);
    }
}