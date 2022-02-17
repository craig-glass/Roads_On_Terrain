using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustRoadHeight : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    bool hitTerrain = false;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length - 1; i++)
        {

            Debug.Log("vertices[" + i + "] = " + vertices[i]);
            Vector3 vert = vertices[i];

            vert += transform.position + transform.lossyScale;
            Debug.Log("vert = " + vert);

            Debug.Log(Terrain.activeTerrain.SampleHeight(vert));
            vert.y = Terrain.activeTerrain.SampleHeight(vert);

            vert = transform.InverseTransformPoint(vert);
            Debug.Log("new vert = " + vert);

            vertices[i] = vert;


        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

       

    }
}
