using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnOff : MonoBehaviour
{
    public int timer;

    void Start()
    {
    	 timer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (timer>53){
	    	this.GetComponent<Light>().enabled = !this.GetComponent<Light>().enabled;
	    	timer = 0;
		}else{
			timer++;
		}
        
    }
}
