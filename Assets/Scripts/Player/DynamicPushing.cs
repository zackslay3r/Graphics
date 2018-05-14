using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPushing : MonoBehaviour {

    public float pushPower = 2.0F;


    /// <summary>
    /// This will allow our player to push objects.
    /// </summary>
    /// <param name="hit"> the other object we are colliding with.</param>
    /// <returns></returns>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }
        if (hit.moveDirection.y < -0.3F)
        {
            return;
        }
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}
