using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* \brief This is a DisjointSet datatype used by
*        kruskal's algorithm. The code has been written to
*        handle the Cell class.
*/
public class DisjointSet
{
    public int Count { get; private set; }
    private ArrayList cells = new ArrayList();

    /**
    * \brief constructor that puts each cell in a different
    *        set by making it the parent of itself
    * 
    */
    public DisjointSet(ArrayList c)
    {
        cells = c;
        this.Count = cells.Count;

        //All elements are in their own set initially
        foreach (Cell cell in cells)
        {
            cell.parent = cell;
        }
    }

    /**
    * \brief Recursive function to find the set a cell belongs to,
    *        represented by the root node.
    * 
    * \return Cell which is the root node
    */
    public Cell Find(Cell i)
    {
        foreach (Cell cell in cells)
        {
            if (i.r == cell.r && i.c == cell.c)
            {
                if (cell.parent == cell)
                {
                    // Then i is the representative of this set
                    return cell;
                }
                else
                { // Else if i is not the parent of itself
                  // Then i is not the representative of his set,
                  // so we recursively call Find on its parent
                    return Find(cell.parent);
                }
            }
        }
        return i;
    }

    /**
    * \brief Takes 2 cells and puts them into the
    *        same set
    * 
    * \return null
    */
    public void Union(Cell i, Cell j)
    {

        // Find the representatives (or the root nodes) for the set that includes i
        Cell irep = this.Find(i),
            // And do the same for the set that includes j
            jrep = this.Find(j);

        // Make the parent of i's representative be j's representative
        // (effectively moving all of i's set into j's set)
        irep.parent = jrep;
    }
}
