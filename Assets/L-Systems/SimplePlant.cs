using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace DefaultNamespace
{
    public class SimplePlant : MonoBehaviour
    {

        /*
     * axiom = F
     * F -> FF+[+F-F-F]-[-F+F+F]
     * angle = 22.5
     */


        string axiom = "F";

        Dictionary<string, string> ruleset = new Dictionary<string, string>
        {
            {"F", "FF+[+F-F-F]-[-F+F+F]" }
        };

        Dictionary<string, Action<Turtle>> commands = new Dictionary<string, Action<Turtle>>
        {
            {"F", turtle => turtle.Translate(new Vector3(0, 0.1f, 0))},
            {"+", turtle => turtle.Rotate(new Vector3(25f, 0, 0)) },
            {"-", turtle => turtle.Rotate(new Vector3(-25f, 0, 0)) },
            {"[", turtle => turtle.Push() },
            {"]", turtle => turtle.Pop() }
        };

        // Start is called before the first frame update
        void Start()
        {
            var lSystem = new LSystem(axiom, ruleset, commands, transform.position);
            lSystem.GenerateSentence();
            lSystem.GenerateSentence();
            lSystem.GenerateSentence();
            lSystem.DrawSystem();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

