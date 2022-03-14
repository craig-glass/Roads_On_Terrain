using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

namespace DefaultNamespace
{
    public class DrawRoundabout : MonoBehaviour
    {

        /*
     * axiom = F
     * F -> FF+[+F-F-F]-[-F+F+F]
     * angle = 22.5
     */


        StringBuilder sb = new StringBuilder();
        int iterations;
        string axiom;

     

        Dictionary<string, string> rulesetCircle = new Dictionary<string, string>
        {
            {"Z", "A[C]B[D]AA[C]BB[D]" },
            {"Y", "AAAAA" },
            {"A", "F+F+F+" },
            {"B", "f-f-f-" },
            {"C", "-" },
            {"D", "+"}
        };

        Dictionary<string, Action<Turtle>> commandsCircle = new Dictionary<string, Action<Turtle>>
        {
            {"F", turtle => turtle.Translate(new Vector3(0.32f, 0, 1.5f))},
            {"f", turtle => turtle.Translate(new Vector3(-0.35f, 0, 1.5f))},
            {"+", turtle => turtle.Rotate(new Vector3(0, 24f, 0)) },
            {"-", turtle => turtle.Rotate(new Vector3(0, -25f, 0)) },
            {"[", turtle => turtle.Push() },
            {"]", turtle => turtle.Pop() }
        };

        // Start is called before the first frame update
        void Start()
        {
            Vector3 pos;

            iterations = 3;

            sb.Append("Y");

            Debug.Log("axiom = " + sb);

            pos = new Vector3(0, 0, 0);
            var lSystem = new LSystem(sb, rulesetCircle, commandsCircle, pos, Quaternion.identity);
            while (iterations > 0)
            {
                lSystem.GenerateSentence();
                iterations--;
            }


            StartCoroutine(lSystem.DrawSystem());


        }




    }
}

