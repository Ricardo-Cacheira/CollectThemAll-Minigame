using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum TouchState
{
    Idle,
    Holding,
    Released
}

public class TouchManager : MonoBehaviour
{
    public LayerMask sphereLayer;
    [SerializeField]
    private int minLinkAmount = 3;
    [SerializeField]
    private TouchState state;
    [SerializeField]
    private List<Sphere> link;
    private Camera mainCamera;
    private SColor current;

    private void Awake()
    {
        mainCamera = Camera.main;
        link = new List<Sphere>();
        state = TouchState.Idle;
        current.id = -1;
    }

    void Update()
    {
        //Input.GetTouch
        if (state != TouchState.Released && Input.GetMouseButton(0))
        {
            if(state == TouchState.Idle)
                state = TouchState.Holding;

            Vector3 pointerPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(pointerPosition, mainCamera.transform.TransformDirection(Vector3.forward), out hit, 15, sphereLayer))
            {

                Sphere sphere = hit.collider.GetComponent<Sphere>();
                if(sphere != null)
                {
                    // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    // Debug.Log("Did Hit");

                    if(link.Count > 0)
                    {
                        if(link.Count > 1 && sphere == link[link.Count-2])
                        {
                            link[link.Count - 1].Deselect();
                            link.RemoveAt(link.Count - 1);
                            link[link.Count - 1].Deselect();
                            link.RemoveAt(link.Count - 1);
                        }
                        else if((Vector3.Distance(link.Last().transform.position, sphere.transform.position) < 1.42f) && sphere.GetColor().id == current.id)
                        {
                            if(!link.Contains(sphere))
                            {
                                link.Add(sphere);
                                sphere.Select();
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
            else
            {
                //* Do nothing
                // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                // Debug.Log("Did not Hit");
            }
        }else if(state == TouchState.Holding)
        {
            state = TouchState.Released;
            StartCoroutine(nameof(Score));
        }
    }

    private IEnumerator Score()
    {
        Debug.Log("score: " + link.Count);
        foreach (Sphere ball in link)
        {
            ball.Deselect();

            if(link.Count >= minLinkAmount)
            {
                yield return new WaitForSeconds(0.05f);
                //TODO explode and refill board
            }
        }
        yield return null;

        link = new List<Sphere>();
        state = TouchState.Idle;
        current.id = -1;
    }
}
