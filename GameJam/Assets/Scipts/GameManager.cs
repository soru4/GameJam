using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    public static GameManager inst; 
    Dictionary<Ingredient, int> IngredientAmount = new Dictionary<Ingredient, int>();
    public int Money = 100;
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
        
    }
    public void updateIngredientAmount(PhysicalIngredient physicalIngredient, int amount)
    {
        Ingredient ingredient = physicalIngredient.ingredientType;
        // check MONEY!!!!!!!!!!!!!!!!
        IngredientAmount[ingredient] = IngredientAmount.ContainsKey(ingredient) ? IngredientAmount[ingredient] + amount : amount;
        IngredientAmount[ingredient] = Mathf.Clamp(IngredientAmount[ingredient], physicalIngredient.MinCount, physicalIngredient.MaxCount);
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
