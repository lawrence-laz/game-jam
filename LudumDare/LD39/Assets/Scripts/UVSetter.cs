using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class UVSetter : MonoBehaviour
{
    [SerializeField]
    Vector2 ratio = Vector2.one;

    Mesh mesh;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        RecalculateUV();
    }

    private void Update()
    {
#if UNITY_EDITOR
        RecalculateUV();
#endif
    }

    private void RecalculateUV()
    {
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x * transform.localScale.x * ratio.x, vertices[i].y * transform.localScale.y * ratio.y);
            uvs[i].y += Mathf.Abs(uvs[i].y);
        }
        mesh.uv = uvs;
    }
}
