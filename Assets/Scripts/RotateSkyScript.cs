using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
        RenderSettings.skybox.SetFloat("_Rotation", Time.time*3.0f); //To set the speed, just multiply the Time.time with whatever amount you want.
	}
}
