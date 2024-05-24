using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Renderer renderer = other.GetComponent<Renderer>();
        if (renderer != null) { SetTransparency(renderer, 0.2f); }      
    }

    private void OnTriggerExit(Collider other)
    {
        Renderer renderer = other.GetComponent<Renderer>();
        if (renderer != null) { SetTransparency(renderer, 1f); }
    }

    void SetTransparency(Renderer renderer, float alpha)
    {
        foreach (var material in renderer.materials)
        {
            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }
}
