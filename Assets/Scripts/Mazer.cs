using UnityEngine;
using System.Collections;

public class Mazer : MonoBehaviour
{
    ArrayList walls = new ArrayList();
    ArrayList cells = new ArrayList();

    void MazeIt()
    {

        // Maze Generator by Charles Crete
        // Using Prim's algorithm
        // More info: http://en.wikipedia.org/wiki/Maze_generation_algorithm#Randomized_Prim.27s_algorithm
        int max = 10000;
        int size = 49; // must be odd?
        int sizer = (size - 1) / 2;
        Debug.Log("size : " + size + " r " + sizer);

        for (int x = -sizer; x <= sizer; x++)
        {
            for (int z = -sizer; z <= sizer; z++)
            {
                cells.Add(new Cell(x, z));
            }
        }



        Cell startingCell = getCellAt(0, 0);
        walls.Add(startingCell);

        while (true)
        {

            Cell wall = (Cell)walls[Random.Range(0, walls.Count)];

            processWall(wall);


            if (walls.Count <= 0)
                break;
            if (--max < 0)
                break;

        }

        foreach (Cell cell in cells)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            if (cell.wall)
            {
                cube.transform.position = new Vector3((float)cell.x, (float)0.5, (float)cell.z);

                //cube.transform.localScale = new Vector3 (1, 3, 1);

            }
            else
            {
                //cube.transform.localScale = new Vector3 (1, 0.1, 1);
            }

        }

    }

    void processWall(Cell cell)
    {
        int x = cell.x;
        int z = cell.z;
        if (cell.from == null)
        {
            if (Random.Range(0, 2) == 0)
            {
                x += Random.Range(0, 2) - 1;
            }
            else
            {
                z += Random.Range(0, 2) - 1;
            }
        }
        else
        {

            x += (cell.x - cell.from.x);
            z += (cell.z - cell.from.z);
        }
        Cell next = getCellAt(x, z);
        if (next == null || !next.wall)
            return;
        cell.wall = false;
        next.wall = false;


        foreach (Cell process in getWallsAroundCell(next))
        {
            process.from = next;
            walls.Add(process);
        }
        walls.Remove(cell);

    }

    Cell getCellAt(int x, int z)
    {
        foreach (Cell cell in cells)
        {
            if (cell.x == x && cell.z == z)
                return cell;
        }
        return null;
    }

    ArrayList getWallsAroundCell(Cell cell)
    {
        ArrayList near = new ArrayList();
        ArrayList check = new ArrayList();

        check.Add(getCellAt(cell.x + 1, cell.z));
        check.Add(getCellAt(cell.x - 1, cell.z));
        check.Add(getCellAt(cell.x, cell.z + 1));
        check.Add(getCellAt(cell.x, cell.z - 1));

        foreach (Cell checking in check)
        {
            if (checking != null && checking.wall)
                near.Add(checking);
        }
        return near;

    }


    public class Cell
    {
        public int x { get; set; }

        public int z { get; set; }

        public bool wall { get; set; }

        public Cell from { get; set; }

        public Cell(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.wall = true;
        }


    }

    // Use this for initialization
    // Making it cleaner
    void Start()
    {
        MazeIt();
    }
}