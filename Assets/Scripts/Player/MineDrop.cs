using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDrop : MonoBehaviour {

    
    // store a reference to the real mine.
    public GameObject realMine;


	/// <summary>
    /// During Update, if the user left clicked while the mine was active,
    /// we will instantiate a real mine which will then drop.
    /// </summary><returns>void</returns>
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
             GameObject g = Instantiate(realMine,new Vector3(transform.position.x,transform.position.y - 0.1f,transform.position.z),transform.rotation);
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
        }
	}
}
