using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatforms : MonoBehaviour {


    // If the player hits a rotating platform, make it a child of the platform.
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "platform")
        {
            this.transform.parent = hit.transform;
        }
        else
        {
            this.transform.parent = null;
        }
    }



}
