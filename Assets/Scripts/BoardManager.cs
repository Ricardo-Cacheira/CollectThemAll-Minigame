using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoardManager : MonoBehaviour
{
    public static Action BoardReady;

    public Transform board;
    public GameObject sphere;
    [Space]
    public int boardSize = 7;
    public Sphere[,] boardArray;

    private void Start()
    {
        Create();
    }

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
                // Debug.Log(x + "," + y);
                var obj = Instantiate(sphere, new Vector3(x, -y, 0), Quaternion.identity, board);
                boardArray[x,y] = obj.GetComponent<Sphere>();
            }
        }

        if(GameManager.Instance != null)
            GameManager.Instance.mainCamera.transform.position = new Vector3(boardSize/2,-(boardSize/2),-10);
        BoardReady?.Invoke();
    }

    public void RefillBoard()
    {
        StartCoroutine(RefillCoroutine());
    }

    private IEnumerator RefillCoroutine()
    {
        //Organize board Array and Move Spheres
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = boardSize - 1; y >= 0; y--)            
            {
                if(boardArray[x,y] == null)
                {
                    for (int i = y-1; i >= 0; i--)
                    {
                        if(boardArray[x,i] != null)
                        {
                            boardArray[x,y] = boardArray[x,i];
                            boardArray[x,i] = null;
                            // boardArray[x,y].transform.position = new Vector3(x,-y,0);
                            LeanTween.move(boardArray[x,y].gameObject, new Vector3(x,-y,0), 0.25f).setEase(LeanTweenType.easeInCubic);
                            break;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.5f);

        //Fill empty spaces
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if(boardArray[x,y] == null)
                {
                    var obj = Instantiate(sphere, new Vector3(x, -y, 0), Quaternion.identity, board);
                    boardArray[x,y] = obj.GetComponent<Sphere>();
                    yield return new WaitForSeconds(0.025f);
                }
            }
        }
        BoardReady?.Invoke();
    }
}