using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;
    [Range(0,5)]
    public float offsetY;

    

    private void Start()
    {
        mesh = new Mesh();
        xSize = Random.Range(10, 60);
        zSize = Random.Range(10, 60);
        this.GetComponent<MeshFilter>().mesh = mesh;

        CreateShape() ;
        UpdateMesh();


        NavMeshController.instance.SetNavMesh();
      

    }

    private void Update()
    {
   
  
    }

    void UpdateVertices()
    {
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f + offsetY, z * .3f + offsetY) * 2f;

                vertices[i].y = y;
                i++;
            }
        }
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

 

        for (int i =0,z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f+offsetY, z * .3f+ offsetY) * 2f;

                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        /*One Quat
        triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = xSize+1;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = xSize + 1;
        triangles[5] = xSize + 2; 
    
        */
        triangles = new int[xSize*zSize*6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;


            }

            vert++;
        }



    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // Just for lighting stuffs

        mesh.RecalculateBounds();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
