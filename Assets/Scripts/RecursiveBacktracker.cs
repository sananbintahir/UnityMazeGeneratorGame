using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* \brief Recursive Backtracker implementation of the maze
*        generation algorithm.
*       
* Works directly with a 2d array where it recursively searches for
* walls to remove in random directions.
*/
public class RecursiveBacktracker
{
    public int[,] maze { get; private set; }
    private int mazeHeight, mazeWidth;

    /**
    * \brief Generate a maze through recursion on a 2d array,
    *        starting from a random cell
    * 
    * \param height and width of the maze
    * 
    * \return 2d array representing the maze
    */
    public int[,] GenerateMaze(int height, int width)
    {

        maze = new int[height, width];

        //Initialize the 2d array to make the entire maze into walls
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                maze[i, j] = 1;

        //Generate random row
        int r = Random.Range(0, height);
        while (r % 2 == 0)
        {
            r = Random.Range(0, height);
        }
        //Generate random column
        int c = Random.Range(0, width);
        while (c % 2 == 0)
        {
            c = Random.Range(0, width);
        }

        //Random starting cell
        maze[r, c] = 0;

        mazeHeight = height;
        mazeWidth = width;

        //Create the maze with recursive method
        recursion(r, c);

        return maze;
    }

    /**
    * \brief recursive algorithm to generate maze
    * 
    * \param starting row and column position
    * 
    * \return null
    */
    public void recursion(int r, int c)
    {
        //4 random directions
        int[] directions = new int[] { 1, 2, 3, 4 };

        //Shuffle the array so a random direction is chosen
        Shuffle(directions);

        //Examine each direction
        for (int i = 0; i < directions.Length; i++)
        {

            switch (directions[i])
            {
                //Look for 2 cells upwards
                case 1:
                    if (r - 2 <= 0)
                        continue;
                    if (maze[r - 2, c] != 0)
                    {
                        //remove walls
                        maze[r - 2, c] = 0;
                        maze[r - 1, c] = 0;
                        //call recursion
                        recursion(r - 2, c);
                    }
                    break;
                //Look for 2 cells to the right
                case 2:
                    if (c + 2 >= mazeWidth - 1)
                        continue;
                    if (maze[r, c + 2] != 0)
                    {
                        //remove walls
                        maze[r, c + 2] = 0;
                        maze[r, c + 1] = 0;
                        //call recursion
                        recursion(r, c + 2);
                    }
                    break;
                //Look for 2 cells to the right
                case 3:
                    if (r + 2 >= mazeHeight - 1)
                        continue;
                    if (maze[r + 2, c] != 0)
                    {
                        //remove walls
                        maze[r + 2, c] = 0;
                        maze[r + 1, c] = 0;
                        //call recursion
                        recursion(r + 2, c);
                    }
                    break;
                    //Look for 2 walls to the left
                case 4:             
                    if (c - 2 <= 0)
                        continue;
                    if (maze[r, c - 2] != 0)
                    {
                        //remove walls
                        maze[r, c - 2] = 0;
                        maze[r, c - 1] = 0;
                        //call recursion
                        recursion(r, c - 2);
                    }
                    break;
            }
        }

    }

    /**
    * \brief A generic algorithm to shuffle an array
    * 
    * \param Array to shuffle
    * 
    * \return Shuffled array
    */
    public void Shuffle<T>(T[] array)
    {
        Random _random = new Random();
        for (int i = array.Length; i > 1; i--)
        {
            // Pick random element to swap.
            int j = Random.Range(0, i); // 0 <= j <= i-1
            // Swap.
            T tmp = array[j];
            array[j] = array[i - 1];
            array[i - 1] = tmp;
        }
    }
}
