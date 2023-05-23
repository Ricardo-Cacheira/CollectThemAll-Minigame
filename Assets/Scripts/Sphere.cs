using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public bool current = false;
    [SerializeField]  private SColor sColor;
    [SerializeField]  private GameObject highlight;
    [SerializeField]  private LineRenderer line;
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

    public void Fall()
    {
        Vector3 target = transform.position;
        transform.position += Vector3.up*5;
        LeanTween.move(gameObject, target, 0.1f * transform.position.y).setEase(LeanTweenType.easeInCirc);
    }

    public void Select()
    {
        highlight.SetActive(true);
        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        current = true;
    }

    public void Deselect()
    {
        highlight.SetActive(false);
        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        current = false;
    }

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
}
