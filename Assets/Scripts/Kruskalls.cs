using System.Collections;
using UnityEngine;

/**
* \brief Kruskall's implementation of the
*        maze generation algorithm.
* 
* Works with the cell class and disjoint sets, 
* and converts data into a 2d array
* which it returns to the maze constructor.
*/
public class Kruskalls
{
    //contains empty cells
    ArrayList cells = new ArrayList();
    //contains all of the cells and walls
    ArrayList total = new ArrayList();
    DisjointSet set;

    /**
    * \brief main function that generates the maze
    * 
    * \param number of rows and columns of maze
    * 
    * \return 2d array representing the maze
    */
    public int[,] Mazer(int R, int C)
    {
        int sizerX = (R - 1) / 2;
        int sizerY = (C - 1) / 2;
        int[,] maze = new int[R, C];

        //initialising maze as a grid
        for (int c = 0; c <= sizerY * 2; c++)
        {
            for (int r = 0; r <= sizerX * 2; r++)
            {
                if (c % 2 == 0)
                {
                    if (r % 2 == 0)
                    {
                        cells.Add(new Cell(r, c, false));
                        total.Add(new Cell(r, c, false));
                    }
                    else
                    {
                        total.Add(new Cell(r, c, true));
                    }
                }
                else
                {
                    total.Add(new Cell(r, c, true));
                }
            }
        }

        //making cells as disjoint set
        set = new DisjointSet(cells);

        //shuffling the ArrayList so random wall is chosen
        total = ShuffleList(total);

        foreach (Cell current in total)
        {
            //if cells divided by the wall belong to distinct sets,
            //remove the wall and join the sets
            if (current.c % 2 == 0 && current.wall == true)
            {
                checkLeftRight(current);
            }
            else
            {
                if (current.r % 2 == 0 && current.wall == true)
                {
                    checkTopDown(current);
                }  
            }
            //convert to 2d array
            if (current.wall)
            {
                maze[current.r, current.c] = 1;
            }
            else
            {
                maze[current.r, current.c] = 0;
            }
        }

        return maze;

    }

    /**
    * \brief Shuffles a list of cells
    * 
    * \param arraylist to shuffle
    * 
    * \return shuffled arraylist
    */
    private ArrayList ShuffleList(ArrayList inputList)
    {
        ArrayList randomList = new ArrayList();
        
        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            //Choose a random object in the list
            randomIndex = Random.Range(0, inputList.Count); 
            //add it to the new, random list
            randomList.Add(inputList[randomIndex]);
            //remove to avoid duplicates
            inputList.RemoveAt(randomIndex); 
        }
        //return the new random list
        return randomList; 
    }

    /**
    * \brief check the cells to the left and right
    *        the wall
    * 
    * \param wall to check around
    * 
    * \return null
    */
    void checkLeftRight(Cell current)
    {
        Cell left = getCellAt(current.c, current.r-1);
        Cell right = getCellAt(current.c, current.r+1);
        if (right == null || left == null)
        {
            return;
        }
        if (set.Find(left) != set.Find(right)){
            //remove wall
            current.wall = false;
            //join sets
            set.Union(left, right);
        }
    }

    /**
    * \brief check the cells up and below the wall
    * 
    * \param wall to check around
    * 
    * \return null
    */
    void checkTopDown(Cell current)
    {
        Cell top = getCellAt(current.c-1, current.r);
        Cell down = getCellAt(current.c+1, current.r);
        if (top == null || down == null)
        {
            return;
        }
        if (set.Find(top) != set.Find(down))
        {
            //remove wall
            current.wall = false;
            //join sets
            set.Union(top, down);
        }
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
        foreach (Cell cell in total)
        {
            if (cell.c == c && cell.r == r)
                return cell;
        }
        return null;
    }
}
