using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour {

    public float timer = 5.0f;
 


	// Once the Timer has hit 0, run TimerEnded, which will destroy the object.
	void Update () {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            TimerEnded();
        }



    }
    void TimerEnded()
    {
        Destroy(gameObject);
    
    }
}
