using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dish
{
	public static List<Dish> dishes = new List<Dish>();
	public static int dishIndex;

	public static Dish QueryDish() //implement later
	{
		return dishes[dishIndex++];
	}
    
	public static AnimationCurve bellCurve;

	// Static Above, Instance Below

	public string name;
	public Dictionary<Ingredient, (float, float)> valuations;
	
	public Dish(string n, Dictionary<Ingredient, (float, float)> ingredientVals)
	{
		this.name = n;
		valuations = ingredientVals;

		/*
		foreach (Ingredient i in ingredients)
		{
			float max = i.GetMax();
			int mean = (int)Random.Range(1, max);
			int std = (int)(max * 0.25f);
			valuations.Add(i, (mean, std));
		}
		*/

		//this.equipmentValuations = equipmentValuations;
	}


	public int CalculateScore(Dictionary<Ingredient, float> ingredients, int[] equipmentQuantities)
	{
		float sum = 0;

		foreach(var item in valuations)
		{
			if (!ingredients.ContainsKey(item.Key))
				continue;
			UnityEngine.MonoBehaviour.print("Expected: " + item.Key.ToString() +"  :"+ item.Value.Item1  + " float 2: " + item.Value.Item2+ " Given: " + ingredients[item.Key] );
			float zScore = (ingredients[item.Key] - item.Value.Item1) / item.Value.Item2;
			sum += bellCurve.Evaluate(zScore);
			UnityEngine.MonoBehaviour.print("Sum::" + sum);
		}
		sum /= ingredients.Count;

		return (int)Mathf.Round(sum * 100);
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
	public static Dictionary<Ingredient, float> ingredientCosts = new Dictionary<Ingredient, float>();

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