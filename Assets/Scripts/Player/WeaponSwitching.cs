using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {


    public int SelectedWeapon = 0;
    
    
    
	void Start () {

        SelectWeapon();

	}
	
	// Update is called once per frame
	void Update () {


        int previousSelectedWeapon = SelectedWeapon;

        // If we go over the count or under the count of weapons, then have it start from the beginning/end again.
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedWeapon >= transform.childCount - 1)
            {
                SelectedWeapon = 0;
            }
            else
            {
                SelectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedWeapon <= 0)
            {
                SelectedWeapon = transform.childCount - 1;
            }
            else
            {
                SelectedWeapon--;
            }
        }
        if (previousSelectedWeapon != SelectedWeapon)
        {
            SelectWeapon();
        }

    }

    // When we select a weapon, we set it to be active and everything else is deactivated.
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == SelectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
