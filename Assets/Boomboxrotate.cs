using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        this.transform.Rotate((Mathf.Clamp(samples[16]*50, 0, 50))*90, 0f,0f, Space.World);

      //  this.transform.localScale += new Vector3(0,0.5f,0);
       // cubePos.Set(cubesTransform[i].position.x, goTransform.position.y + Mathf.Clamp(samples[i] * (50 + i * i), 0, 50), cubesTransform[i].position.z);
        //this.transform.localRotation =  new Vector3(this.transform.localRotation.x, (Mathf.Clamp(samples[16]*50, 0, 50))*0.5f,this.transform.localRotation.z);
    
	}

}
