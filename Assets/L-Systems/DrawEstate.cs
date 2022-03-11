using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

namespace DefaultNamespace
{
    public class DrawEstate : MonoBehaviour
    {

        /*
     * axiom = F
     * F -> FF+[+F-F-F]-[-F+F+F]
     * angle = 22.5
     */
        

        StringBuilder sb;
        int iterations;

        Dictionary<string, string> ruleset = new Dictionary<string, string>
        {
            {"A", "F" },
            {"B", "AAAAA" },
            {"C", "f" },
            {"D", "[GH]AD" },
            {"E", "[HG]AE" },
            {"G", "AA" },
            {"H", "AA" },
            {"I", "C[--G]I" },
            {"J", "C[++G]J" }
        };

        Dictionary<string, Action<Turtle>> commands = new Dictionary<string, Action<Turtle>>
        {
            {"F", turtle => turtle.Translate(new Vector3(0, 0, 20))},
            {"f", turtle => turtle.Translate(new Vector3(0, 0, 60)) },
            {"O", turtle => turtle.TranslateOffset(new Vector3(0, 0, 10)) },
            {"o", turtle => turtle.TranslateOffset(new Vector3(0, 0, -10)) },
            {"P", turtle => turtle.TranslateOffset(new Vector3(0, 0, 30)) },
            {"p", turtle => turtle.TranslateOffset(new Vector3(0, 0, -30)) },
            {"+", turtle => turtle.Rotate(new Vector3(0, 45f, 0)) },
            {"-", turtle => turtle.Rotate(new Vector3(0, -45f, 0)) },
            {"[", turtle => turtle.Push() },
            {"]", turtle => turtle.Pop() }
        };

        Dictionary<string, string> rulesetCircle = new Dictionary<string, string>
        {
            {"Z", "A[C]B[D]AA[C]BB[D]" },
            {"A", "F+F+F+" },
            {"B", "f-f-f-" },
            {"C", "-" },
            {"D", "+"}
        };

        Dictionary<string, Action<Turtle>> commandsCircle = new Dictionary<string, Action<Turtle>>
        {
            {"F", turtle => turtle.Translate(new Vector3(0.35f, 0, 1.5f))},
            {"f", turtle => turtle.Translate(new Vector3(-0.35f, 0, 1.5f))},
            {"+", turtle => turtle.Rotate(new Vector3(0, 25f, 0)) },
            {"-", turtle => turtle.Rotate(new Vector3(0, -25f, 0)) },
            {"[", turtle => turtle.Push() },
            {"]", turtle => turtle.Pop() }
        };

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
                iterations = UnityEngine.Random.Range(3, 8);

                sb = Axiom(sb);
                bool tooclose = false;

                Debug.Log("axiom = " + sb);
                foreach (KeyValuePair<Vector2, Quaternion> otherEdge in Voronoi.edges)
                {
                    if (edge.Key == otherEdge.Key) continue;

                    if (Vector3.Distance(edge.Key, otherEdge.Key) < 400)
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


                while (iterations > 0)
                {
                    // Debug.Log(lSystem.GenerateSentence());
                    lSystem.GenerateSentence();
                    iterations--;
                }


                StartCoroutine(lSystem.DrawSystem());
            }

        }

        StringBuilder Axiom(StringBuilder sb)
        {
            sb = new StringBuilder();
            int roadBlocks = iterations - 1;
            int lengthOfBlocks = 8 - roadBlocks;
            StringBuilder rulesetI = new StringBuilder();
            StringBuilder rulesetG = new StringBuilder();
            int index = UnityEngine.Random.Range(1, 6);
            int gAmount = 0;
            bool firstLoop = true;

            roadBlocks *= lengthOfBlocks;

            gAmount = UnityEngine.Random.Range(1, 4);
            while (gAmount > 0)
            {
                rulesetG.Append("AA");
                gAmount--;
            }

            while (lengthOfBlocks > 0)
            {
                rulesetI.Append("C");
                lengthOfBlocks--;
            }

            ruleset["I"] = rulesetI + "[--G]I";
            ruleset["J"] = rulesetI + "[++G]J";

            if (UnityEngine.Random.Range(0, 100) < 50)
            {

                sb.Append("[");
            Loop:
                Debug.Log("loop = " + index);
                if (firstLoop)
                {
                    sb.Append("[");
                    firstLoop = false;
                }
                else
                {
                    sb.Append("O[");
                }

                ruleset["I"] = rulesetI + "P[--o" + rulesetG + "]pI";

                sb.Append("++pI");

                sb.Append("]o" + rulesetG);

                while (index > 0)
                {

                    index--;
                    goto Loop;

                }
                sb.Append("O[++p");
                while (roadBlocks > 0)
                {
                    sb.Append("C");
                    roadBlocks--;
                }
                sb.Append("]");
                sb.Append("]");

                // next?
                sb.Append("-");
                sb.Append("CCC");

            }
            else if (UnityEngine.Random.Range(0, 100) <= 100)
            {
                sb.Append("[");
            Loop:
                Debug.Log("loop = " + index);
                if (firstLoop)
                {
                    sb.Append("[");
                    firstLoop = false;
                }
                else
                {
                    sb.Append("O[");
                }
         
                Debug.Log("G = " + rulesetG);
                ruleset["J"] = rulesetI + "P[++o" + rulesetG + "]pJ";


                sb.Append("--pJ");

                Debug.Log("sb G = " + rulesetG);
                sb.Append("]o" + rulesetG);

                while (index > 0)
                {

                    index--;
                    goto Loop;
                }
                sb.Append("O[--p");
                while (roadBlocks > 0)
                {
                    sb.Append("C");
                    roadBlocks--;
                }
                sb.Append("]");
                sb.Append("]");

                // next?
            }

            return sb;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

