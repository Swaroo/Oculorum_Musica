using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChange : MonoBehaviour
{
	public AudioSource aSource;
	public static bool foundSecondRoom;
	public static bool foundFirstRoom;
    // Start is called before the first frame update
    void Start()
    {
        	foundSecondRoom = false;
        	foundFirstRoom = true;
        	//print(this.transform.position.z + "Initial Z position");
        
        	//print("Position is "+GameObject.FindGameObjectWithTag("ControllerTag").transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    	// print("Controller pos is "+GameObject.FindGameObjectWithTag("ControllerTag").transform.position.z);
    	//print(this.transform.position.x + "X position");
    	//print(this.transform.position.z + "Z position");
    	if(!foundSecondRoom &&  this.transform.position.x>(28.0f)){ //this.transform.position.z<(-3.75f) &&
    		foundSecondRoom = true;
    		foundFirstRoom = false;
        	print("reached second");
        	aSource = GameObject.FindGameObjectWithTag("DancingAudio").GetComponent<AudioSource>();
        	aSource.clip = GameObject.FindGameObjectWithTag("calmTag").GetComponent<AudioSource>().clip;
        	//music; //Resources.Load("Come_With_Some_Funk.mp3") as AudioClip;
        	aSource.Play();
        }
        else if(!foundFirstRoom && this.transform.position.x<(28.0f)){ //this.transform.position.z>(-3.75f) &&
    		foundFirstRoom = true;
    		foundSecondRoom = false;
        	print("reached first");
        	aSource = GameObject.FindGameObjectWithTag("DancingAudio").GetComponent<AudioSource>();
        	aSource.clip = GameObject.FindGameObjectWithTag("musicTag").GetComponent<AudioSource>().clip;
        	//music; //Resources.Load("Come_With_Some_Funk.mp3") as AudioClip;
        	aSource.Play();
        }
    }
}
