using UnityEngine;
using UnityEngine.SceneManagement;

/**
* \brief This class defines what happens when the ball and
*        the coin collide
*        
* The animation on the coin is run, and the scene is reloaded
* after 1 second (to wait for the animation to finish)
*/
public class CollideAnimation : MonoBehaviour
{
    public Animator anim;
    public Scene scene;
    
    void OnTriggerEnter(Collider coll)
    {
        anim.SetTrigger("Collide");
        Invoke("LoadScene", (float)1);
    }

    /**
    * \brief Reload the scene
    * 
    * \return null
    */
    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
