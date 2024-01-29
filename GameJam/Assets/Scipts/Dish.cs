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

	public Dictionary<Ingredient, (float, float)> valuations;
	public Dictionary<Equipments, float> equipmentValuations;
	public int complexityLevel;

	public Dish(List<Ingredient> ingredients, Dictionary<Equipments, float> equipmentValuations, int complexityLevel)
	{
		foreach (Ingredient i in ingredients)
		{
			float min = GameManager.inst.physicalIngredientMap[i].MinCount, max = GameManager.inst.physicalIngredientMap[i].MaxCount;
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

public enum Equipments{

}
