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

        Dictionary<string, string> roundaboutRuleset = new Dictionary<string, string>
        {
            {"A", "R[E]S++[D]T++D" },
            {"B", "FFFFF" },
            {"C", "111" },
            {"D", "2p[P2p[++OC[--OC]C[--OC]Co[--OC]]]" },
            {"E", "Co[--OC[++OC]Co[++OC]]OCo[++OC]" }
        };

        Dictionary<string, Action<Turtle>> roundaboutCommands = new Dictionary<string, Action<Turtle>>
        {
            {"R", turtle => turtle.TranslateRoundabout(new Vector3(0, 0, 14.3f)) },
            {"S", turtle => turtle.ExitRoundabout(new Vector3(34.3f, 0, -14.3f)) },
            {"T", turtle => turtle.ExitRoundabout(new Vector3(34.3f, 0, -34.3f)) },
            {"+", turtle => turtle.Rotate(new Vector3(0, 45f, 0)) },
            {"-", turtle => turtle.Rotate(new Vector3(0, -45f, 0)) },
            {"F", turtle => turtle.Translate(new Vector3(0, 0, 2)) },
            {"1", turtle => turtle.Translate(new Vector3(0, 0, 20)) },
            {"2", turtle => turtle.Translate(new Vector3(0, 0, 60)) },
            {"O", turtle => turtle.TranslateOffset(new Vector3(0, 0, 10)) },
            {"o", turtle => turtle.TranslateOffset(new Vector3(0, 0, -10)) },
            {"P", turtle => turtle.TranslateOffset(new Vector3(0, 0, 30)) },
            {"p", turtle => turtle.TranslateOffset(new Vector3(0, 0, -30)) },
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
            var lSystem = new LSystem(sb, roundaboutRuleset, roundaboutCommands, pos, Quaternion.identity);


            while (iterations > 0)
            {
                // Debug.Log(lSystem.GenerateSentence());
                lSystem.GenerateSentence();
                iterations--;
            }


            lSystem.DrawSystem();        

        }

      

    }
}

