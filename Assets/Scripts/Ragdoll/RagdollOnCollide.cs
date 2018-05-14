using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnCollide : MonoBehaviour {


    // When a ragdoll collides with this object, have it ragdoll.
    private void OnTriggerEnter(Collider other)
    {
        Ragdoll ragdoll = other.gameObject.GetComponentInParent<Ragdoll>();
        if (ragdoll != null)
        {
            ragdoll.RagdollOn = true;
        }

    }
}
