using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ATP2016Project.Model.Algrothims.MazeGenerators
{
    class MyMaze3dGenerator : AMazeGenerator
    {
        /// <summary>
        /// build the 3d maze, according to the size of the maze. it calls to maze2d that build
        /// for every layer maze2d.
        /// </summary>
        /// <param name="x">dim X of the maze</param>
        /// <param name="y">dim Y of the maze</param>
        /// <param name="z">dim Z of the maze</param>
        /// <returns>maze 3d</returns>
        public override Maze generate(int x, int y, int z)
        {
            MyMaze3dGenerator g = new MyMaze3dGenerator();
            x = x / 2 + 1;
            y = y / 2 + 1;
            Maze3d maze3d = new Maze3d(x,y, z);
            for (int layer = 0; layer < maze3d.MZ; layer++)
            {
                Maze2d maze2d = new Maze2d(x, y, layer);
                g.generateMaze2d(maze2d);
                maze3d.MAZE3dArray.Add(maze2d);
                // maze3d.finalGoalPosition(maze2d.getStartPosition());
                // maze3d.getGoalPosition().Z = maze3d.MZ - 1;
                if (layer == 0)
                {
                    maze3d.setStartPosition(maze2d.getStartPosition());
                }
                if (layer == maze3d.MZ - 1)
                {
                    maze3d.finalGoalPosition(maze2d.getGoalPosition());
                    maze3d.getGoalPosition().Z = maze3d.MZ - 1;
                }
            }
            return maze3d;
        }

        /// <summary>
        /// build a maze2d that we can solve - break walls to get to the goal position
        /// </summary>
        /// <param name="maze"></param>
        /// <returns>maze2d</returns>
        /// 
        ////IMaze maze in function
        public void generateMaze2d(Maze2d maze2d)
        {
            
            int randomWallLocation;
            Position currentWall;
            Position pMaze;
            Position startPosition = new Position(0, 0, 0);
            positionsInMaze = new ArrayList();
            ArrayList neighborWallsList = new ArrayList();
            maze2d.setGoalPosition();
            Position goalPosition = maze2d.getGoalPosition();
            neighborWallsList.AddRange(maze2d.getOptionsToMove(goalPosition)); // add all the neighbors to neighborsWallList
            positionsInMaze.Add(goalPosition); // add the goalPosition to List
            while (neighborWallsList.Count != 0) // while there is positions in neighborsWallsPOsitions
            {
                
                randomWallLocation = ran.Next(0, neighborWallsList.Count - 1); // random and get 1 of the walls in the List
                currentWall = ((Position)neighborWallsList[randomWallLocation]);
                pMaze = getUnvisitedWallNeighborCell(currentWall); // get all the UnvisitedNeighbors of the current wall
                if (pMaze != null)
                {
                    startPosition.setPosition(pMaze); // cheange the Position
                    maze2d.setMazeWallsCell(currentWall.X, currentWall.Y); // change the cell to 0 - we visited it
                    if (!isPositionExist(pMaze, positionsInMaze))
                    {
                        positionsInMaze.Add(pMaze); // add it to positionInMAze - all the position we have visited
                    }
                    neighborWallsList.AddRange(filterExistNeighbors(neighborWallsList, maze2d.getOptionsToMove(pMaze)));

                }
                neighborWallsList.RemoveAt(randomWallLocation); // remove the current wall from the neighborWallsList
            }
            maze2d.setStartPosition(startPosition); // change the startPosition
        }

        /// <summary>
        /// filter the neighbors in the list, if it is exist already in the list of maze,
        /// dont add it to the new neighbor list (option to move)
        /// check if one of the option to move(neigbors of the current position) 
        /// are exist already in the list of the maze
        /// </summary>
        /// <param name="neighbors">neighbors that exist in the list of the maze</param>
        /// <param name="newNeighbors">neigbors of the current position</param>
        /// <returns>arraylist of option to move</returns>
        public ArrayList filterExistNeighbors(ArrayList neighbors, ArrayList newNeighbors)
        {
            ArrayList filteredNeighbors = new ArrayList();
            foreach (Position p in newNeighbors)
            {
                if (!isPositionExist(p, neighbors)) // if it isnt exist in the list of neighbors, add it to filterList
                    filteredNeighbors.Add(p);
            }
            return filteredNeighbors;
        }
    }
}
