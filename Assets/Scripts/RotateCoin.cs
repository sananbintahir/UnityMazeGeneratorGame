using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* \brief Rotates the coin along the x axis
*/
public class RotateCoin : MonoBehaviour
{

    public float speed = 150f;

    /**
    * \brief Update() is called once every frame
    * 
    * \return null
    */
    void Update()
    {
        transform.Rotate(Vector3.right, speed * Time.deltaTime);
    }
}
