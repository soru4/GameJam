﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerItems : MonoBehaviour
{
	public Vector3 startPosition; 
	public Vector3 endPosition;
	public int distanceModify = 20;
	public BeltManager beltManager;
    // Start is called before the first frame update
    void Start()
    {
	    startPosition = transform.position;
	    endPosition = startPosition;
	    endPosition.x += distanceModify;
    }

    // Update is called once per frame
	void Update()
	{
		
		if((transform.position.x >= endPosition.x  && beltManager.GetComponent<BeltManager>().speed > 0)|| (transform.position.x <= endPosition.x && beltManager.GetComponent<BeltManager>().speed < 0)){
			
	    	transform.position = startPosition;
	    }
	    
	    transform.position += new Vector3 (distanceModify * beltManager.GetComponent<BeltManager>().speed * Time.deltaTime, 0,0);
	}
	void UpdateEndPos(){
		
		
		if(beltManager.GetComponent<BeltManager>().speed > 0){
			endPosition = startPosition;
			endPosition.x += distanceModify;
		}else{
			endPosition = startPosition;
			endPosition.x -= distanceModify;
		}
		
	}
	
}