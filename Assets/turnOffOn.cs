using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffOn : MonoBehaviour
{
    // Start is called before the first frame update

	public int timer;

    void Start()
    {
    	 timer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (timer>31){
	    	this.GetComponent<Light>().enabled = !this.GetComponent<Light>().enabled;
	    	timer = 0;
		}else{
			timer++;
		}
        
    }
}
