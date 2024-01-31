using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalIngredient : MonoBehaviour
{
    public string ingredientName = null;
    public Ingredient ingredientType;

    // Start is called before the first frame update
    void Start()
    {
        ingredientName = gameObject.name.Split()[0];
        ingredientType = (Ingredient)Enum.Parse(typeof(Ingredient), ingredientName);
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
