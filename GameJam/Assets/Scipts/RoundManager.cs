using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    
	private int RoundTimer; // round is one dish
	public Dish dishRef; 
	
	public float elapsedTime = 0f;
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		RoundTimer += GameManager.inst.CookingTime + GameManager.inst.ScrollPastTime;
		
	}
    // Start is called before the first frame update
    void Start()
    {
	    // we want to start with starting the animcontroller
	    GameManager.inst.currentGameState = GameState.ScrollPast;
	    SpawnIngredients();
    }

    // Update is called once per frame
    void Update()
    {
	    switch(GameManager.inst.currentGameState){
	    case GameState.ScrollPast:
	    	if(elapsedTime >= GameManager.inst.ScrollPastTime ){
	    		GameManager.inst.currentGameState = GameState.JustIngredients;
	    		
	    	}
	    	break;
	    }
    }
    
	public void SpawnIngredients(){
		List<GameObject> ingrediests = new List<GameObject>();
		foreach(Ingredient x  in dishRef.valuations.Keys){
			ingrediests.Add(Resources.Load("Prefabs/" + x.ToString() + " Plate") as GameObject);
		}
		
		
	}
}
