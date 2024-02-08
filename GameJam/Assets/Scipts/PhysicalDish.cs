using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalDish : MonoBehaviour
{
	public bool onBelt = false;
	

    // Update is called once per frame
    void Update()
	{
    	
		if(onBelt && (GameManager.inst.currentGameState == GameState.ShowScore || GameManager.inst.currentGameState == GameState.ScrollPast)){
			transform.position += new Vector3(RoundManager.inst.beltSpeed * 20 * Time.deltaTime ,0,0);
		}
		if((transform.position.x <= -130 && GameManager.inst.currentGameState == GameState.ShowScore) || (transform.position.x >= 230 && GameManager.inst.currentGameState == GameState.ScrollPast)  && onBelt){
	    	
	    	GameManager.inst.currentFinishedDishesOnScreen.Remove(gameObject);
	    	Destroy(gameObject);
	    }
    }
}
