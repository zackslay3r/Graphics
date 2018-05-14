using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public float range = 100;
    public float impactForce;
    public Transform EndOfGun;
    public Camera fpsCam;

    private void Update()
    {
        // If the user presses the left mouse button with the ak out, shoot.
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // A variable that will store the imformation gathered from the raycast.
        RaycastHit hit;
        Debug.DrawRay(EndOfGun.transform.position, fpsCam.transform.forward * range, Color.blue, 3.0f);
        // If we hit something with our shot raycast.
        //if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) ;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.DrawLine(fpsCam.transform.position, hit.point, Color.black, 3.0f);

            // Try to convert all the rigidbodies of a dummy into an array.
            Rigidbody[] rigidbodies = transform.root.GetComponentsInChildren<Rigidbody>();
           
           
            // if we hit a rigidbody...
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                // and its a part of a ragdoll...
                if (hit.transform.root.GetComponent<Ragdoll>() != null)
                {
                    // and ragdoll is not turned on...
                    if (!hit.transform.root.GetComponent<Ragdoll>().RagdollOn)
                    {

                        // make the ragdoll turn on.
                        hit.transform.root.GetComponent<Ragdoll>().RagdollOn = true;
                        // have all the rigidbodies be not kinematic.
                        foreach (Rigidbody r in rigidbodies)
                        {
                            r.isKinematic = false;
                        }

                    }

                }
                // if its just an object, then just have a force applied at a position.
                hit.rigidbody.AddForceAtPosition((hit.point - EndOfGun.transform.position).normalized * impactForce, hit.point);
            }
            // if we hit a softbody, then apply its own forces.
            if (hit.transform.tag == "Softbody" )
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}