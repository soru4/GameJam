
using System;
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
		foreach (Ingredient ing in Enum.GetValues(typeof(Ingredient)))
		{
			ing.SetCost(1);
			ing.SetMax(10);
		}
	}

	void InitializeMinMaxDict()
	{
        
	}

	// Update is called once per frame
	void Update()
	{
        
	}
}


