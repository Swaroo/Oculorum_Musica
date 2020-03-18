using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middlePiecesMovement : MonoBehaviour
{
    public int spectrumIndex;
    private AudioSource aSource;
    //A float array that stores the audio samples  
    public float[] samples = new float[64];
    public float initialy;
    void Awake()
    {
        //Get and store a reference to the following attached components:
        
      	 this.aSource = GameObject.FindGameObjectWithTag("DancingAudio").GetComponent<AudioSource>();
    }

    void Start()
    {
      	initialy = this.transform.position.y;
    }

    void Update()
    {
        //Obtain the samples from the frequency bands of the attached AudioSource  
        aSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
        int i = spectrumIndex;
      //  this.transform.localScale += new Vector3(0,0.5f,0);
       // cubePos.Set(cubesTransform[i].position.x, goTransform.position.y + Mathf.Clamp(samples[i] * (50 + i * i), 0, 50), cubesTransform[i].position.z);
        this.transform.position = new Vector3(this.transform.position.x, initialy + Mathf.Clamp(samples[i] * (50 + i * i),0,50)*0.3f, this.transform.position.z);
        //this.transform.localScale =  new Vector3(this.transform.localScale.x, (Mathf.Clamp(samples[i]*50, 0, 50)),this.transform.localScale.z);
       
    }
}
