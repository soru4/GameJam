using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerItems : MonoBehaviour
{
	public Vector3 startPositionP; 
	public Vector3 endPositionP;
	public Vector3 startPositionN; 
	public Vector3 endPositionN;
	public int distanceModify = 20;
	public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
	    startPositionP = new Vector3(-147f,0,0);
	    endPositionP = new Vector3(257f,0,0);
	    endPositionN = startPositionP;
	    startPositionN = endPositionP;
    }

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
	
	public void SetSpeed(float speed){
		if( System.Math.Round(speed, 1) != System.Math.Round(this.speed, 1)){
			this.speed  = speed;
			
		}
	}
	
	
}
