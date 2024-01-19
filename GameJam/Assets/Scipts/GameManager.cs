using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst; 
    Dictionary<Ingredient, int> IngredientAmount = new Dictionary<Ingredient, int>();
    public int Money = 100; 
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
    public void updateIngredientAmount(PhysicalIngredient physicalIngredient,  Ingredient ingredient, int amount)
    {
        // check MONEY!!!!!!!!!!!!!!!!
        IngredientAmount[ingredient] = IngredientAmount.ContainsKey(ingredient) ? IngredientAmount[ingredient] + amount : amount;
        IngredientAmount[ingredient] = Mathf.Clamp(IngredientAmount[ingredient], physicalIngredient.MinCount, physicalIngredient.MaxCount);
    }
}
