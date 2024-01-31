using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dish
{
	public static List<Dish> dishes = new List<Dish>();

	public static Dish QueryDish(float complexity) //implement later
	{
		return dishes[Random.Range(0, dishes.Count)];
	}
    
	public static AnimationCurve bellCurve;

	// Static Above, Instance Below

	public Dictionary<Ingredient, (float, float)> valuations;
	public Dictionary<Equipments, float> equipmentValuations;
	public int complexityLevel;

	public Dish(List<Ingredient> ingredients, Dictionary<Equipments, float> equipmentValuations, int complexityLevel)
	{
		valuations = new Dictionary<Ingredient, (float, float)>();
		
		foreach (Ingredient i in ingredients)
		{
			float min = i.GetMinCount(), max = i.GetMaxCount();
			float mean = Random.Range(min, max);
			float std = (min + max) * 0.25f;
			valuations.Add(i, (mean, std));
		}

		this.equipmentValuations = equipmentValuations;
		this.complexityLevel = complexityLevel;
	}


	float CalculateScore(Dictionary<Ingredient, float> ingredients, Dictionary<Equipments, float> equipment)
	{
		float sum = 0;
		foreach(var item in valuations)
		{
			float zScore = (ingredients[item.Key] - item.Value.Item1) / item.Value.Item2;
			sum += bellCurve.Evaluate(zScore);
		}
		sum /= ingredients.Count;

		float equipScore = 0;
		foreach(var item in equipmentValuations)
		{
			float score = 0.2f * (item.Value - equipment[item.Key]);
			equipScore += score;
		}

		return Mathf.Clamp01(sum * equipScore);

		// involve equipment as well
	}


}

public enum Ingredient
{
	Avocado, Bread, Cheese, Chocolate, Eggs, Fish, Flour, Lemon, Lettuce, Seasoning, Tomato
}

public static class IngredientMethods
{
	public static Dictionary<Ingredient, (float, float)?> ingredientToMinMax = new Dictionary<Ingredient, (float, float)?>();
	public static Dictionary<Ingredient, float> ingredientCosts = new Dictionary<Ingredient, float>();

	public static float GetMinCount(this Ingredient ing) => (ingredientToMinMax[ing] ?? (0f, 0f)).Item1;
	public static float GetMaxCount(this Ingredient ing) => (ingredientToMinMax[ing] ?? (0f, 0f)).Item2;
	public static void SetMinMax(this Ingredient ing, float min, float max) => ingredientToMinMax[ing] = (min, max);

	public static void SetCost(this Ingredient ing, float cost) => ingredientCosts[ing] = cost;
	public static float GetCost(this Ingredient ing) => ingredientCosts[ing];
}

public enum Equipments{

}
