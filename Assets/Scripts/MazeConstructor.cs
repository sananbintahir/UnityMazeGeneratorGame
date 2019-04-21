using UnityEngine;
using UnityEngine.UI;

/**
 * \brief The MazeConstructor Class
 * 
 * This class uses a 2d array to represent the maze where
 * 1s represent walls and 0s represend open spaces.
 * It applies the algorithm to the array according to an 
 * option selected in the dropdown on the UI.
 */
public class MazeConstructor : MonoBehaviour
{
    //objects for different algorithms
    private Prims prims;
    private Kruskalls krusk;
    private RecursiveBacktracker backtracker;

    //stores the starting position of the ball
    public int startRow {get; private set;}
    public int startCol {get; private set;}
    //stores the position of the goal (coin)
    public int goalRow{get; private set;}
    public int goalCol{get; private set;}

    //plane to be generated
    [SerializeField] public GameObject Plane;
    //camera to position and rotate
    [SerializeField] public GameObject camera;
    //ball to be positioned
    [SerializeField] private GameObject sphere;
    //coin to be positioned (goal)
    [SerializeField] public GameObject coin;
    //dropdown to choose algorithm implementation
    [SerializeField] private Dropdown dropdown;

    //to normalise between different algorithms
    //a 2d array is used to represent the maze
    public int[,] data
    {
        get; private set;
    }

    void Awake()
    {
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
    }

    /**
     * \brief Function to generate the maze
     * 
     * Uses the appropriate algorithm according to
     * the selected option in the UI and applies it to the 2d
     * array. Disposes the old maze.
     * 
     * \return null
     */
    public void GenerateNewMaze(int sizeR, int sizeC)
    {
        //Better mazes are produced if number of rows and columns are odd
        if (sizeR % 2 == 0)
        {
            sizeR += 1;
        }
        if (sizeC % 2 == 0)
        {
            sizeC += 1;
        }

        //Destroy the old maze
        DisposeOldMaze();

        if(dropdown.value == 0)
        {
            backtracker = new RecursiveBacktracker();
            data = backtracker.GenerateMaze(sizeR, sizeC);
        }
        else if(dropdown.value == 1)
        {
            prims = new Prims();
            data = prims.Mazer(sizeR, sizeC);
        }
        else if(dropdown.value == 2)
        {
            krusk = new Kruskalls();
            data = krusk.Mazer(sizeR, sizeC);
        }

        DisplayMaze();
    }

    /**
     * \brief function to delete the old maze
     * 
     * Every gameObject is initialised with a "Generated" tag
     * and we use that fact to destroy all of the objects
     * 
     * \return null
     */
    public void DisposeOldMaze()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject go in objects)
        {
            Destroy(go);
        }
    }

    /**
     * \brief All GameObjects are created and the maze is generated
     * 
     * The function makes appropriate calculations to position all of
     * the walls, which are 3d cubes, the plane, which is also a cube,
     * and the camera.
     * 
     * \return null
     */
    private void DisplayMaze()
    {

        int rMax = data.GetUpperBound(0);
        int cMax = data.GetUpperBound(1);

        //making maze borders
        CreateBorders(rMax, cMax);
        //setting camera and plane
        SetPlaneCamera();

        //Creating the maze with cubes
        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (data[i, j] == 1)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.tag = "Generated";
                    cube.transform.position = new Vector3((float)i, (float)0,(float)j);
                    cube.transform.localScale = new Vector3 (1, 3, 1);

                }
            }
        }

        //determining start position of ball
        FindStartPosition();
        //determining position of coin
        FindGoalPosition();

        //placing ball
        sphere.transform.position = new Vector3((float)startRow, 0, (float)startCol);
        sphere.tag = "Generated";
        Instantiate(sphere);

        //placing coin
        coin.transform.position = new Vector3((float)goalRow, (float)1.2, (float)goalCol);
        coin.tag = "Generated";
        Instantiate(coin);
    }

    /**
     * \brief Creats borders of the maze
     * 
     * Takes max number of rows and columns as parameters
     * 
     * \return null
     */
    private void CreateBorders(int rMax, int cMax)
    {
        int r = rMax / 2;
        int c = cMax / 2;

        GameObject border1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        border1.tag = "Generated";
        border1.transform.position = new Vector3((float)r,
            (float)0, (float)cMax + 1);
        border1.transform.localScale = new Vector3(rMax + 1, 3, 1);

        GameObject border2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        border2.tag = "Generated";
        border2.transform.position = new Vector3((float)rMax + 1,
            (float)0, (float)c);
        border2.transform.localScale = new Vector3(1, 3, cMax + 3);

        GameObject border3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        border3.tag = "Generated";
        border3.transform.position = new Vector3((float)r,
            (float)0, (float)-1);
        border3.transform.localScale = new Vector3(rMax + 1, 3, 1);

        GameObject border4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        border4.tag = "Generated";
        border4.transform.position = new Vector3((float)-1,
            (float)0, (float)c);
        border4.transform.localScale = new Vector3(1, 3, cMax + 3);
    }

    /**
     * \brief Positions the Camera and the plane according
     *        to the number of rows and columns of the maze
     *        
     * \return null
     */
    private void SetPlaneCamera()
    {
        int rMax = data.GetUpperBound(0);
        int cMax = data.GetUpperBound(1);
        int r = rMax / 2;
        int c = cMax / 2;
        int max = Mathf.Max(r, c) + 2;
        float scrHeight = Screen.height;
        float scrWidth = Screen.width;
        float aspect = scrWidth / scrHeight;

        Camera cam = camera.GetComponent<Camera>();
        cam.aspect = aspect;

        cam.transform.position = new Vector3((float)r, (float)21, (float)c - 7);
        cam.transform.rotation = Quaternion.Euler(70, 0, 0);
        cam.orthographicSize = max+2;

        Plane.tag = "Generated";
        Plane.transform.position = new Vector3((float)r, (float)0, (float)c);
        Plane.transform.localScale = new Vector3(max/2, 1, max / 2);
        Instantiate(Plane);
    }

    /**
     * \brief Loops from the start of the maze to find empty position
     *        and sets the class variables accordingly
     * 
     * \return null
     */
    private void FindStartPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    startRow = i;
                    startCol = j;
                    return;
                }
            }
        }
    }

    /**
     * \brief Loops from the end of the maze to find an empty position
     *        and sets the class variable accordingly
     * 
     * \return null
     */
    private void FindGoalPosition()
    {
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        // loop top to bottom, right to left
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = cMax; j >= 0; j--)
            {
                if (maze[i, j] == 0)
                {
                    goalRow = i;
                    goalCol = j;
                    return;
                }
            }
        }
    }
}