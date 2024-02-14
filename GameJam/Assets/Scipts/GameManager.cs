﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager inst;
    public Dictionary<Ingredient, int> IngredientAmount = new Dictionary<Ingredient, int>();
    public float money = 100;
    public RoundState currentRoundState;
    public GameObject beltSpawnPoint;
    public int ScrollPastTime = 5;
    public int CookingTime = 10;
    public int levelNumber = 0;
    public int[] ingredientsPerLevel = new int[] { 4, 6, 8, 11, 15 };
    public int roundNumber = 0; // 0 equipment, 1 for dish 1, 2, 3
    public Dictionary<Ingredient, PhysicalIngredient> physicalIngredientMap = new Dictionary<Ingredient, PhysicalIngredient>();
    public List<GameObject> ingredientsOnBelt;
    public int totalNumOfIngredients = 0;
    [SerializeField] Transform ingredientParent;
    public List<GameObject> currentFinishedDishesOnScreen;
    public int totalSpawnedDished = 0;

    private void Awake()
    {
        inst = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //IngredientAmount = new Dictionary<Ingredient, int>();
    }

    public void UpdateIngredientAmount(PhysicalIngredient physicalIngredient, int amount)
    {
        if (currentRoundState == RoundState.StopScroll || (RoundManager.inst.instIngredients.Count > 0 && currentRoundState == RoundState.ContinueScroll))
        {
            Ingredient ingredient = physicalIngredient.ingredientType;
            if (amount >= 1 && money < ingredient.GetCost())
                return;
            if (amount <= -1 && (IngredientAmount.ContainsKey(ingredient) ? IngredientAmount[ingredient] : 0) == 0)
                return;
            totalNumOfIngredients += amount;
            if (amount >= 1)
            {
                GameObject x = Instantiate(physicalIngredient.gameObject, beltSpawnPoint.transform.position + new Vector3((-21 * totalNumOfIngredients) - 110, 0, 0), Quaternion.identity, ingredientParent);
                x.GetComponent<PhysicalIngredient>().onBelt = true;
                ingredientsOnBelt.Add(x);
            }
            else if (amount <= -1)
            {
                for (int i = 0; i < ingredientsOnBelt.Count; i++)
                {
                    GameObject x = ingredientsOnBelt[i];
                    if (x.GetComponent<PhysicalIngredient>().ingredientType == physicalIngredient.ingredientType)
                    {
                        ingredientsOnBelt.Remove(x);
                        Destroy(x);
                        break;
                    }
                }
            }

            IngredientAmount[ingredient] = IngredientAmount.ContainsKey(ingredient) ? IngredientAmount[ingredient] + amount : amount;
            IngredientAmount[ingredient] = Mathf.Clamp(IngredientAmount[ingredient], 0, ingredient.GetMax());
            money -= amount * ingredient.GetCost();
            print(IngredientAmount[ingredient]);
        }
    }


	public int GetCount(Ingredient ing)
	{
		return IngredientAmount.ContainsKey(ing) ? IngredientAmount[ing] : 0;
	}


}


public enum LevelState
{
    Equipping,
    Round1,
    Round2,
    Round3,
}

public enum RoundState
{
    ScrollPast,
    StopScroll,
    ContinueScroll,
    ShowScore,
    RoundStart,
    RoundEnd,
}

