
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAudio: MonoBehaviour
{
    //An AudioSource object so the music can be played  
    private AudioSource aSource;
    //A float array that stores the audio samples  
    public float[] samples = new float[64];
    //A renderer that will draw a line at the screen  
    private LineRenderer lRenderer;
    //The transform attached to this game object  
    private Transform goTransform;
    //The position of the current cube. Will also be the position of each point of the line.  
    private Vector3 cubePos;
    //An array that stores the Transforms of all instantiated cubes  
    private Vector3[] cubesTransform;

    void Awake()
    {
        //Get and store a reference to the following attached components:  
        //AudioSource  
        this.aSource = GameObject.FindGameObjectWithTag("DancingAudio").GetComponent<AudioSource>();
        //LineRenderer  
        this.lRenderer = GetComponent<LineRenderer>();
        //Transform  
        this.goTransform = GetComponent<Transform>();
    }

    void Start()
    {
        //The line should have the same number of points as the number of samples  
        lRenderer.SetVertexCount(samples.Length);
        //The cubesTransform array should be initialized with the same length as the samples array  
        cubesTransform = new Vector3[samples.Length];
        //Center the audio visualization line at the X axis, according to the samples array length  
        goTransform.position = new Vector3(goTransform.position.x -samples.Length*0.1f / 2, goTransform.position.y, goTransform.position.z);

        //For each sample  
        for (int i = 0; i < samples.Length; i++)
        {
            //Get the recently instantiated cube Transform component  
            cubesTransform[i] = new Vector3(goTransform.position.x + i*0.1f, goTransform.position.y, goTransform.position.z);
        }
    }

    void Update()
    {
        //Obtain the samples from the frequency bands of the attached AudioSource  
        aSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);

        //For each sample  
        for (int i = 0; i < samples.Length; i++)
        {
            /*Set the cubePos Vector3 to the same value as the position of the corresponding 
             * cube. However, set it's Y element according to the current sample.*/
            cubePos.Set(cubesTransform[i][0], goTransform.position.y + Mathf.Clamp(samples[i] * (50 + i * i), 0, 50), cubesTransform[i][2]);

            /*Set the position of each vertex of the line based on the cube position. 
             * Since this method only takes absolute World space positions, it has 
             * been subtracted by the current game object position.*/
            lRenderer.SetPosition(i, cubePos -goTransform.position);
        }
    }
}
