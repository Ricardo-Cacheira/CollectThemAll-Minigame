using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchState
{
    Idle,
    Holding,
    Released
}

public class TouchManager : MonoBehaviour
{
    public static Action<int> MoveMadeEvent;

    public LayerMask sphereLayer;
    [SerializeField]
    private int minLinkAmount = 3;
    [SerializeField]
    private TouchState state;
    [SerializeField]
    private List<Sphere> link;
    private SColor current;
    private float diagonaDistance;

    private void OnEnable()
    {
        diagonaDistance = GameManager.Instance.GetDiagonalDistance();
        
        BoardManager.BoardReady += OnBoardReady;
        UIManager.GameEndedEvent += OnGameEnded;
    }

    private void OnDisable()
    {
        BoardManager.BoardReady -= OnBoardReady;
        UIManager.GameEndedEvent -= OnGameEnded;
    }

    void Update()
    {
        //Check for input
        if (state != TouchState.Released && Input.GetMouseButton(0))
        {
            if(state == TouchState.Idle)
                state = TouchState.Holding;

            //Check for collisions
            Vector3 pointerPosition = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(pointerPosition, GameManager.Instance.mainCamera.transform.TransformDirection(Vector3.forward), out hit, 15, sphereLayer))
            {
                //If there's a link ongoing, validate new collision, else start new link
                Sphere sphere = hit.collider.GetComponent<Sphere>();
                if(sphere != null)
                {
                    if(link.Count > 0)
                    {
                        if(link.Count > 1 && sphere == link[link.Count-2])
                        {
                            link[link.Count - 1].Deselect();
                            link.RemoveAt(link.Count - 1);
                            link[link.Count - 1].Deselect();
                            link.RemoveAt(link.Count - 1);
                        }
                        else if((Vector3.Distance(link.Last().transform.position, sphere.transform.position) < diagonaDistance) && sphere.GetColor().id == current.id)
                        {
                            if(!link.Contains(sphere))
                            {
                                link[link.Count - 1].SetCurrent(false);
                                link[link.Count - 1].Link(sphere.transform.position);
                                sphere.Select();
                                link.Add(sphere);
                            }
                        }
                    }
                    else
                    {
                        current = sphere.GetColor();
                        link.Add(sphere);
                        sphere.Select();
                    }
                }
            }

        }else if(state == TouchState.Holding)
        {
            state = TouchState.Released;


            StartCoroutine(nameof(Score));
        }
    }

    private IEnumerator Score()
    {
        if(link.Count >= minLinkAmount)
            MoveMadeEvent?.Invoke(link.Count);

        foreach (Sphere ball in link)
        {
            ball.Deselect();

            if(link.Count >= minLinkAmount)
            {
                Destroy(ball.gameObject);
                yield return new WaitForSeconds(0.05f);
            }
        }

        if(link.Count >= minLinkAmount)
            GameManager.Instance.boardManager.RefillBoard();
        else
            OnBoardReady();
    }

    private void OnBoardReady()
    {
        link = new List<Sphere>();
        current.id = -1;
        state = TouchState.Idle;
    }

    private void OnGameEnded(bool win)
    {
        link = new List<Sphere>();
        current.id = -1;
        state = TouchState.Released;
    }
}
