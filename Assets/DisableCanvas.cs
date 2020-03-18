using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvas : MonoBehaviour
{
	public int counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter>1500){
        	//print("Reached counter limit");
        	Destroy(this.gameObject);
        }else{
        	counter++;
        }
        //Destroy(this, 10);
    }
}
