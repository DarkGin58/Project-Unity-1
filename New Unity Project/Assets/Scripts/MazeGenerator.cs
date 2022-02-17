using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MazeCell
{
    public int xPos;
    public int yPos;

    public bool WallLeft = true;
    public bool WallBottom = true;
    public bool Enemy = true;

    public bool Visited = false;

}
public class MazeGenerator
{
    public int Width = 20;
    public int Height = 20;

    public MazeCell[,] GenerateMaze()
    {
        MazeCell[,] maze = new MazeCell[Width, Height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeCell { xPos = x, yPos = y };
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, Height - 1].WallLeft = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width - 1, y].WallBottom = false;
        }

        BackTrack(maze);


        return maze;
    }

    private void BackTrack(MazeCell[,] maze)
    {
        MazeCell current = maze[0, 0];
        current.Visited = true;

        Stack<MazeCell> stack = new Stack<MazeCell>();
        do
        {
            List<MazeCell> unvisitedNeighbours = new List<MazeCell>();

            int x = current.xPos;
            int y = current.yPos;


            //Проверка соседей
            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeCell a, MazeCell b)
    {
        if (a.xPos == b.xPos)
        {
            if (a.yPos > b.yPos) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.xPos > b.xPos) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }
}
