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
        Ingredient.Avocado.     SetMax(4);
        Ingredient.Bread.       SetMax(5);
        Ingredient.Cheese.      SetMax(10);
        Ingredient.Chocolate.   SetMax(3);
        Ingredient.Eggs.        SetMax(8);
        Ingredient.Fish.        SetMax(3);
        Ingredient.Flour.       SetMax(4);
        Ingredient.Lemon.       SetMax(5);
        Ingredient.Lettuce.     SetMax(3);
        Ingredient.Seasoning.   SetMax(15);
        Ingredient.Tomato.      SetMax(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
