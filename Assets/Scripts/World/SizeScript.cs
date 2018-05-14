using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeScript : MonoBehaviour {

    public GameObject button;
    private SoftBodyCreator SpawnerScript;
    public enum IncreaseDecrease {ADD,SUBTRACT};
    public enum WidthHeightBreath {Length,Height,Width};

    public WidthHeightBreath SizeChooser;
    public IncreaseDecrease sizeScalar;

    private void Start()
    {
        SpawnerScript = button.GetComponent<SoftBodySpawner>().spawner.GetComponent<SoftBodyCreator>();
    }

   
    // When the player collides with a button, depending on what they have selected,
    // Add/Subtract to the Length/Height/Widith of the softbody about to be spawned.
    private void OnTriggerEnter(Collider other)
    {
        switch (SizeChooser)
        {
            case WidthHeightBreath.Length:
                if (sizeScalar == IncreaseDecrease.ADD)
                {
                    SpawnerScript.Length += 1;
                }
                else
                {
                    SpawnerScript.Length -= 1;
                }
                break;
            case WidthHeightBreath.Height:
                if (sizeScalar == IncreaseDecrease.ADD)
                {
                    SpawnerScript.Height += 1;
                }
                else
                {
                    SpawnerScript.Height -= 1;
                }
                break;
            case WidthHeightBreath.Width:
                
                if (sizeScalar == IncreaseDecrease.ADD)
                {
                    SpawnerScript.Width += 1;
                }
                else
                {
                    SpawnerScript.Width -= 1;
                }
                break;
            default:
                break;
        }
    }
}
