using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public AnimationCurve bellCurve;

    // Start is called before the first frame update
    void Awake()
    {
        Dish.bellCurve = bellCurve;
        InitializeCostDict();
        InitializeMinMaxDict();
    }

    void InitializeCostDict()
    {
        Ingredient.Avocado.     SetCost(1);
        Ingredient.Bread.       SetCost(1);
        Ingredient.Cheese.      SetCost(1);
        Ingredient.Chocolate.   SetCost(1);
        Ingredient.Eggs.        SetCost(1);
        Ingredient.Fish.        SetCost(1);
        Ingredient.Flour.       SetCost(1);
        Ingredient.Lemon.       SetCost(1);
        Ingredient.Lettuce.     SetCost(1);
        Ingredient.Seasoning.   SetCost(1);
        Ingredient.Tomato.      SetCost(1);
    }

    void InitializeMinMaxDict()
    {
        Ingredient.Avocado.     SetMinMax(1, 4);
        Ingredient.Bread.       SetMinMax(1, 5);
        Ingredient.Cheese.      SetMinMax(1, 10);
        Ingredient.Chocolate.   SetMinMax(1, 3);
        Ingredient.Eggs.        SetMinMax(1, 8);
        Ingredient.Fish.        SetMinMax(1, 3);
        Ingredient.Flour.       SetMinMax(1, 4);
        Ingredient.Lemon.       SetMinMax(1, 5);
        Ingredient.Lettuce.     SetMinMax(1, 3);
        Ingredient.Seasoning.   SetMinMax(1, 15);
        Ingredient.Tomato.      SetMinMax(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
