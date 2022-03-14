using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

namespace DefaultNamespace
{
    public class DrawRoundabouts : MonoBehaviour
    {

        /*
     * axiom = F
     * F -> FF+[+F-F-F]-[-F+F+F]
     * angle = 22.5
     */


        StringBuilder sb = new StringBuilder();
        int iterations;

        Dictionary<string, string> ruleset = new Dictionary<string, string>
        {
            {"A", "R[C]S++[D]" },
            {"B", "FFFFF" },
            {"C", "111" },
            {"D", "2[-B]2[++B]2" }
        };

        Dictionary<string, Action<Turtle>> commands = new Dictionary<string, Action<Turtle>>
        {
            {"R", turtle => turtle.TranslateRoundabout(new Vector3(0, 0, 14.3f)) },
            {"S", turtle => turtle.ExitRoundabout(new Vector3(34.3f, 0, -14.3f)) },
            {"+", turtle => turtle.Rotate(new Vector3(0, 45f, 0)) },
            {"-", turtle => turtle.Rotate(new Vector3(0, -45f, 0)) },
            {"F", turtle => turtle.Translate(new Vector3(0, 0, 2)) },
            {"1", turtle => turtle.Translate(new Vector3(0, 0, 20)) },
            {"2", turtle => turtle.Translate(new Vector3(0, 0, 60)) },
            {"O", turtle => turtle.TranslateOffset(new Vector3(0, 0, 10)) },
            {"[", turtle => turtle.Push() },
            {"]", turtle => turtle.Pop() },
        };

      
        // Start is called before the first frame update
        void Start()
        {
            Vector3 pos;
            iterations = 3;

            sb.Append("A");
            Debug.Log("sb = " + sb);

            pos = new Vector3(0, 0, 0);
            var lSystem = new LSystem(sb, ruleset, commands, pos, Quaternion.identity);


            while (iterations > 0)
            {
                // Debug.Log(lSystem.GenerateSentence());
                lSystem.GenerateSentence();
                iterations--;
            }


            StartCoroutine(lSystem.DrawSystem());        

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

    }
}

