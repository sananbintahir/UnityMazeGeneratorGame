using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    private MazeConstructor generator;
    [SerializeField] private Slider rows;
    [SerializeField] private Slider columns;
    private float nRows;
    private float nColumns;

    /**
     * \brief Start is called before the first frame update
     *        
     * MazeConstructor object is created and the default
     * row and column values for the first maze are passed
     * to newMaze(float, float) function
     * 
     * \return null
     */
    void Start()
    {
        generator = GetComponent<MazeConstructor>();
        newMaze(27, 23);
    }

    /**
     * \brief function used by the regen button to regenerate maze
     * 
     * \return null
     */
    public void RegenMaze()
    {
        newMaze(rows.value, columns.value);
    }

    /**
     * \brief Generates a maze using the GenerateNewMaze
     *        method of MazeConstructor
     * 
     * \return null
     */
    public void newMaze(float rows, float columns)
    {
        generator.GenerateNewMaze((int)rows, (int)columns);
    }
}