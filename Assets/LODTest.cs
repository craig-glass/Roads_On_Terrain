using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODTest : MonoBehaviour
{
    Terrain terrain;
    float distance;
    float prevdistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        terrain = Terrain.activeTerrain;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, Vector3.zero);
        if (distance > prevdistance)
        {
            Vector2 tilesize = terrain.terrainData.terrainLayers[0].tileSize;
             tilesize += new Vector2(0.1f * Time.deltaTime, 0.1f * Time.deltaTime);
            terrain.terrainData.terrainLayers[0].tileSize = tilesize;
        }
        prevdistance = distance;
        transform.Translate(0, 0, -2 * Time.deltaTime);
    }
}
