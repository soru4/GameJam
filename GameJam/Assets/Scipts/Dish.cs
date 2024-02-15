using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dish
{
	public static List<Dish> dishes = new List<Dish>();

	public static Dish QueryDish() //implement later
	{
		return dishes[Random.Range(0, dishes.Count)];
	}
    
	public static AnimationCurve bellCurve;

	// Static Above, Instance Below

	public string name;
	public Dictionary<Ingredient, (float, float)> valuations;
	public Dictionary<Equipment, float> equipmentValuations;

	public Dish(string n, List<Ingredient> ingredients, Dictionary<Equipment, float> equipmentValuations)
	{
		this.name = n;
		valuations = new Dictionary<Ingredient, (float, float)>();
		
		foreach (Ingredient i in ingredients)
		{
			float max = i.GetMax();
			float mean = Random.Range(0, max);
			float std = max * 0.25f;
			valuations.Add(i, (mean, std));
		}

		this.equipmentValuations = equipmentValuations;
	}


	public float CalculateScore(Dictionary<Ingredient, float> ingredients, Dictionary<Equipment, float> equipment)
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
	Avocados, Beef, Bread, Broth, Buns, Cheese, Chocolate, Dough, Eggs, Fish_Fillets, Flour, Grape_Juice, Grapes, Hot_Dog_Buns, Ice_Cream_Cones,
	Ketchup, Lemons, Lettuce, Milk, Mustard, Pizza_Sauce, Rice_and_Seaweed, Sausages, Seasoning, Soy_Sauce, Sprinkles, Stew_Vegetables, Strawberries,
	Strawberry_Syrup, Sugar, Taco_Meat, Taco_Sauce, Tomatoes, Toppings, Tortillas, Vegetables, Whipped_Cream
}

public static class IngredientMethods
{
	public static Dictionary<Ingredient, int> ingredientMax = new Dictionary<Ingredient, int>();
	public static Dictionary<Ingredient, float> ingredientCosts = new Dictionary<Ingredient, float>();

	public static void SetMax(this Ingredient ing, int max) => ingredientMax[ing] = max;
	public static int GetMax(this Ingredient ing) => ingredientMax[ing];

	public static void SetCost(this Ingredient ing, float cost) => ingredientCosts[ing] = cost;
	public static float GetCost(this Ingredient ing) => ingredientCosts[ing];
}



public enum Equipment
{
	Knives, Tools, Pans, Stove, Microwave, Mixer
}

public static class EquipmentMethods
{
	public static Dictionary<Equipment, (float, float)> upgradeCosts = new Dictionary<Equipment, (float, float)>();
	public static void SetCosts(this Equipment eq, (float, float) vals) => upgradeCosts[eq] = vals;
	public static float GetCost(this Equipment eq, int id) => id == 0 ? upgradeCosts[eq].Item1 : upgradeCosts[eq].Item2;
}
