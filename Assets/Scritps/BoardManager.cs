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
    public float spacing = 1.15f;
    public int boardSize = 7;

    [ContextMenu("Create Board")]
    public void Create()
    {
        for (int i = this.transform.childCount; i > 0; --i)
        {
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        }
        float edge = (spacing*3);
        int size = (int)Math.Ceiling(boardSize/2f);
        for (float x = -edge; x < size; x+= spacing)
        {
            for (float y = edge; y > -size; y-= spacing)
            {
                // Debug.Log(x + "," + y);
                Instantiate(sphere, new Vector3(x, y, 0), Quaternion.identity, board);
            }
        }
    }
}