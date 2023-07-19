using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum Direction
{
	Up=1,
	Left=2,
	Right=4
}

namespace Hitcode_RoomEscape
{

    public class Path : List<Direction> { }


    public class PathGenerator : Singleton<PathGenerator>
    {
        public Direction GetNewDirection(Direction allowed, float rnd)
        {
            Direction newd;
            int maxd = System.Enum.GetValues(typeof(Direction)).Length;
            int[] vals = (int[])System.Enum.GetValues(typeof(Direction));
            do
            {
                var t = Random.Range(0, maxd);
                newd = (Direction)vals[t];
            }
            while ((newd & allowed) == 0);
            return newd;
        }

        public Path GenerateRandomPath(int startx, int starty, int endx, int endy, double prob)
        {
            Path newpath = new Path();
            //Random rnd = new Random();
            var rnd = Random.Range(0, 1);

            int curx = startx; int cury = starty; Direction curd = Direction.Right;
            Direction newd = curd;

            while (!(curx == endx && cury == endy))
            {
                if (Random.Range(0.0f, 1.0f) <= prob) // let's generate a turn
                {

                    do
                    {
                        if (curx == endx) newd = GetNewDirection(Direction.Left | Direction.Up, rnd);
                        else if (cury == endy) newd = Direction.Right;
                        else if (curx <= 0) newd = GetNewDirection(Direction.Right | Direction.Up, rnd);
                        else newd = GetNewDirection(Direction.Right | Direction.Up | Direction.Left, rnd);

                    }
                    while ((newd | curd) == (Direction.Left | Direction.Right)); // excluding going back

                    newpath.Add(newd);
                    curd = newd;
                    switch (newd)
                    {
                        case Direction.Left:
                            curx--;
                            break;
                        case Direction.Right:
                            curx++;
                            break;
                        case Direction.Up:
                            cury++;
                            break;
                    }
                }

            }


            return newpath;
        }
    }
}