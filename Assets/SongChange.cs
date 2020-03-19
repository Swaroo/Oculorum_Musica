using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChange : MonoBehaviour
{
	//AudioSource object to access the attached audio.
	public AudioSource aSource;
	
	//Two boolean variabes to keep track of which room the user is currently in.
	public static bool foundSecondRoom;
	public static bool foundFirstRoom;
    // Start is called before the first frame update
    void Start()
    {
        	foundSecondRoom = false;
        	foundFirstRoom = true;
        	
			//Debug information to get initial 'z' position.
        	//print("Position is "+GameObject.FindGameObjectWithTag("ControllerTag").transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
		//Debug information to find 'x' position of the user.
    	//print("Controller pos is "+GameObject.FindGameObjectWithTag("ControllerTag").transform.position.z);
    	//print(this.transform.position.x + "X position");
    	//print(this.transform.position.z + "Z position");
		
		//If 'x' position crosses a specific threshold, second room reached, change the audio.
		//An extra boolean check with foundSecondRoom, so that music is not switched every time update is called.
		
    	if(!foundSecondRoom &&  this.transform.position.x>(28.0f)){ 
    		foundSecondRoom = true;
    		foundFirstRoom = false;
        	//print("reached second");
        	aSource = GameObject.FindGameObjectWithTag("DancingAudio").GetComponent<AudioSource>();
        	aSource.clip = GameObject.FindGameObjectWithTag("calmTag").GetComponent<AudioSource>().clip;
        	aSource.Play();
        }
		
		//If 'x' position is within a threshold, user is currently in the first room.
        else if(!foundFirstRoom && this.transform.position.x<(28.0f)){ 
    		foundFirstRoom = true;
    		foundSecondRoom = false;
        	//print("reached first");
        	aSource = GameObject.FindGameObjectWithTag("DancingAudio").GetComponent<AudioSource>();
        	aSource.clip = GameObject.FindGameObjectWithTag("musicTag").GetComponent<AudioSource>().clip;
        	aSource.Play();
        }
    }
}
