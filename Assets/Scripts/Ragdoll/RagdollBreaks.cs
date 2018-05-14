using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBreaks : MonoBehaviour {

    public int BreakStrength;
    private int BreakStrengthSquared;
    public int timer;
    private List<GameObject> limbs;

	// Use this for initialization
	void Start () {
       
        limbs = new List<GameObject>();
  
	}
	
	
	/// <summary>
	/// During Update, we want to check if certain joints in the ragdoll have exceed the breakforce allowed and if so, we want to unparent it and detact it.
	/// </summary>
	/// <returns></returns>
	void Update () {

        
        CharacterJoint[] joints = GetComponentsInChildren<CharacterJoint>();

        // for each of the joints, if the certain joints have had their breakforce exceeded, break them.
        foreach (CharacterJoint j in joints)
        {
            BreakStrengthSquared = BreakStrength * BreakStrength;

            if (j.name.Contains("Arm") || j.name.Contains("UpLeg") || j.name.Contains("Head"))
            {
                if (j.currentForce.sqrMagnitude > BreakStrengthSquared)
                {
                    
                    
                    j.gameObject.transform.SetParent(null);
                    limbs.Add(j.gameObject);
                    Destroy(j);
                    foreach (GameObject limb in limbs)
                    {   
                        Destroy(gameObject.transform.root.gameObject, timer);

                        Destroy(limb,timer);
                    }
                }
            }
        }
	}
  
}
