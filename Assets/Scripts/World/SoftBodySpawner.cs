using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBodySpawner : MonoBehaviour {

    public GameObject spawner;

    
    // When the play collides with this object, reference to function "MakeSoftBody" and make a softbody with the values the user has.
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            spawner.GetComponent<SoftBodyCreator>().MakeSoftBody(spawner.GetComponent<SoftBodyCreator>().Length, spawner.GetComponent<SoftBodyCreator>().Height, spawner.GetComponent<SoftBodyCreator>().Width, spawner.GetComponent<SoftBodyCreator>().springCoefficient, spawner.GetComponent<SoftBodyCreator>().dampering);
        }
    }


}
