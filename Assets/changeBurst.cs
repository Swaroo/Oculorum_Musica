﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBurst : MonoBehaviour
{

	public ParticleSystem particleSystem;
	public int emitno = 10;

    // Start is called before the first frame update
    void Start()
    {
    	this.particleSystem = GetComponent<ParticleSystem>();

    	AudioProcessor processor = FindObjectOfType<AudioProcessor> ();
		processor.onBeat.AddListener (onOnbeatDetected);
		//processor.onSpectrum.AddListener (onSpectrum);
    }

    //this event will be called every time a beat is detected.
	//Change the threshold parameter in the inspector
	//to adjust the sensitivity
	void onOnbeatDetected ()
	{
		//Debug.Log ("Beat!!!");
		particleSystem.Emit(emitno);
	}

	//This event will be called every frame while music is playing
	// void onSpectrum (float[] spectrum)
	// {
	// 	//The spectrum is logarithmically averaged
	// 	//to 12 bands

	// 	for (int i = 0; i < spectrum.Length; ++i) {
	// 		Vector3 start = new Vector3 (i, 0, 0);
	// 		Vector3 end = new Vector3 (i, spectrum [i], 0);
	// 		Debug.DrawLine (start, end);
	// 	}
	// }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("space")){
        	
        // 	print("key down");
        // 	particleSystem.Emit(10);
        // }
    }
}
