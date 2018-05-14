using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour {

    public GameObject button;
    private SoftBodyCreator SpawnerScript;

    private void Start()
    {
        SpawnerScript = button.GetComponent<SoftBodySpawner>().spawner.GetComponent<SoftBodyCreator>();
    }
    
    // Display the Width/Height/Length of the softbody about to be created.
    void Update () {

        gameObject.GetComponent<TextMesh>().text = "Width: " + SpawnerScript.Width.ToString() + " " + "Height: " + SpawnerScript.Height.ToString() + " "+ "Breadth:" + SpawnerScript.Length.ToString() ;

    }
}
