using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Voronoi 
{
    public static int[,] voronoiMap = null;
    public static List<Vector2> roadmap;
    public static float fBM(float x, float y, int octaves)
    {
        float total = 0;
        float frequency = 1;

        for (int i = 0; i < octaves; i++)
        {
            total += Mathf.PerlinNoise(x * frequency, y * frequency);
            frequency *= 2;
        }

        return total / (float)octaves;
    }
    public static Dictionary<Vector2Int, int> locations = new Dictionary<Vector2Int, int>();
    public static void GenerateVoronoi(int numDistricts, int width, int height)
    {
        voronoiMap = new int[width, height];

        Terrain terrain = Terrain.activeTerrain;

        int i = 1;

         while (locations.Count < numDistricts)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (!locations.ContainsKey(new Vector2Int(x, y)))
            {
                locations.Add(new Vector2Int(x, y), i);                
                i++;
            }
        }

        int roadPlacement = 0;
        roadmap = new List<Vector2>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float distance = Mathf.Infinity;


                foreach (KeyValuePair<Vector2Int, int> val in locations)
                {
                    float distanceTo = Vector2Int.Distance(val.Key, new Vector2Int(x, y));

                    if (distanceTo < distance)
                    {
                        voronoiMap[x, y] = val.Value;
                        distance = distanceTo;
                    }
                }
                if (roadPlacement != voronoiMap[x, y])
                {
                    roadPlacement = voronoiMap[x, y];
                    roadmap.Add(new Vector2(x, y));
                }
                if (y != 0)
                {
                    if (roadPlacement != voronoiMap[x, y - 1])
                    {
                        roadPlacement = voronoiMap[x, y];
                        roadmap.Add(new Vector2(x, y));
                    }
                }

                //if (terrain.SampleHeight(new Vector3(x, terrain.terrainData.size.y, y)) < 20)
                //{
                //    float distance = Mathf.Infinity;


                //    foreach (KeyValuePair<Vector2Int, int> val in locations)
                //    {
                //        float distanceTo = Vector2Int.Distance(val.Key, new Vector2Int(x, y));

                //        if (distanceTo < distance)
                //        {
                //            voronoiMap[x, y] = val.Value;
                //            distance = distanceTo;
                //        }
                //    }
                //    if (roadPlacement != voronoiMap[x, y])
                //    {
                //        roadPlacement = voronoiMap[x, y];
                //        roadmap.Add(new Vector2(x, y));
                //    }
                //    if (y != 0)
                //    {
                //        if (roadPlacement != voronoiMap[x, y - 1])
                //        {
                //            roadPlacement = voronoiMap[x, y];
                //            roadmap.Add(new Vector2(x, y));
                //        }
                //    }
                //}
               
            }

        }
    }
}
