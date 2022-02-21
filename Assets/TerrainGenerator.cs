using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TerrainGenerator : MonoBehaviour
{
    Terrain terrain;
    TerrainData terrainData;
    public float roughness = 2.0f;
    public float heightDampenerPower = 2.0f;

    // Perlin noise
    public float perlinXScale = 0.01f;
    public float perlinYScale = 0.01f;
    public int perlinXOffset = 0;
    public int perlinYOffset = 0;
    public int perlinOctaves = 2;
    public float perlinPersistance = 8;
    public float perlinHeightScale = 0.09f;

    // Smooth Terrain
    public int smoothAmount = 100;

    int width = 1000;
    int depth = 1000;
    public GameObject redCube;
    public GameObject greenCube;

    private void Awake()
    {
        Debug.Log("Initializing Terrain Data");
        terrain = GetComponent<Terrain>();
        terrainData = Terrain.activeTerrain.terrainData;

        MidPointDisplacement();
        Perlin();
        SmoothTerrain();
        StartCoroutine(GenerateRoads());
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        float[,] resetHeights = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
        terrainData.SetHeights(0, 0, resetHeights);
    }

    float[,] GetHeightMap()
    {
        return terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
    }

    public void MidPointDisplacement()
    {
        float[,] heightMap = GetHeightMap();
        int width = terrainData.heightmapResolution - 1;
        int squareSize = width;
        float height = (float)squareSize / 2.0f * 0.01f;

        float heightDampener = (float)Mathf.Pow(heightDampenerPower, -1 * roughness);

        int cornerX, cornerY;
        int midX, midY;
        int pmidXL, pmidXR, pmidYU, pmidYD;

        //heightMap[0, 0] = UnityEngine.Random.Range(0f, 0.2f);
        //heightMap[0, terrainData.heightmapResolution - 2] = UnityEngine.Random.Range(0f, 0.2f);
        //heightMap[terrainData.heightmapResolution - 2, 0] = UnityEngine.Random.Range(0f, 0.2f);
        //heightMap[terrainData.heightmapResolution - 2, terrainData.heightmapResolution - 2] = UnityEngine.Random.Range(0f, 0.2f);

        while (squareSize > 0)
        {
            for (int x = 0; x < width; x += squareSize)
            {
                for (int y = 0; y < width; y += squareSize)
                {
                    cornerX = (x + squareSize);
                    cornerY = (y + squareSize);

                    midX = (int)(x + squareSize / 2.0f);
                    midY = (int)(y + squareSize / 2.0f);

                    heightMap[midX, midY] = (float)(
                        (heightMap[x, y] +
                        heightMap[cornerX, y] +
                        heightMap[x, cornerY] +
                        heightMap[cornerX, cornerY]) / 4.0f +
                        UnityEngine.Random.Range(-height, height));
                }
            }

            for (int x = 0; x < width; x += squareSize)
            {
                for (int y = 0; y < width; y += squareSize)
                {
                    cornerX = (x + squareSize);
                    cornerY = (y + squareSize);

                    midX = (int)(x + squareSize / 2.0f);
                    midY = (int)(y + squareSize / 2.0f);

                    pmidXR = (int)(midX + squareSize);
                    pmidYU = (int)(midY + squareSize);
                    pmidXL = (int)(midX - squareSize);
                    pmidYD = (int)(midY - squareSize);

                    if (pmidXL <= 0 || pmidYD <= 0 || pmidXR >= width - 1 || pmidYU >= width - 1) continue;

                    heightMap[midX, y] = (float)(
                        (heightMap[midX, midY] +
                        heightMap[x, y] +
                        heightMap[midX, pmidYD] +
                        heightMap[cornerX, y]) / 4.0f +
                        UnityEngine.Random.Range(-height, height));

                    heightMap[x, midY] = (float)(
                        (heightMap[x, cornerY] +
                        heightMap[midX, midY] +
                        heightMap[x, y] +
                        heightMap[pmidXL, midY]) / 4.0f +
                        UnityEngine.Random.Range(-height, height));

                    heightMap[midX, cornerY] = (float)(
                        (heightMap[midX, pmidYU] +
                        heightMap[cornerX, cornerY] +
                        heightMap[midX, midY] +
                        heightMap[x, cornerY]) / 4.0f +
                        UnityEngine.Random.Range(-height, height));

                    heightMap[cornerX, midY] = (float)(
                        (heightMap[cornerX, cornerY] +
                        heightMap[pmidXR, midY] +
                        heightMap[cornerX, y] +
                        heightMap[midX, midY]) / 4.0f +
                        UnityEngine.Random.Range(-height, height));
                }
            }

            squareSize = (int)(squareSize / 2.0f);
            height *= heightDampener;
        }


        terrainData.SetHeights(0, 0, heightMap);
    }

    public void Perlin()
    {
        float[,] heightMap = GetHeightMap();

        for (int y = 0; y < terrainData.heightmapResolution; y++)
        {
            for (int x = 0; x < terrainData.heightmapResolution; x++)
            {
                heightMap[x, y] += Utils.fBM(x * perlinXScale, y * perlinYScale, perlinOctaves, perlinPersistance, perlinXOffset, perlinYOffset) * perlinHeightScale;
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    public void SmoothTerrain()
    {
        float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
        float smoothProgress = 0;
        EditorUtility.DisplayProgressBar("Smoothing Terrain", "Progress", smoothProgress);

        for (int i = 0; i < smoothAmount; i++)
        {
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                for (int x = 0; x < terrainData.heightmapResolution; x++)
                {
                    float averageHeight = 0;

                    if (y == 0 && x > 0 && x < terrainData.heightmapResolution - 2)
                    {
                        averageHeight = (
                            heightMap[x, y] +
                            heightMap[x + 1, y] +
                            heightMap[x - 1, y] +
                            heightMap[x + 1, y + 1] +
                            heightMap[x - 1, y + 1] +
                            heightMap[x, y + 1]) / 6.0f;
                    }

                    else if (x == 0 && y > 0 && y < terrainData.heightmapResolution - 2)
                    {
                        averageHeight = (
                            heightMap[x, y] +
                            heightMap[x + 1, y] +
                            heightMap[x + 1, y + 1] +
                            heightMap[x + 1, y - 1] +
                            heightMap[x, y + 1] +
                            heightMap[x, y - 1]) / 6.0f;
                    }
                    else if (x == terrainData.heightmapResolution - 2 && y > terrainData.heightmapResolution - 2 && y < 0)
                    {
                        averageHeight = (
                            heightMap[x, y] +
                            heightMap[x - 1, y] +
                            heightMap[x - 1, y + 1] +
                            heightMap[x - 1, y - 1] +
                            heightMap[x, y + 1] +
                            heightMap[x, y - 1]) / 6.0f;
                    }
                    else if (y > 0 && x > 0 && y < terrainData.heightmapResolution - 2 && x < terrainData.heightmapResolution - 2)
                    {
                        averageHeight = (
                            heightMap[x, y] +
                            heightMap[x + 1, y] +
                            heightMap[x - 1, y] +
                            heightMap[x + 1, y + 1] +
                            heightMap[x - 1, y - 1] +
                            heightMap[x + 1, y - 1] +
                            heightMap[x - 1, y + 1] +
                            heightMap[x, y - 1] +
                            heightMap[x, y + 1]) / 9.0f;
                    }

                    heightMap[x, y] = averageHeight;
                }
            }
            smoothProgress++;
            EditorUtility.DisplayProgressBar("Smoothing Terrain", "Progress", smoothProgress / smoothAmount);
        }

        terrainData.SetHeights(0, 0, heightMap);
        EditorUtility.ClearProgressBar();
    }

    IEnumerator GenerateRoads()
    {
        int startPos = Random.Range(1, 500);
        int endPos = Random.Range(startPos + 250, width);

        Voronoi.GenerateVoronoi(50, startPos, endPos, endPos);

        //foreach (KeyValuePair<Vector2Int, int> location in Voronoi.locations)
        //{
        //    Instantiate(redCube, new Vector3(location.Key.x, 100, location.Key.y), Quaternion.identity);
        //}

        foreach (Vector2 roadPos in Voronoi.roadmap)
        {
            Instantiate(greenCube, new Vector3(roadPos.x, 100, roadPos.y), Quaternion.identity);
        }
        yield return null;
    }
}
