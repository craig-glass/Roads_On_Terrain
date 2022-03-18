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
        GameObject house;
        public Vector3 Position { get; private set; }
        public Quaternion Orientation { get; private set; }


        Stack<TurtleTransform> stack = new Stack<TurtleTransform>();

        public Turtle(Vector3 position, Quaternion angle)
        {
            Position = position;
            Orientation = angle;
            
        }

        public void Translate(Vector3 delta)
        {
            house = (GameObject)Resources.Load("Prefabs/House");
            if (delta.z == 20)
            {
                road = (GameObject)Resources.Load("Prefabs/Road10", typeof(GameObject));
                Vector3 housePos = Orientation * new Vector3(10, -45, 0);
                housePos += Position;
                GameObject.Instantiate(house, housePos, Orientation);

                Vector3 housePosOpposite = Orientation * new Vector3(-10, -45, 0);
                housePosOpposite += Position;
                GameObject.Instantiate(house, housePosOpposite, Orientation);
            }
            else if (delta.z == 60)
            {
                road = (GameObject)Resources.Load("Prefabs/Road30", typeof(GameObject));
            }
            else
            {
                road = (GameObject)Resources.Load("Prefabs/RoadSquareOriginal");
            }

            
            delta = Orientation * delta;
            GameObject.Instantiate(road, Position, Orientation);
            
            
            //Debug.DrawLine(Position, Position + delta, Color.black, 100f);
            Position += delta;
        }

        public void TranslateRoundabout(Vector3 delta)
        {
            delta = Orientation * delta;
            road = (GameObject)Resources.Load("Prefabs/round");
            GameObject.Instantiate(road, Position, Orientation);
            Position += delta;
        }

        public void ExitRoundabout(Vector3 delta)
        {
            delta = Orientation * delta;
            Position += delta;
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
