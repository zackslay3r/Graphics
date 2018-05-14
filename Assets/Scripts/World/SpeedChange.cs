using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChange : MonoBehaviour {

    // The amount of platforms we wish to change.
    public GameObject[] platforms;
    // An enum for the selection of adding or subtracting speed values.
    public enum IncreaseDecrease { ADD, SUBTRACT };

    // the selector for the value.
    public IncreaseDecrease selector;


    /// <summary>
    /// When the player hits one of the buttons, depending on the value assigned ("the enum") it will add or subtract the speed from the platforms.
    /// </summary>
    /// <param name="other"> the other collider. which will in this case be the player.</param>
    /// <returns></returns>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (selector)
            {
                case IncreaseDecrease.ADD:
                    foreach (GameObject platform in platforms)
                    {
                        ConstantSpinning cs = platform.GetComponentInChildren<ConstantSpinning>();
                        if (cs != null)
                        {
                            cs.speed += 10;
                        }
                    }
                    break;
                case IncreaseDecrease.SUBTRACT:
                    foreach (GameObject platform in platforms)
                    {
                        ConstantSpinning cs = platform.GetComponentInChildren<ConstantSpinning>();
                        if (cs != null)
                        {
                            if (cs.speed > 9)
                            {
                                cs.speed -= 10;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
