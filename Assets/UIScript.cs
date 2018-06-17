using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour {

    public Button widthPlus, widthMinus, lengthPlus, lengthMinus;
    public Slider magnitude, frequency,emission, intensity, threshold, sthreshold, iterations;
    public PerlinNoise noiseScript;
    public Toggle animatePerlin, BloomOnly;
    public ParticleSystem rain;
    public BloomEffect bloomCam;


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
        // Have the emission rate of our rain to be that of the slider for the rain.
        ParticleSystem.EmissionModule emmiter = rain.emission;
        // And have our bloom effect variables set to the value on the sliders.
        emmiter.rateOverTime = emission.value;
        bloomCam.intensity = intensity.value;
        bloomCam.threshold = threshold.value;
        bloomCam.softThreshold = sthreshold.value;
        bloomCam.iterations = (int)iterations.value;


        // if we have the tick box on, then animate the perlin noise
        if (animatePerlin.isOn)
        {
            noiseScript.AnimatePerlin = true;
            frequency.enabled = false;
            magnitude.enabled = false;
        }
        else
        {
            noiseScript.AnimatePerlin = false;
            frequency.enabled = true;
            magnitude.enabled = true;
        }

        // if Bloom is checked, show bloom only.
        if (BloomOnly.isOn)
        {
            bloomCam.ShowBloomOnly = true;
        }
        else
        {
            bloomCam.ShowBloomOnly = false;
        }


	}

    // Check the plusOrMinus value, then check the 'value' variable in order to perform the correct function.

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
