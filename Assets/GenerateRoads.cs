using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRoads : MonoBehaviour
{
    public GameObject crawler;
    public GameObject road;
    Vector3 position;
    float offset = 0.5f;
    float[,] heightmap;
    Terrain terrain;

    // Start is called before the first frame update
    void Awake()
    {
        terrain = Terrain.activeTerrain;
        
        for (int z = 0; z < 5; z++)
        {
            for (int x = 0; x < 5; x++)
            {
                position = new Vector3(x + offset, 200, z);
                Instantiate(road, position , Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
