using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadVoronoi : MonoBehaviour
{
    int width = 1000;
    int depth = 1000;
    public GameObject redCube;
    public GameObject greenCube;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(GenerateRoads());
    }

    // Update is called once per frame
    void Update()
    {
        
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
