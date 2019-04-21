using System.Collections;
using UnityEngine;


/**
* \brief Prim's implementation of the maze
*        generation algorithm.
*       
* Works with the cell class, and converts it into a 2d array
* which it returns to the maze constructor.
*/
public class Prims
{
    //ArrayList to store walls and cells
    ArrayList walls = new ArrayList();
    ArrayList cells = new ArrayList();

    /**
    * \brief Main function that generates a maze with cells
    * 
    * Works with the cell class to perform operations and finally converts
    * to a 2d array where 1s are walls and 0s are empty cells
    * 
    * \param number of rows and columns of maze
    * 
    * \return 2d array representing the maze
    */
    public int[,] Mazer(int R, int C)
    {
        int max = 50000;
        int sizerR = (R - 1) / 2;
        int sizerC = (C - 1) / 2;
        int[,] maze = new int[R, C];

        //initialising with all walls
        for (int c = 0; c <= sizerC*2; c++)
        {
            for (int r = 0; r <= sizerR*2; r++)
            {
                cells.Add(new Cell(r, c, true));
            }
        }

        //starting at first cell
        Cell startingCell = getCellAt(0, 0);
        walls.Add(startingCell);

        while (true)
        {
            //take a random wall
            Cell wall = (Cell)walls[Random.Range(0, walls.Count)];

            //process it
            processWall(wall);

            //break conditions
            if (walls.Count <= 0)
                break;
            if (--max < 0)
                break;

        }

        //converting to 2d array
        foreach (Cell cell in cells)
        {

            if (cell.wall)
            {
                maze[cell.r, cell.c] = 1;
            }
            else
            {
                maze[cell.r, cell.c] = 0;
            }

        }

        return maze;

    }

    /**
    * \brief Function applies to cell if it is a wall 
    * 
    * Starts with a random cell and keeps track of the cell
    * seperated by the current wall by storing it as a parent.
    * The wall is destroyed if only one of the two cells it
    * seperates is visited.
    * 
    * \param Cell to process
    * 
    * \return null
    */
    void processWall(Cell cell)
    {
        int c = cell.c;
        int r = cell.r;
        if (cell.parent == null)
        {
            if (Random.Range(0, 2) == 0)
            {
                c += Random.Range(0, 2) - 1;
            }
            else
            {
                r += Random.Range(0, 2) - 1;
            }
        }
        else
        {

            c += (cell.c - cell.parent.c);
            r += (cell.r - cell.parent.r);
        }
        Cell next = getCellAt(c, r);
        //if out of bounds or not a wall do nothing
        if (next == null || !next.wall)
            return;
        //remove current and next wall
        cell.wall = false;
        next.wall = false;

        //keep track of visited cell by adding parent
        foreach (Cell process in getWallsAroundCell(next))
        {
            process.parent = next;
            walls.Add(process);
        }
        //remove from list
        walls.Remove(cell);

    }

    /**
    * \brief gets cell at row and column
    * 
    * \param row and column position
    * 
    * \return cell at row and column in parameter
    */
    Cell getCellAt(int c, int r)
    {
        foreach (Cell cell in cells)
        {
            if (cell.c == c && cell.r == r)
                return cell;
        }
        return null;
    }

    /**
    * \brief get the walls around the cell in a list
    * 
    * \param The cell to look around
    * 
    * \return Arraylist containing the walls
    */
    ArrayList getWallsAroundCell(Cell cell)
    {
        ArrayList near = new ArrayList();
        ArrayList check = new ArrayList();

        check.Add(getCellAt(cell.c + 1, cell.r));
        check.Add(getCellAt(cell.c - 1, cell.r));
        check.Add(getCellAt(cell.c, cell.r + 1));
        check.Add(getCellAt(cell.c, cell.r - 1));

        foreach (Cell checking in check)
        {
            if (checking != null && checking.wall)
                near.Add(checking);
        }
        return near;

    }
}