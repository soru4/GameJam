using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalIngredient : MonoBehaviour
{

    public string ingredientName = null;
    public Ingredient ingredientType;
    public bool onBelt = false;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        ingredientName = gameObject.name.Replace(" ", "_");
        ingredientType = (Ingredient)Enum.Parse(typeof(Ingredient), ingredientName);
    }

    // Update is called once per frame
    void Update()
    {
	    if (onBelt && (GameManager.inst.currentGameState == RoundState.StopScroll || GameManager.inst.currentGameState == RoundState.ContinueScroll || GameManager.inst.currentGameState == RoundState.ShowScore) )
        {
	        transform.position += new Vector3(BeltManager.inst.speed * 20 * Time.deltaTime, 0, 0);
        }
        if (transform.position.x >= 270 && onBelt)
        {

            GameManager.inst.ingredientsOnBelt.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.inst.UpdateIngredientAmount(this, +1);
            print("adding 1 " + ingredientName);
            //increments up
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameManager.inst.UpdateIngredientAmount(this, -1);
            // increments down
        }
    }

}

