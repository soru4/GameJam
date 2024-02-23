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
        CreateDishes();
        InitializeCostDict();
    }

    void CreateDishes()
    {
        Dish.dishes = new List<Dish>
        {
            new Dish( "Hot_Dog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Hot_Dog_Buns,   (1, 1)  },
                    {Ingredient.Sausages,       (1, 1)  },
                    {Ingredient.Ketchup,        (1, 1)  },
                }
            ),
            new Dish( "Donut",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Eggs,           (2, 0.5f)  },
                    {Ingredient.Dough,          (1, 0.5f)  },
                    {Ingredient.Chocolate,      (2, 1)  },
                }
            ),
            new Dish( "Cake",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Eggs,           (2, 1)  },
                    {Ingredient.Flour,          (2, 1)  },
                    {Ingredient.Chocolate,      (1, 0.3f)  },
                    {Ingredient.Strawberries,   (5, 2)  },
                }
            ),
            new Dish( "Burger",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Buns,           (2, 1)  },
                    {Ingredient.Cheese,         (2, 1)  },
                    {Ingredient.Beef,           (1, 0.3f)  },
                }
            ),
            new Dish( "Ice_Cream",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Milk,               (2, 1)  },
                    {Ingredient.Strawberry_Syrup,   (4, 2)  },
                    {Ingredient.Sprinkles,          (4, 1)  },
                    {Ingredient.Chocolate,          (4, 2)  },
                }
            ),
            new Dish( "Pizza",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Dough,          (1, 0.3f)  },
                    {Ingredient.Pizza_Sauce,    (2, 1)  },
                    {Ingredient.Cheese,         (4, 1)  },
                    {Ingredient.Toppings,       (5, 1)  },
                }
            ),
            new Dish( "Taco",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Taco_Meat,      (3, 1)  },
                    {Ingredient.Taco_Sauce,     (2, 0.4f)  },
                    {Ingredient.Cheese,         (4, 1)  },
                    {Ingredient.Tortillas,      (2, 1)  },
                }
            ),
            new Dish( "Sushi",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Rice_and_Seaweed,   (7, 2)  },
                    {Ingredient.Fish_Fillets,       (3, 1)  },
                    {Ingredient.Avocados,           (4, 1)  },
                }
            ),
        };

        RoundManager.Shuffle(ref Dish.dishes);


        /*
        Dish[] possibleDishes = {
    *        new Dish("HotDog", new List<Ingredient> {Ingredient.Hot_Dog_Buns, Ingredient.Sausages, Ingredient.Ketchup}, new float[6] )
    *        , new Dish("Donut", new List<Ingredient> {Ingredient.Eggs, Ingredient.Dough, Ingredient.Chocolate}, new float[6])
            , new Dish("Cake", new List<Ingredient> {Ingredient.Eggs, Ingredient.Flour, Ingredient.Chocolate, Ingredient.Strawberries}, new float[6])
            , new Dish("Burger", new List<Ingredient> {Ingredient.Buns,Ingredient.Cheese,Ingredient.Beef}, new float[6])
            , new Dish("IceCream", new List<Ingredient> {Ingredient.Eggs, Ingredient.Milk },new float[6])
            , new Dish("Pizza", new List<Ingredient> {Ingredient.Dough, Ingredient.Pizza_Sauce, Ingredient.Cheese, Ingredient.Toppings }, new float[6])
            , new Dish("Taco", new List<Ingredient> {Ingredient.Taco_Meat, Ingredient.Taco_Sauce, Ingredient.Cheese, Ingredient.Tortillas }, new float[6])
            , new Dish("Sushi", new List<Ingredient> {Ingredient.Rice_and_Seaweed, Ingredient.Fish_Fillets, Ingredient.Avocados},new float[6])
        };
        */
    }

    void InitializeCostDict()
    {
        Ingredient.Hot_Dog_Buns.SetCost(3);
        Ingredient.Sausages.SetCost(7);
        Ingredient.Ketchup.SetCost(1);
        Ingredient.Mustard.SetCost(1.3f);
        Ingredient.Dough.SetCost(4f);
        Ingredient.Eggs.SetCost(3);
        Ingredient.Chocolate.SetCost(4);
        Ingredient.Flour.SetCost(3);
        Ingredient.Milk.SetCost(3);
        Ingredient.Strawberries.SetCost(2);
        Ingredient.Buns.SetCost(3);
        Ingredient.Cheese.SetCost(2);
        Ingredient.Beef.SetCost(5);
        Ingredient.Lettuce.SetCost(10);
        Ingredient.Pizza_Sauce.SetCost(12);
        Ingredient.Toppings.SetCost(24);
        Ingredient.Taco_Meat.SetCost(9);
        Ingredient.Taco_Sauce.SetCost(7);
        Ingredient.Tortillas.SetCost(3);
        Ingredient.Rice_and_Seaweed.SetCost(7);
        Ingredient.Fish_Fillets.SetCost(100);
        Ingredient.Avocados.SetCost(2);
        /*
	    Equipment.Knives.SetCosts((10, 20));
	    Equipment.Microwave.SetCosts((10, 20));
	    Equipment.Mixer.SetCosts((10, 20));
	    Equipment.Pans.SetCosts((10, 20));
	    Equipment.Stove.SetCosts((10, 20));
	    Equipment.Tools.SetCosts((10, 20));
	    */
    }


    // Update is called once per frame
    void Update()
    {

    }
}


