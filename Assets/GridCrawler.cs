using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCrawler : MonoBehaviour
{
    public GameObject crawler;
    int width = 1000;
    int depth = 1000;
    Vector3Int crawlerPos = new Vector3Int(500, 0, 500);
    public GameObject road;
    int crawlerCount = 0;
    int crawlerMax = 2000;

    enum Movement 
    {
        Forward,
        Right,
        Left, 
        Back
    }

    Movement movement = Movement.Forward;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Crawl());
    }

    IEnumerator Crawl()
    {
        while (crawlerCount < crawlerMax)
        {
            crawlerCount++;      

            int direction = Random.Range(0, 4);
            Vector3Int moveDir;

            switch (direction)
            {
                case 0:
                    moveDir = new Vector3Int(0, 0, 12);
                    if (movement == Movement.Right) crawlerPos += new Vector3Int(4, 0, -4);
                    if (movement == Movement.Left) crawlerPos += new Vector3Int(-4, 0, -4);
                    Move(Movement.Forward, moveDir, Quaternion.identity);
                    break;

                case 1:
                    moveDir = new Vector3Int(12, 0, 0);
                    if (movement == Movement.Forward) crawlerPos += new Vector3Int(-4, 0, 4); 
                    if (movement == Movement.Back) crawlerPos += new Vector3Int(-4, 0, -4);
                    Move(Movement.Right, moveDir, Quaternion.Euler(0, 90, 0));
                    break;

                case 2:
                    moveDir = new Vector3Int(-12, 0, 0);
                    if (movement == Movement.Forward) crawlerPos += new Vector3Int(4, 0, 4);
                    if (movement == Movement.Back) crawlerPos += new Vector3Int(4, 0, -4);
                    Move(Movement.Left, moveDir, Quaternion.Euler(0, -90, 0));
                    break;

                case 3:
                    moveDir = new Vector3Int(0, 0, -12);
                    if (movement == Movement.Right) crawlerPos += new Vector3Int(4, 0, 4);
                    if (movement == Movement.Left) crawlerPos += new Vector3Int(-4, 0, 4);
                    Move(Movement.Back, moveDir, Quaternion.identity);
                    break;

            };           

            yield return null;
        }
        
    }

  

    void Move(Movement moveDirection, Vector3Int directionVector, Quaternion rotation)
    {
        movement = moveDirection;
        crawlerPos += directionVector;
        if (crawlerPos.z >= depth || crawlerPos.x >= width || crawlerPos.z <= 0 || crawlerPos.x <= 0 || crawlerCount == 300 || crawlerCount == 600 || crawlerCount == 900 || crawlerCount == 1200 || crawlerCount == 1500)
        {
            crawlerPos = new Vector3Int(Random.Range(10, 900), 0, Random.Range(10, 900));
            return;
        }

        crawler.transform.position = crawlerPos;
        Instantiate(road, crawler.transform.position, rotation);
    }
}
