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

    void CreateDishes()
    {
        Dish.dishes = new List<Dish>
        {
            new Dish( "HotDog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Hot_Dog_Buns,   (2, 1)  },
                    {Ingredient.Sausages,       (0, 0)  },
                    {Ingredient.Ketchup,        (0, 0)  },  
                }
            ),
            new Dish( "HotDog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Eggs,   (2, 1)  },
                    {Ingredient.Dough,       (0, 0)  },
                    {Ingredient.Chocolate,        (0, 0)  },
                }
            ),
            new Dish( "HotDog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Eggs,   (2, 1)  },
                    {Ingredient.Flour,       (0, 0)  },
                    {Ingredient.Chocolate,        (0, 0)  },
                    {Ingredient.Strawberries,        (0, 0)  },
                }
            ),
            new Dish( "HotDog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Eggs,   (2, 1)  },
                    {Ingredient.Dough,       (0, 0)  },
                    {Ingredient.Chocolate,        (0, 0)  },
                }
            ),
            new Dish( "HotDog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Buns,   (2, 1)  },
                    {Ingredient.Cheese,       (0, 0)  },
                    {Ingredient.Beef,        (0, 0)  },
                }
            ),
            new Dish( "HotDog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Eggs,   (2, 1)  },
                    {Ingredient.Milk,       (0, 0)  },
                    {Ingredient.Strawberry_Syrup,        (0, 0)  },
                }
            ),
            new Dish( "HotDog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Dough,   (2, 1)  },
                    {Ingredient.Pizza_Sauce,       (0, 0)  },
                    {Ingredient.Cheese,        (0, 0)  },
                    {Ingredient.Toppings,        (0, 0)  },
                }
            ),
            new Dish( "HotDog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Taco_Meat,   (2, 1)  },
                    {Ingredient.Taco_Sauce,       (0, 0)  },
                    {Ingredient.Cheese,        (0, 0)  },
                    {Ingredient.Tortillas,        (0, 0)  },
                }
            ),
            new Dish( "HotDog",
                new Dictionary<Ingredient, (float,float)>
                {
                    {Ingredient.Rice_and_Seaweed,   (2, 1)  },
                    {Ingredient.Fish_Fillets,       (0, 0)  },
                    {Ingredient.Avocados,        (0, 0)  },
                }
            ),
        };


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
        foreach (Ingredient ing in Enum.GetValues(typeof(Ingredient)))
        {
            ing.SetCost(5);
            ing.SetMax(10);
        }
	    Equipment.Knives.SetCosts((10, 20));
	    Equipment.Microwave.SetCosts((10, 20));
	    Equipment.Mixer.SetCosts((10, 20));
	    Equipment.Pans.SetCosts((10, 20));
	    Equipment.Stove.SetCosts((10, 20));
	    Equipment.Tools.SetCosts((10, 20));
    }

    void InitializeMinMaxDict()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
