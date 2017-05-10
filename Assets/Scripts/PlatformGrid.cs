using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PlatformGrid : MonoBehaviour
{
    public int RowCount;
    public int ColumnCount;
    
    private void Generate()
    {
        var mesh = new Mesh();

        var gridSize = (RowCount + 1) * (ColumnCount + 1);

        var vertices = new Vector3[gridSize];
        var normals = new Vector3[gridSize];
        var uv = new Vector2[gridSize];

        for (int i = 0, z = 0; z <= RowCount; z++)
        {
            for (int x = 0; x <= ColumnCount; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z);
                normals[z * ColumnCount + x] = Vector3.up;
                uv[z * ColumnCount + x] = new Vector2((float)x / ColumnCount, (float)z / RowCount);
            }
        }
        
        int[] triangles = new int[RowCount * ColumnCount * 6];
        for (int ti = 0, vi = 0, z = 0; z < RowCount; z++)
        {
            for (int x = 0; x < ColumnCount; x++, ti += 6, vi++)
            {
                triangles[ti    ] = vi;
                triangles[ti + 1] = vi + ColumnCount + 1;
                triangles[ti + 2] = vi + ColumnCount;
                triangles[ti + 3] = vi;
                triangles[ti + 4] = vi + 1;
                triangles[ti + 5] = vi + ColumnCount + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Awake()
    {
        Generate();
    }
    
    void Start()
    {
		
	}
	
	void Update()
    {
		
	}
}
