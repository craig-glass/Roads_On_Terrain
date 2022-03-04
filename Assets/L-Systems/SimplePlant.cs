using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        string axiom = "ABABAB";

        Dictionary<string, string> ruleset = new Dictionary<string, string>
        {
            {"A", "FF[++FFF--FFF]" },
            {"B", "[--FFF++FFF]C" },
            {"C", "FFABC" }
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
            Vector3 pos = new Vector3();
            if (Voronoi.testdict.TryGetValue(new Vector2(1, 2), out List<Vector2> value))
            {               
                pos = new Vector3(value[0].x, 50, value[0].y);
                Debug.Log("pos = " + pos);
            }
            var lSystem = new LSystem(axiom, ruleset, commands, pos);
            Debug.Log(lSystem.GenerateSentence());            
            Debug.Log(lSystem.GenerateSentence());            
            Debug.Log(lSystem.GenerateSentence());            
            Debug.Log(lSystem.GenerateSentence());            
            Debug.Log(lSystem.GenerateSentence());            
            StartCoroutine(lSystem.DrawSystem());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

