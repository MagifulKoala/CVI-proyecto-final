using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//unity triangles are drawn clockwise. this indicates the direction they are facing


[RequireComponent(typeof(MeshFilter))]

public class MeshGenerator2 : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    public Gradient gradiente;

    float hMin;
    float hMax;

    public float xTransform = 0.3f;
    public float zTransform = 0.3f;
    float globalTransform = 4.2f;


    public int textureHeight = 1024;
    public int textureWidth = 1024;

    Color[] colors;
     

    void Start()
    {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh; 


        CreateShape();
        UpdateMesh();

        
        /*
        //genera un meshCollider que se ajusta al mesh creado
        MeshCollider meshCollider = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshCollider.sharedMesh = mesh;
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

                if( y > hMax)
                {
                    hMax = y;
                }
                if(y < hMin)
                {
                    hMin = y; 
                }

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


        colors = new Color[vertices.Length];


        for(int actual = 0,  i = 0; i <= zSize; i++)
        {
            for(int j = 0; j <= xSize; j++)
            {
                float h = Mathf.InverseLerp(hMin, hMax, vertices[i].y);
                colors[i] = gradiente.Evaluate(h);
                actual++;
            }

        }


    }




    public void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;  
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
    }

}

