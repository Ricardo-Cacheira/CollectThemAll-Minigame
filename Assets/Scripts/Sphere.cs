using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField] private SColor sColor;
    private Renderer rend;
    private GameObject highlight;

    private void OnEnable()
    {
        rend = GetComponent<Renderer>();
        highlight = transform.GetChild(0).gameObject;
        Deselect();
        sColor = GameManager.Instance.colors[Random.Range(0,GameManager.Instance.colors.Count)];
        rend.material.color = sColor.color;
        Fall();
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
    }

    public void Deselect()
    {
        highlight.SetActive(false);
    }

    public SColor GetColor() => sColor;
}