using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalDish : MonoBehaviour
{
	public bool onBelt = false;
	

	// Update is called once per frame
	void Update()
	{
    	
		if(onBelt && (GameManager.inst.currentRoundState == RoundState.ShowScore || GameManager.inst.currentRoundState == RoundState.ScrollPast)){
			transform.position += new Vector3(BeltManager.inst.speed * 20 * Time.deltaTime ,0,0);
		}
		if((transform.position.x <= -130 && GameManager.inst.currentRoundState == RoundState.ShowScore) || (transform.position.x >= 230 && GameManager.inst.currentRoundState == RoundState.ScrollPast)  && onBelt){
	    	
			GameManager.inst.currentFinishedDishesOnScreen.Remove(gameObject);
			Destroy(gameObject);
		}
	}
}