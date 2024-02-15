using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerItems : MonoBehaviour
{

	
	
	Vector3 startPos;
	public int distanceModify = 20;
	public float speed = 1;
	// Start is called before the first frame update
	void Start()
	{

		
		startPos = transform.position;
		
	}

	/*
	// Update is called once per frame
	void FixedUpdate()
	{
		
		if(( System.Math.Round(transform.position.x,1) >= System.Math.Round( endPositionP.x ,1)&& speed > 0) ){
			
			if(speed > 0){
				
				transform.position = new Vector3(startPositionP.x, transform.position.y, transform.position.z );
			}
			
		}else if( (transform.position.x <= endPositionN.x && speed < 0 )){
			if(speed < 0 ){
				
				transform.position = new Vector3(startPositionN.x, transform.position.y, transform.position.z );
			}
		}
	    
		transform.position += new Vector3 (distanceModify * speed * Time.deltaTime, 0,0);
	}
	*/
	public void SetSpeed(float speed){
		if( System.Math.Round(speed, 1) != System.Math.Round(this.speed, 1)){
			this.speed  = speed;
		}
	}
	
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	void Update()
	{
		Vector3 add = new Vector3(distanceModify * speed * Time.deltaTime, 0,0);
		transform.position += add;
		if(Vector3.Distance(transform.position, startPos) >= distanceModify)
			transform.position = startPos;
	}
	
	
}