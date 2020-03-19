using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to any GameObject to rotate along 'x' axis according to Spectrum data
public class Boomboxrotate : MonoBehaviour
{
     //An AudioSource object so the music can be played  
    private AudioSource aSource;
    //A float array that stores the audio samples  
    public float[] samples = new float[64];

    void Awake()
    {
        //Get and store a reference to the following attached components:        
      	 this.aSource = GameObject.FindGameObjectWithTag("DancingAudio").GetComponent<AudioSource>();
    }

    void Start()
    {
      
    }

    void Update()
    {
        //Obtain the samples from the frequency bands of the attached AudioSource  
        aSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
		
		//Rotate the GameObject along one of the axis to get the rotating effect.
        this.transform.Rotate((Mathf.Clamp(samples[16]*50, 0, 50))*90, 0f,0f, Space.World);    
	}

}
