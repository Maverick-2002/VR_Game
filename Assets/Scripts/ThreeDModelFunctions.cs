using UnityEngine;
using System.Collections.Generic;
using System;
using DG.Tweening;
using NaughtyAttributes;

[Serializable]
public class SubMeshes
{
    public MeshRenderer meshRenderer;
    public Vector3 originalPosition;
    public Vector3 explodedPosition;
}

public class ThreeDModelFunctions : BaseInteractable
{
    [ReadOnly, SerializeField] private List<SubMeshes> childMeshRenderers;
    [SerializeField] private float explosionTime = 0.1f;
    [SerializeField] private float explosionDistance = 0.5f; // Distance for the explosion in 2D

    private Collider _collider;
    private bool isInExplodedView = false;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        childMeshRenderers = new List<SubMeshes>();
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();

        // Reference center for explosion (center of the collider)
        Vector3 center = _collider.bounds.center;

        foreach (var renderer in meshRenderers)
        {
            SubMeshes mesh = new SubMeshes
            {
                meshRenderer = renderer,
                originalPosition = renderer.transform.localPosition
            };

            // Direction in 2D plane (X, Y only)
            Vector3 direction = (renderer.transform.localPosition - transform.InverseTransformPoint(center));
            direction.z = 0; // Ensure no movement in Z-axis
            direction = direction.normalized; // Normalize to get direction only

            // Calculate exploded position
            mesh.explodedPosition = renderer.transform.localPosition + direction * explosionDistance;

            childMeshRenderers.Add(mesh);
        }
    }

    [Button]
    public override void Interact()
    {
        ToggleExplodedView();
    }

    private void ToggleExplodedView()
    {
        if (isInExplodedView)
        {
            isInExplodedView = false;
            foreach (var mesh in childMeshRenderers)
            {
                mesh.meshRenderer.transform.DOLocalMove(mesh.originalPosition, explosionTime);
            }
        }
        else
        {
            isInExplodedView = true;
            foreach (var mesh in childMeshRenderers)
            {
                mesh.meshRenderer.transform.DOLocalMove(mesh.explodedPosition, explosionTime);
            }
        }
    }
}
