using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSpeed : MonoBehaviour {

    // The platform we wish to show the speed of.
    public GameObject platform;

	
	
	/// <summary>
	/// During Update, we want to show the speed value of the platforms spinning.
    /// since both of the platforms that speed alter have the same speed, we only need to display the info of one.
	/// </summary>
	/// <returns>void</returns>
	void Update () {
        
        // assign the component
        ConstantSpinning cs = platform.GetComponentInChildren<ConstantSpinning>();
       
        // display the speed vaule.
        gameObject.GetComponent<TextMesh>().text = "Speed: " + cs.speed.ToString();


    }
}
