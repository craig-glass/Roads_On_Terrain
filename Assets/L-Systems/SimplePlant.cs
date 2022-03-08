using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

namespace DefaultNamespace
{
    public class SimplePlant : MonoBehaviour
    {

        /*
     * axiom = F
     * F -> FF+[+F-F-F]-[-F+F+F]
     * angle = 22.5
     */
        public GameObject road;

        StringBuilder sb;

        Dictionary<string, string> ruleset = new Dictionary<string, string>
        {
            {"A", "FFFFFFFFFF" },
            {"B", "--AAAAA" },
            {"C", "++AAAAA" },
            {"D", "[GH]AD" },
            {"E", "[HG]AE" },
            {"G", "--AA" },
            {"H", "++AA" },
            {"I", "[CG]AAI" },
            {"J", "[BH]AAJ" }
        };

        Dictionary<string, Action<Turtle>> commands = new Dictionary<string, Action<Turtle>>
        {
            {"F", turtle => turtle.Translate(new Vector3(0, 0, 2))},
            {"+", turtle => turtle.Rotate(new Vector3(0, 45f, 0)) },
            {"-", turtle => turtle.Rotate(new Vector3(0, -45f, 0)) },
            {"[", turtle => turtle.Push() },
            {"]", turtle => turtle.Pop() }
        };

        //Dictionary<string, string> ruleset = new Dictionary<string, string>
        //{
        //    {"Z", "A[C]B[D]AA[C]BB[D]" },
        //    {"A", "F+F+F+" },
        //    {"B", "f-f-f-" },
        //    {"C", "-" },
        //    {"D", "+"}
        //};

        //Dictionary<string, Action<Turtle>> commands = new Dictionary<string, Action<Turtle>>
        //{
        //    {"F", turtle => turtle.Translate(new Vector3(0.35f, 0, 1.5f))},
        //    {"f", turtle => turtle.Translate(new Vector3(-0.35f, 0, 1.5f))},
        //    {"+", turtle => turtle.Rotate(new Vector3(0, 25f, 0)) },
        //    {"-", turtle => turtle.Rotate(new Vector3(0, -25f, 0)) },
        //    {"[", turtle => turtle.Push() },
        //    {"]", turtle => turtle.Pop() }
        //};

        // Start is called before the first frame update
        void Start()
        {
            Vector3 pos;


            //if (Voronoi.testdict.TryGetValue(new Vector2(1, 2), out List<Vector2> value))
            //{               
            //    pos = new Vector3(value[0].x, 50, value[0].y);
            //    Debug.Log("pos = " + pos);
            //}


            foreach (KeyValuePair<Vector2, Quaternion> edge in Voronoi.edges)
            {
                sb = Axiom(sb);
                bool tooclose = false;

                Debug.Log("axiom = " + sb);
                foreach(KeyValuePair<Vector2, Quaternion> otherEdge in Voronoi.edges)
                {
                    if (edge.Key == otherEdge.Key) continue;

                    if (Vector3.Distance(edge.Key, otherEdge.Key) < 300)
                    {
                        tooclose = true;
                    }
                }
               
                if (tooclose)
                {
                    Debug.Log("toocloase = " + tooclose);
                    continue;
                }
                pos = new Vector3(edge.Key.x, 50, edge.Key.y);
                var lSystem = new LSystem(sb, ruleset, commands, pos, edge.Value);

                Debug.Log("edge: " + edge.ToString());
                //Debug.Log(lSystem.GenerateSentence());
                //Debug.Log(lSystem.GenerateSentence());
                //Debug.Log(lSystem.GenerateSentence());
                //Debug.Log(lSystem.GenerateSentence());
                //Debug.Log(lSystem.GenerateSentence());
                //Debug.Log(lSystem.GenerateSentence());
                //Debug.Log(lSystem.GenerateSentence());
                //Debug.Log(lSystem.GenerateSentence());
                //Debug.Log(lSystem.GenerateSentence());
                //Debug.Log(lSystem.GenerateSentence());
                lSystem.GenerateSentence();
                lSystem.GenerateSentence();
                lSystem.GenerateSentence();
                lSystem.GenerateSentence();
                lSystem.GenerateSentence();
                lSystem.GenerateSentence();
                lSystem.GenerateSentence();
                lSystem.GenerateSentence();
                lSystem.GenerateSentence();
                StartCoroutine(lSystem.DrawSystem());
            }

        }

        StringBuilder Axiom(StringBuilder sb)
        {
            
            if (UnityEngine.Random.Range(0, 100) < 50)
            {
                sb = new StringBuilder("-");
                int index = 0;
            Loop:
                if (UnityEngine.Random.Range(0, 100) > 50)
                {
                    sb.Append("--AAAAAAAAAA");
                }
                else
                {
                    sb.Append("++AAAAAAAAAA");
                }
                sb.Append("[");
                if (UnityEngine.Random.Range(0, 100) < 50)
                {
                    sb.Append("[I]J");
                }
                else 
                {
                    sb.Append("[J]I");
                }
          
                sb.Append("]AA");
                while (index < UnityEngine.Random.Range(0, 8))
                {
                    index++;
                    goto Loop;

                }
            }
            else if (UnityEngine.Random.Range(0, 100) <= 100)
            {
                sb = new StringBuilder("+");
                int index = 0;
            Loop:
                if (UnityEngine.Random.Range(0, 100) > 50)
                {
                    sb.Append("--AAAAA");
                }
                else
                {
                    sb.Append("++AAAAA");
                }
                sb.Append("[");
                if (UnityEngine.Random.Range(0, 100) < 50)
                {
                    sb.Append("[J]I");
                }
                else 
                {
                    sb.Append("[I]J");
                }
                
                sb.Append("]AA");
                while (index < UnityEngine.Random.Range(0, 8))
                {
                    index++;
                    goto Loop;
                }
            }


            return sb;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

