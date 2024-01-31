using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    public static GameManager inst; 
    public Dictionary<Ingredient, int> IngredientAmount = new Dictionary<Ingredient, int>();
    public float money = 100;
    public GameState currentGameState;
    
    public int ScrollPastTime = 5;
    public int CookingTime = 10;
    public int levelNumber = 0;
    public int[] ingredientsPerLevel = new int[] { 4, 6, 8, 11, 15 };
	public int roundNumber = 0; // 0 equipment, 1 for dish 1, 2, 3
	public Dictionary<Ingredient, PhysicalIngredient> physicalIngredientMap = new Dictionary<Ingredient, PhysicalIngredient>();


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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string s = "";
            foreach(var pair in IngredientAmount)
            {
                if (pair.Value == 0)
                    continue;
                s += pair.Key.ToString() + " " + pair.Value + ", ";
            }
            print(s);
            IngredientAmount = new Dictionary<Ingredient, int>();
        }
    }

    public void UpdateIngredientAmount(PhysicalIngredient physicalIngredient, int amount)
    {


        Ingredient ingredient = physicalIngredient.ingredientType;

        if(money >= ingredient.GetCost())
        {
            IngredientAmount[ingredient] = IngredientAmount.ContainsKey(ingredient) ? IngredientAmount[ingredient] + amount : amount;
            IngredientAmount[ingredient] = Mathf.Clamp(IngredientAmount[ingredient], 0, ingredient.GetMax());
            money += amount * ingredient.GetCost();
        }


    }

    
}
public enum GameState
    {
	    ScrollPast,
	    JustIngredients,
	    ShowScore, 
	    RoundStart,
	    RoundEnd,
    }
