using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//unity triangles are drawn clockwise. this indicates the direction they are facing


[RequireComponent(typeof(MeshFilter))]

public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;


    public float xTransform = 0.3f;
    public float zTransform = 0.3f;
    float globalTransform = 4.2f;


    public int textureHeight = 1024;
    public int textureWidth = 1024;

    Vector2[] uv;
     

    void Start()
    {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; 


        CreateShape();
        UpdateMesh();


        /* 
        //genera un mesh collider alrededor del mesh generado
        MeshCollider meshCollider2 = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshCollider2.sharedMesh = mesh;
        */
    }



    public void CreateShape()
    {

  
        vertices = new Vector3[(xSize+1) * (zSize+1)];


        
        for(int actual = 0,  i = 0; i <= zSize; i++)
        {
            for(int j = 0; j <= xSize; j++)
            {
                float y = Mathf.PerlinNoise(j * xTransform, i * zTransform)*globalTransform;
                vertices[actual] = new Vector3(j,y,i);
                actual++;
            }

        }

        triangles = new int[xSize*zSize*6];
        int vert = 0;
        int tris = 0;


        for(int j = 0; j < zSize; j++)
        {
            for(int i = 0; i < xSize; i++)
            {    
            triangles[tris + 0] = vert + 0;
            triangles[tris + 1] = vert + xSize + 1;
            triangles[tris + 2] = vert + 1;
            triangles[tris + 3] = vert + 1;
            triangles[tris + 4] = vert + xSize + 1;
            triangles[tris + 5] = vert + xSize + 2;

            vert++;
            tris+=6;
            }
            vert++;

        }


        uv = new Vector2[vertices.Length];


        for(int actual = 0,  i = 0; i <= zSize; i++)
        {
            for(int j = 0; j <= xSize; j++)
            {
                uv[actual] = new Vector2((float)j/xSize, (float)i/zSize);
                actual++;
            }

        }


    }



    public void OnDrawGizmos()
    {

        if(vertices == null)
        {
            return; 
        }

        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }




    public void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;  
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
    }

}
