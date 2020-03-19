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
		
		// Link audioprocessor to this object
    	AudioProcessor processor = FindObjectOfType<AudioProcessor> ();
		processor.onBeat.AddListener (onOnbeatDetected);
    }

    //this event will be called every time a beat is detected.
	void onOnbeatDetected ()
	{
		//Debug.Log ("Beat!!!");
		particleSystem.Emit(emitno);
	}

    // Update is called once per frame
    void Update()
    {

    }
}
