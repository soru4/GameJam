using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dish
{
    public static List<Dish> dishes = new List<Dish>
    {

    };

    public static Dish QueryDish(float complexity) //implement later
    {
        return dishes[Random.Range(0, dishes.Count)];
    }

    public static Dictionary<Ingredient, float> ingredientCosts = new Dictionary<Ingredient, float>
    {

    };

    public static AnimationCurve bellCurve;
    // Static Above, Instance Below


    Dictionary<Ingredient, (float, float)> valuations;
    Dictionary<Equipments, float> equipmentValuations;
    int complexityLevel;

    public Dish(Dictionary<Ingredient, (float, float)> valuations, Dictionary<Equipments, float> equipmentValuations, int complexityLevel)
    {

    }

    float CalculateScore(Dictionary<Ingredient, float> ingredients, Dictionary<Ingredient, float> equipment)
    {
        float sum = 0;
        foreach (var item in ingredients)
        {
            float zScore = (item.Value - valuations[item.Key].Item1) / valuations[item.Key].Item2;
            sum += bellCurve.Evaluate(zScore);
        }
        sum /= ingredients.Count;
        return sum;

        // involve equipment as well
    }


}

public enum Ingredient
{
    Eggs, Milk, Flour, Sugar,

    
}

public enum Equipments
{

}
