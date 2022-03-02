using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testscript : MonoBehaviour
{
    List<Vector2> testlist;
    Dictionary<Vector2, List<Vector2>> testdict;

    // Start is called before the first frame update
    void Start()
    {
        Test();
        foreach (KeyValuePair<Vector2, List<Vector2>> t in testdict)
        {

            Debug.Log("key: " + t.Key);
            for (int i = 0; i < t.Value.Count; i++)
            {
                Debug.Log("value: " + t.Value[i]);
            }
        }

        //int testlistcount = 0;
        //while (testlistcount < testlist.Count)
        //{
        //    for (int i = 0; i < testlist[testlistcount].Count; i++)
        //    {
        //        Debug.Log("testlist[" + testlistcount + "][" + i + "] = " + testlist[testlistcount][i]);
        //    }
        //    testlistcount++;
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }



    void Test()
    {
        
        testdict = new Dictionary<Vector2, List<Vector2>>();

        for (int x = 0; x < 3; x++)
        {
            
            for (int z = 0; z < 3; z++)
            {
                if (x == 0 || x == 2)
                {
                    testlist = new List<Vector2>();
                    testlist.Add(new Vector2(11,22));
                }
                else
                {
                    testlist = new List<Vector2>();
                    testlist.Add(new Vector2(x, z));
                }

                
            }
            testdict.Add(new Vector2(x, 0), testlist);
        }
        
    }
}
