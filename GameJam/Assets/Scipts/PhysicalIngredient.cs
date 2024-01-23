using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalIngredient : MonoBehaviour
{

    public string ingredientName = null;
    [Range(2, 20)]
    public int MaxCount = 10;
    [Range(0, 10)]
    public int MinCount = 0;
    public Ingredient ingredientType;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            GameManager.inst.updateIngredientAmount(this, ingredientType, +1);
            //increments up
        }else if (Input.GetMouseButton(1))
        {
            GameManager.inst.updateIngredientAmount(this, ingredientType, -1);
            // increments down
        }
    }

   
}
