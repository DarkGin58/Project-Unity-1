using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MazeSpawner : MonoBehaviour
{

    public GameObject Cell;
    public Vector3 size = new Vector3(1, 1, 0);
    void Start()
    {
        MazeGenerator gen = new MazeGenerator();
        MazeCell[,] maze = gen.GenerateMaze();
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell c = Instantiate(Cell, new Vector3(x * size.x, y * size.y, y * size.z), Quaternion.identity).GetComponent<Cell>();
                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallBottom.SetActive(maze[x, y].WallBottom);
                //if (Random.Range(0f, 1f) < 0.2f || (x == 0 && y == 0))
                //{
                //    c.Enemy.SetActive(!maze[x, y].Enemy);
                //}
                //else
                //{
                //    c.Enemy.SetActive(maze[x, y].Enemy);
                //}
            }
        }
    }
}
