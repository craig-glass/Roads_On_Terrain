using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVertex : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector3[] normals;
    Vector3[] displacedVertices;
    Vector3[] worldspaceVertices;
    Terrain terrain;

    float startPosX = 500;
    float endPosX = 0;
    float startPosZ = 500;
    float endPosZ = 0;

    float lowestHeight = 600;

    // Start is called before the first frame update
    void Start()
    {
        terrain = Terrain.activeTerrain;
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        displacedVertices = new Vector3[vertices.Length];
        worldspaceVertices = new Vector3[vertices.Length];
        Vector3 pos = transform.position;
        pos.y = Terrain.activeTerrain.SampleHeight(transform.position);
        transform.position = pos;


        for (int i = 0; i < vertices.Length; i++)
        {

            displacedVertices[i] = transform.TransformPoint(vertices[i]);
            worldspaceVertices[i] = displacedVertices[i];

            if (worldspaceVertices[i].x < startPosX) startPosX = worldspaceVertices[i].x;

            if (worldspaceVertices[i].x > endPosX) endPosX = worldspaceVertices[i].x;

            if (worldspaceVertices[i].z < startPosZ) startPosZ = worldspaceVertices[i].z;

            if (worldspaceVertices[i].z > endPosZ) endPosZ = worldspaceVertices[i].z;

           

            displacedVertices[i].y = Terrain.activeTerrain.SampleHeight(displacedVertices[i]);
            displacedVertices[i].y += 0.25f;

            if (displacedVertices[i].y < lowestHeight) lowestHeight = displacedVertices[i].y;

            displacedVertices[i] = transform.InverseTransformPoint(displacedVertices[i]);
            
        }
        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
