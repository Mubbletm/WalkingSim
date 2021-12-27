using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalObject : MonoBehaviour
{
    public Condition condition;

    private Collider collider;
    private MeshRenderer meshRenderer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        collider = gameObject.GetComponent<Collider>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        bool shown = condition.Invoke(0);
        if (spriteRenderer != null) spriteRenderer.enabled = shown;
        if (meshRenderer != null) meshRenderer.enabled = shown;
        if (collider != null) collider.enabled = shown;
    }
}
