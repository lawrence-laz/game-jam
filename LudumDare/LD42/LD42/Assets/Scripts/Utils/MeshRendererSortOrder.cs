using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererSortOrder : MonoBehaviour {

    public int SortOrder;

    private MeshRenderer _meshRenderer;

    private void OnEnable()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        _meshRenderer.sortingOrder = SortOrder;
    }
}
