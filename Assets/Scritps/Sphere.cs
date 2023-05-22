using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Renderer rend;

    private void OnEnable()
    {
        rend = GetComponent<Renderer>();
        int rand = Random.Range(0,2);
        rend.material.color = rand > 0 ? Color.green : Color.blue;
    }
}
