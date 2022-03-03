using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Voronoi
{
    public static int[,] voronoiMap = null;
    public static List<List<Vector2>> roadmap;
    public static Vector2 direction;
    public static List<Quaternion> q;
    public static Dictionary<Vector2, List<Vector2>> testdict;
    public static List<Vector2> testlist;

    public class RoadCross
    {
        public int first;
        public int second;

    }


    public static Dictionary<RoadCross, List<Vector2Int>> roadsCross = new Dictionary<RoadCross, List<Vector2Int>>();

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

    public static Dictionary<int, Vector2Int> locations = new Dictionary<int, Vector2Int>();
    public static void GenerateVoronoi(int numDistricts, int startPos, int width, int height)
    {
        voronoiMap = new int[width, height];

        testdict = new Dictionary<Vector2, List<Vector2>>();
        q = new List<Quaternion>();

        List<Vector2> roads = new List<Vector2>();

        Terrain terrain = Terrain.activeTerrain;

        int i = 1;

        while (locations.Count < numDistricts)
        {
            int x = Random.Range(startPos, width);
            int y = Random.Range(startPos, height);

            if (!locations.ContainsValue(new Vector2Int(x, y)))
            {
                locations.Add(i, new Vector2Int(x, y));
                i++;
            }

        }

        int roadPlacement = 0;
        roadmap = new List<List<Vector2>>();
        bool firstIteration = true;

        for (int y = startPos; y < height; y++)
        {
            bool movedUp = true;
            for (int x = startPos; x < width; x++)
            {
                float distance = Mathf.Infinity;


                foreach (KeyValuePair<int, Vector2Int> val in locations)
                {
                    float distanceTo = Vector2Int.Distance(val.Value, new Vector2Int(x, y));

                    if (distanceTo < distance)
                    {
                        voronoiMap[x, y] = val.Key;
                        distance = distanceTo;

                    }
                }

                if (movedUp && voronoiMap[x - 1, y] == 0)
                {
                    roadPlacement = voronoiMap[x, y];
                    movedUp = false;
                }

                if (roadPlacement != voronoiMap[x, y] || roadPlacement != voronoiMap[x, y - 1] && voronoiMap[x, y - 1] != 0)
                {
                    if (firstIteration && roadPlacement != voronoiMap[x, y])
                    {
                        testlist = new List<Vector2>();
                        testdict.Add(new Vector2(roadPlacement, voronoiMap[x, y]), testlist);

                        direction = locations[roadPlacement] - locations[voronoiMap[x, y]];

                        direction = Vector2.Perpendicular(direction);
                        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

                        q.Add(Quaternion.Euler(0f, angle, 0f));

                        firstIteration = false;
                    }
                  
                    if (!testdict.ContainsKey(new Vector2(roadPlacement, voronoiMap[x, y])) && roadPlacement != voronoiMap[x, y])
                    {
                        testlist = new List<Vector2>();
                        testlist.Add(new Vector2(x, y));
                        testdict.Add(new Vector2(roadPlacement, voronoiMap[x, y]), testlist);

                        direction = locations[roadPlacement] - locations[voronoiMap[x, y]];

                        direction = Vector2.Perpendicular(direction);
                        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

                        q.Add(Quaternion.Euler(0f, angle, 0f));
                    }
                    else
                    {
                        if (testdict.TryGetValue(new Vector2(roadPlacement, voronoiMap[x, y]), out List<Vector2> value))
                        {
                            value.Add(new Vector2(x, y));
                        }
                        else if (testdict.TryGetValue(new Vector2(voronoiMap[x, y], voronoiMap[x, y - 1]), out List<Vector2> othervalue))
                        {
                            othervalue.Add(new Vector2(x, y));
                        }
                        else if (testdict.TryGetValue(new Vector2(voronoiMap[x, y - 1], voronoiMap[x, y]), out List<Vector2> thatvalue))
                        {
                            thatvalue.Add(new Vector2(x, y));
                        }

                    }
                
                    roadPlacement = voronoiMap[x, y];

                    roads.Add(new Vector2(x, y));
                    roadmap.Add(roads);

                }
            }
        }
    }
}
