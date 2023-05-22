using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField] private SColor sColor;
    private Renderer rend;

    private void OnEnable()
    {
        rend = GetComponent<Renderer>();
        sColor = GameManager.Instance.colors[Random.Range(0,GameManager.Instance.colors.Count)];
        rend.material.color = sColor.color;
    }

    public SColor GetColor() => sColor;
}
