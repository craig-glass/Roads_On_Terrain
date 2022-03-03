using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DefaultNamespace;

/*
 * axiom = F
 * F -> FF+[+F-F-F]-[-F+F+F]
 * angle = 22.5
 */


public class LSystem
{
    string sentence;
    Dictionary<string, string> ruleset;
    Dictionary<string, Action<Turtle>> turtleCommands;
    Turtle turtle;

    public LSystem(string axiom, Dictionary<string, string> ruleset, Dictionary<string, Action<Turtle>> turtleCommands, Vector3 initialPos)
    {
        sentence = axiom;
        this.ruleset = ruleset;
        this.turtleCommands = turtleCommands;

        turtle = new Turtle(initialPos);
    }

    public void DrawSystem()
    {
        foreach (var instruction in sentence)
        {
            if (turtleCommands.TryGetValue(instruction.ToString(), out var command))
            {
                command(turtle);
            }
        }
    }

    public string GenerateSentence()
    {
        sentence = IterateSentence(sentence);
        return sentence;
    }

    string IterateSentence(string oldSentence)
    {
        var newSentence = "";

        //generate/iterate

        foreach (var c in oldSentence)
        {
            if (ruleset.TryGetValue(c.ToString(), out string replacement))
            {
                newSentence += replacement;
            }
            else
            {
                newSentence += c;
            }
        }

        return newSentence;

    }
}
