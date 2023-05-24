using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField]  private SColor sColor;
    [SerializeField]  private GameObject highlight;
    [SerializeField]  private LineRenderer line;
    private bool current = false;
    private Renderer rend;

    private void OnEnable()
    {
        rend = GetComponent<Renderer>();
        Deselect();
        sColor = GameManager.Instance.colors[Random.Range(0,GameManager.Instance.colors.Count)];
        rend.material.color = sColor.color;
        Fall();
    }

    private void Update()
    {
        //Control the line
        if(current)
        {
            Vector3 mousePos = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 direction = mousePos - transform.position;
            if(direction.magnitude > GameManager.Instance.GetDiagonalDistance())
            {
                direction.Normalize();
                direction *= GameManager.Instance.GetDiagonalDistance();
            }

            line.SetPosition(1, direction);
        }
    }

    //Animate to final position
    public void Fall()
    {
        Vector3 target = transform.position;
        transform.position += Vector3.up*5;
        LeanTween.move(gameObject, target, 0.1f * transform.position.y).setEase(LeanTweenType.easeInCirc);
    }

    //Enable Highlight and set to Current = true
    public void Select()
    {
        highlight.SetActive(true);
        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        current = true;
    }

    //Remove Highlight and line
    public void Deselect()
    {
        highlight.SetActive(false);
        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        current = false;
    }

    //Set Line to the next Sphere in the link
    public void Link(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        if(direction.magnitude > GameManager.Instance.GetDiagonalDistance())
        {
            direction.Normalize();
            direction *= GameManager.Instance.GetDiagonalDistance();
        }

        line.SetPosition(1, direction);
    }

    public SColor GetColor() => sColor;
    public void SetCurrent(bool value) => current = value;
}
