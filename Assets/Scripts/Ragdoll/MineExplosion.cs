using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour {
    public float radius;
    public float force;


    /// <summary>
    /// When a ragdoll collides with a mine, we will have it have its ragdoll turn on, then an explosive force with be appplied with a force and a radius.
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private void OnCollisionEnter(Collision collision)
    {
        // where the explosion starts.
        Vector3 explosionPos = transform.position;

        // an array storing all the rigidbodies in the dummy.
        Rigidbody[] rigidbodies = collision.transform.root.GetComponentsInChildren<Rigidbody>();

        // if it is a ragdoll...
        if (collision.transform.root.GetComponent<Ragdoll>() != null)
        {
           
           
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                collision.transform.root.GetComponent<Ragdoll>().RagdollOn = true;

                foreach (Rigidbody r in rigidbodies)
                {
                    r.isKinematic = false;
                }
                rb.AddExplosionForce(force, gameObject.transform.position, radius, 10.0f);

                // then destory the mine.
                Destroy(gameObject);


            


        }
    }
}
