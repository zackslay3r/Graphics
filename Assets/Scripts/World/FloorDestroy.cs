using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDestroy : MonoBehaviour {


    // If a gameobject hits the floor, destroy it.
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
