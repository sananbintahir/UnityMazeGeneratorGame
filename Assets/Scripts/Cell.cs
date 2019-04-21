/**
* \brief Cell class which is used by Prim's and
* Kruskall's algorithms. A boolean value determines
* if it is a wall or not and two integer values determine
* its column and row position 
*/
public class Cell
{
    public int r { get; set; }

    public int c { get; set; }

    public bool wall { get; set; }

    public Cell parent { get; set; }

    public Cell(int r, int c, bool wall)
    {
        this.r = r;
        this.c = c;
        this.wall = wall;
    }
}