using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to any GameObject to scale along 'y' axis according to Spectrum data.
public class BoomBox : MonoBehaviour
{
    //An AudioSource object so the music can be played  
    private AudioSource aSource;
    //A float array that stores the audio samples  
    public float[] samples = new float[64];

    void Awake()
    {
        //Find the attached audio source         
      	 this.aSource = GameObject.FindGameObjectWithTag("DancingAudio").GetComponent<AudioSource>();
    }

    void Start()
    {
      
    }

    void Update()
    {
        //Obtain the samples from the frequency bands of the attached AudioSource  
        aSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
		
		//Scale the GameObject along y axis to get the desired boombox effect.
        this.transform.localScale =  new Vector3(this.transform.localScale.x, (Mathf.Clamp(samples[16]*50, 0, 50))*0.5f,this.transform.localScale.z);
       
    }
}
