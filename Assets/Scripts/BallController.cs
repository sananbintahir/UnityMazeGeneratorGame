using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* \brief Control the ball's movements through the movement keys
*        (arrow and WASD)
*/
public class BallController : MonoBehaviour
{
    public float speed = 7;
    private Rigidbody rb;

    /**
    * \brief Start is called before the first frame update
    * 
    * \return null
    */
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    /**
    * \brief FixedUpdate is called on a regular timeline
    * 
    * \return null
    */
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        rb.AddForce(movement * speed);
    }
}
