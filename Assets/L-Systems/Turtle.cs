using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{

    /*
     * F    Move forward by line length drawing a line
     * +    Turn left by turning angle
     * -    Turn right by turning angle
     * [    Push current drawing state onto stack
     * ]    Pop current drawing state from the stack
     */


    public class Turtle
    {
        class TurtleTransform
        {
            public Vector3 Position { get; }
            public Quaternion Orientation { get; }

            public TurtleTransform(Vector3 position, Quaternion orientation)
            {
                Position = position;
                Orientation = orientation;
            }
        }

        GameObject road;
        public Vector3 Position { get; private set; }
        public Quaternion Orientation { get; private set; }


        Stack<TurtleTransform> stack = new Stack<TurtleTransform>();

        public Turtle(Vector3 position, Quaternion angle)
        {
            Position = position;
            Orientation = angle;
            road = (GameObject)Resources.Load("Prefabs/Road10", typeof(GameObject));
        }

        public void Translate(Vector3 delta)
        {
            delta = Orientation * delta;
            
            //Debug.DrawLine(Position, Position + delta, Color.black, 100f);
            Position += delta;
            GameObject.Instantiate(road, Position, Orientation);
        }

        public void TranslateOffset(Vector3 delta)
        {
            delta = Orientation * delta;
            Position += delta;
        }

        public void Rotate(Vector3 delta) => Orientation = Quaternion.Euler(Orientation.eulerAngles + delta);

        public void Push() => stack.Push(new TurtleTransform(Position, Orientation));

        public void Pop()
        {
            var poppedTransform = stack.Pop();
            Position = poppedTransform.Position;
            Orientation = poppedTransform.Orientation;
        }

       
    }

}
