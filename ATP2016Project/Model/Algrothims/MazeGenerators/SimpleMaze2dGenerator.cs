using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.MazeGenerators
{
    class SimpleMaze2dGenerator : AMazeGenerator
    {
        /// <summary>
        /// build the maze 2d in a simple way,
        /// always start from the left side and have options to move (up,down,right), the goalPosition is in the right side
        /// </summary>
        /// <param name="x">dim X of the maze2d</param>
        /// <param name="y">dim Y of the maze2d</param>
        /// <param name="z">dim Z of the maze2d</param>
        /// <returns>a generated 2dMaze</returns>
        public override Maze generate(int x, int y, int z)
        {
            Position currentWall;
            Position startPosition = new Position(0, 0, 0);
            Maze2d maze2d = new Maze2d(x, y, z);
            maze2d.setGoalPosition();
            Position goalPosition = maze2d.getGoalPosition();
            startPosition.setPosition(goalPosition);

            while (startPosition.Y != maze2d.MY * 2 - 2)
            {
                currentWall = ((Position)MoveTONextPosition(startPosition, maze2d));//get the wall to break
                currentWall.setFatherPosition(startPosition);
                maze2d.setMazeWallsCell(currentWall.X, currentWall.Y);//break the wall
                startPosition.setPosition(MoveToNextCell(startPosition, currentWall));//get the prev position and the wall and move to the next cell

            }
            maze2d.setStartPosition(startPosition);
            breakMoreWalls(maze2d);
            return maze2d;
        }

        /// <summary>
        /// move to the next cell, from the current wall
        /// </summary>
        /// <param name="p">the current position you in</param>
        /// <param name="maze">get the 2d Maze</param>
        /// <returns>return the cell after movment</returns>
        public Position MoveTONextPosition(Position p, Maze2d maze)
        {
            int count = 0;
            Random ran = new Random();
            int num_position;
            Position pToMove = new Position(0, 0, 0);
            ArrayList positions = new ArrayList();
            Position up = new Position(0, 0, 0);
            Position down = new Position(0, 0, 0);
            Position right = new Position(0, 0, 0);
            bool setDown = false;
            bool setUp = false;
            bool setRight = false;
            if (p.X + 1 < maze.MX * 2 - 1)
            {
                down.setPosition(new Position(p.X + 1, p.Y, p.Z)); // move to the down cell
                setDown = true;
            }
            if (p.X - 1 >= 0)
            {
                up.setPosition(new Position(p.X - 1, p.Y, p.Z)); // move to the up cell
                setUp = true;
            }
            if (p.Y + 1 < maze.MY * 2 - 1)
            {
                right.setPosition(new Position(p.X, p.Y + 1, p.Z)); // move to the right cell
                setRight = true;
            }
            if (setUp && checkIfFatherNotEqals(p, up)) // check if we went up and not visited before, and add it to list
                positions.Add(up);
            if (setDown && checkIfFatherNotEqals(p, down))
                positions.Add(down);
            if (setRight && checkIfFatherNotEqals(p, right)) // add it to optionToMove
                positions.Add(right);
            count = positions.Count;
            num_position = ran.Next(0, count - 1);
            pToMove.setPosition((Position)positions[num_position]);
            pToMove.setFatherPosition(p);
            return pToMove;
        }

        /// <summary>
        /// check what move we did (up,down,right) and then move one more step in the same way 
        /// before we break the wall and now we want to move to the cell. and check again the neigbors to move of the cell
        /// 
        /// </summary>
        /// <param name="prev">get the prev position</param>
        /// <param name="wall">get the position of the wall that was broken</param>
        /// <returns>the new position of the cell</returns>
        public Position MoveToNextCell(Position prev, Position wall)
        {
            Position new_p = new Position(0, 0, 0);
            Position bigIn1;
            if (Math.Abs(prev.X - wall.X) == 1)// up or down
            {
                if (prev.X + 1 == wall.X) // down
                {
                    wall.setFatherPosition(prev);
                    bigIn1 = new Position(wall.X + 1, wall.Y, wall.Z);
                    bigIn1.setFatherPosition(wall);
                    new_p.setPosition(bigIn1);

                }
                else  // up
                {
                    wall.setFatherPosition(prev);
                    bigIn1 = new Position(wall.X - 1, wall.Y, wall.Z);
                    bigIn1.setFatherPosition(wall);
                    new_p.setPosition(bigIn1);
                }
            }
            else // right
            {
                wall.setFatherPosition(prev);
                bigIn1 = new Position(wall.X, wall.Y + 1, wall.Z);
                bigIn1.setFatherPosition(wall);
                new_p.setPosition(bigIn1);
            }
            return new_p;
        }

        /// <summary>
        /// check the father of the positions, if they have the ssame father
        /// </summary>
        /// <param name="curr"> the current position</param>
        /// <param name="other">the other position where we continue to move</param>
        /// <returns>true if the fathers of the positions are equal - ptherwise-false</returns>
        public bool checkIfFatherNotEqals(Position curr, Position other)
        {
            if (curr.Father == null)
                return true;
            else
            {
                if (curr.Father.equals(other))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// randomic function that break more walls
        /// </summary>
        /// <param name="maze2d">maze2d of walls</param>
        public void breakMoreWalls(Maze2d maze2d)
        {
            int numOfWallsToBreak;
            int dim_x_size = maze2d.MX;
            int dim_y_size = maze2d.MY;
            int size_all = dim_x_size * dim_y_size;
            numOfWallsToBreak = Convert.ToInt32(0.4 * size_all); // get How many walls to Break
            int x;
            int y;
            Random ran = new Random();
            while (numOfWallsToBreak != 0)
            {
                x = ran.Next(0, dim_x_size * 2 - 1);
                y = ran.Next(0, dim_y_size * 2 - 1);
                if (maze2d.getWallsCell(x, y) == 1)
                {
                    maze2d.setMazeWallsCell(x, y); // break the walls
                    numOfWallsToBreak--; // size--
                }
            }
        }
    }
}
