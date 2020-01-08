using UnityEngine;

[ExecuteInEditMode]
public class ColliderToMesh : MonoBehaviour
{
    private PolygonCollider2D polygon;

    private void Start()
    {
        BuildMesh();
    }

    private void Update()
    {
        BuildMesh();
    }

    public void BuildMesh()
    {
        polygon = gameObject.GetComponent<PolygonCollider2D>();
        int pointCount = 0;
        pointCount = polygon.GetTotalPointCount();
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        Vector2[] points = polygon.points;
        Vector3[] vertices = new Vector3[pointCount];
        Vector2[] uv = new Vector2[pointCount];
        for (int j = 0; j < pointCount; j++)
        {
            Vector2 actual = points[j];
            vertices[j] = new Vector3(actual.x, actual.y, 0);
            uv[j] = actual;
        }
        Triangulator tr = new Triangulator(points);
        int[] triangles = tr.Triangulate();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mf.mesh = mesh;
    }
}