using UnityEngine;
using System.Collections.Generic;

public class NetVisualizer : MonoBehaviour 
{
    
    public NetController m_net;
    List<Vector2> verticies = new List<Vector2>();
    Mesh currentMesh;
	// Update is called once per frame
    void Start()
    {
        PrepareMesh();
    }
    public void PrepareMesh()
    {
        GetVertecies();
        currentMesh = new Mesh();
        
        currentMesh.vertices = new Vector3[verticies.Count* 4 ];
        currentMesh.uv = new Vector2[verticies.Count* 4 ];
        currentMesh.triangles = new int[(verticies.Count-1) * 6];
        DrawMesh();
        currentMesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = currentMesh;
    }
    void DrawMesh()
    {
        transform.rotation = Quaternion.identity;
        GetVertecies();
        Vector2 start = verticies[0];
        Vector2 finish = verticies[verticies.Count - 1];
        Vector2 dir = finish - start;
        Vector2[] uv = currentMesh.uv;
        Vector3[] newVerticies = currentMesh.vertices;
        int[] triangles = currentMesh.triangles;
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 0);
        uv[newVerticies.Length - 1] = new Vector2((verticies.Count - 1) % 2, 0);
        uv[newVerticies.Length - 2] = new Vector2((verticies.Count - 1) % 2, 0);
        for(int i=0; i<verticies.Count; i++)
        {
            newVerticies[2 * i + 1] = verticies[i] ;
            Vector2 proj = start + ((verticies[i].x - start.x) / dir.x) * dir;
            newVerticies[2 * i] = proj;
        }
        for (int i = 1; i < verticies.Count - 1; i++)
        {
            uv[2 * i + 1] = new Vector2(i % 2, 1);
            uv[2 * i + 2] = new Vector2(i % 2, 0);
        }
        for(int i=0; i<verticies.Count-1; i++)
        {
            
            triangles[6 * i] = 2*i;
            triangles[6 * i + 1] = 2 * i + 1;
            triangles[6 * i + 2] = 2 * i + 2;
            triangles[6 * i + 3] = 2 * i + 1;
            triangles[6 * i + 4] = 2 * i + 3;
            triangles[6 * i + 5] = 2 * i + 2;
        }
        currentMesh.vertices = newVerticies;
        currentMesh.triangles = triangles;
        currentMesh.uv = uv;
        GetComponent<MeshFilter>().mesh = currentMesh;
    }
    void GetVertecies()
    {
        verticies.Clear();
        for(int i=0; i<m_net.m_segments.Length; i++)
        {
            verticies.Add(m_net.m_segments[i].GetLeftPoint() - new Vector2(transform.position.x, transform.position.y));
        }
        verticies.Add(m_net.m_segments[m_net.m_segments.Length - 1].GetRightPoint() - new Vector2(transform.position.x, transform.position.y));
    }
	void Update () 
    {
        DrawMesh();
	}
    /*void OnDrawGizmos()
    {
        GetVertecies();
        Debug.Log(verticies.Count);
        Gizmos.color = Color.red;
        Vector2 start = verticies[0] + new Vector2(transform.position.x, transform.position.y);
        Vector2 finish = verticies[verticies.Count - 1] + new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = finish - start;
        for (int i = 0; i < verticies.Count; i++)
        {
            
            Vector2 proj = start + (( verticies[i].x+transform.position.x - start.x) / dir.x) * dir;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(proj, 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(verticies[i]+new Vector2(transform.position.x, transform.position.y), 0.1f);
        }
    }*/
    
}
