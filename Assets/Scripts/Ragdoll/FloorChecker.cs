using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour {

    // Store a distance that the ray will project, as well as a fall speed and a layermask for the ragdoll
    // to consider ground.
    public float fallSpeed;
    public float rayDistance;
    public LayerMask ground;
    // as well as a boolean for it is it in fact grounded.
    private bool isGrounded = false;


    /// <summary>
    /// During FixedUpdate, if the rag doll is not grounded, we will make the y position go down by the fall speed until it does.
    /// </summary>
    void FixedUpdate () {

        if (isGrounded == false)
        {
            switch (Grounded())
            {
                case true:
                    isGrounded = true;
                    break;

                case false:
                    Vector3 position = transform.position;
                    position.y -= fallSpeed * Time.deltaTime;
                    transform.position = position;
                    break;
            }
        }
 

	}

    // a simple raycast check for the ragdoll to the ground.
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, rayDistance, ground);
    }
}
