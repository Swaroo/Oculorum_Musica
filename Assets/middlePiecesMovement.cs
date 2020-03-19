using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middlePiecesMovement : MonoBehaviour
{
    public int spectrumIndex;
	
    public float initialy;
    
	//AudioSource object to access the attached audio.
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
		//Store the initial 'y' position of the GameObject, so that the Line's position can be updated relative to this initial position.
      	initialy = this.transform.position.y;
    }

    void Update()
    {
        //Obtain the samples from the frequency bands of the attached AudioSource  
        aSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
		
        int i = spectrumIndex;
		
		//Position of the gameoject is increased relative to the initial position of the GameObject.
        this.transform.position = new Vector3(this.transform.position.x, initialy + Mathf.Clamp(samples[i] * (50 + i * i),0,50)*0.3f, this.transform.position.z);
       
    }
}
