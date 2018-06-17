using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour {

    public Button widthPlus, widthMinus, lengthPlus, lengthMinus;
    public Slider magnitude, frequency;
    public PerlinNoise noiseScript;
    public Toggle animatePerlin;

    //bool perlinAnimate;


    // Use this for initialization
	void Start () {
        Button btnWidthPlus = widthPlus.GetComponent<Button>();
        Button btnWidthMinus = widthMinus.GetComponent<Button>();
        Button btnlengthPlus = lengthPlus.GetComponent<Button>();
        Button btnlengthMinus = lengthMinus.GetComponent<Button>();


        //We are going to add listeners to these buttons.

       btnWidthPlus.onClick.AddListener(delegate { ChangeValues("plus", "width"); });
        btnWidthMinus.onClick.AddListener(delegate { ChangeValues("minus", "width"); });
        btnlengthPlus.onClick.AddListener(delegate { ChangeValues("plus", "length"); });
        btnlengthMinus.onClick.AddListener(delegate { ChangeValues("minus", "length"); });
    }
	
	// Update is called once per frame
	void Update () {

        // We will grab the values of both the sliders for the magnitude and frequency.
        noiseScript.magnitude = magnitude.value;
        noiseScript.frequency = frequency.value;

        if (animatePerlin.isOn)
        {
            noiseScript.AnimatePerlin = true;
        }
        else
        {
            noiseScript.AnimatePerlin = false;
        }



	}

    void ChangeValues(string plusOrMinus, string value)
    {
        switch (plusOrMinus)
        {
            case "plus":
                if (value == "width")
                {
                    noiseScript.widthOfCubes++;
                    noiseScript.Refresh();
                }
                else
                {
                    noiseScript.lengthOfCubes++;
                    noiseScript.Refresh();
                }

            break;

            case "minus":
                if (value == "width")
                {
                    noiseScript.widthOfCubes--;
                    noiseScript.Refresh();
                }
                else
                {
                    noiseScript.lengthOfCubes--;
                    noiseScript.Refresh();
                }
                break;
        }
    }
}
