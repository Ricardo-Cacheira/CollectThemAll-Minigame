using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoardManager : MonoBehaviour
{
    public Transform board;
    public GameObject sphere;

    [ContextMenu("Create Board")]
    public void Create()
    {
        foreach (Transform ball in board)
        {
            DestroyImmediate(ball.gameObject);
        }

        for (int x = -3; x < 4; x++)
        {
            for (int y = 3; y > -4; y--)
            {
                // Debug.Log(x + "," + y);
                Instantiate(sphere, new Vector3(x, y, 0), Quaternion.identity, board).name = x + "," + y;
            }
        }
    }
}