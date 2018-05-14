using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFloorCheck : MonoBehaviour {

    
    /// <summary>
    /// Once the mine has collided with the ground, make it it's parents and have it not move. this only apply's to moving platforms.
    /// </summary>
    /// <param name="collision"> the soon to be parent of the mine.</param>
    /// <returns></returns>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "platform")
        {
            this.transform.parent = collision.transform;
        }
    }

}
